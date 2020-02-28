using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.API.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        UserManager<AppUser> userManager;
        SignInManager<AppUser> signInManager;
        AppSettings appSettings;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptionsSnapshot<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
        }


        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel user)
        {
            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
                if (result.Succeeded)
                {
                    var appUser = userManager.Users.SingleOrDefault(r => r.Email == user.Email);
                    var token = GenerateJwtToken(user.Email, appUser);
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized();
                }


            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    var token = GenerateJwtToken(user.Email, user);
                    return Ok(new { Token = token });
                }
            }


            return BadRequest();

        }

        private string GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, email)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(appSettings.JWT.ExpireDays));

            var token = new JwtSecurityToken(
                appSettings.JWT.Issuer,
               appSettings.JWT.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}