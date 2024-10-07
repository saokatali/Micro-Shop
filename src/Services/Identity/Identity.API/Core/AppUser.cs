using Microsoft.AspNetCore.Identity;

namespace Identity.API.Core
{
    public class AppUser : IdentityUser<Guid>
    {
        public string City { get; set; }

        public string Country { get; set; }
    }
}