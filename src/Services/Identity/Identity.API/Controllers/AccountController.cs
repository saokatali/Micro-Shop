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
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly AppSettings appSettings;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptionsSnapshot<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appSettings = appSettings.Value;
        }


        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInDto data)
        {


            var result = await signInManager.PasswordSignInAsync(data.UserName, data.Password, false, false);
            if (result.Succeeded)
            {
                var user = userManager.Users.SingleOrDefault(r => r.UserName == data.UserName);
                var roles = await userManager.GetRolesAsync(user);
                var token = GenerateJwtToken(user, roles);
                return Ok(new { Token = token });
            }
            else if(result.RequiresTwoFactor)
            {
                //Send a a code to the user and cache it
                return Ok("TwoFactor");
            }
            else
            {
                return Unauthorized();
            }

        }
        [HttpPost("SignInTwoFactor")]
        public async Task<IActionResult> SignInTwoFactor(string  code)
        {
            var cachedCode = string.Empty; // get it from cache key
            var userName = string.Empty; // get it from cache Value

            if (code == cachedCode)
            {
                var user = userManager.Users.SingleOrDefault(r => r.UserName == userName);
                var roles = await userManager.GetRolesAsync(user);
                var token = GenerateJwtToken(user, roles);
                return Ok(new { Token = token });
            }

            else
            {
                return Unauthorized();
            }

        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDto data)
        {

            var user = new AppUser
            {
                UserName = data.UserName,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber

            };
            var result = await userManager.CreateAsync(user, data.Password);
            foreach (var role in data.Roles)
            {
                await userManager.AddToRoleAsync(user, role);
            }


            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                var token = GenerateJwtToken(user, data.Roles);
                return Ok(new { Token = token });
            }

            return BadRequest(result.Errors.Select(e => e.Description));

        }

        private string GenerateJwtToken(AppUser user, IList<string> roles)
        {

            var claims = new List<Claim>
            {
                new Claim("userName", user.UserName),
                new Claim("userId", user.Id.ToString())

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

            }

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