using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Ordering.API.Policies
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }

        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }

    public class MinimalAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type.Equals("Age")))
            {
                var age = int.Parse(context.User.FindFirst(c => c.Type.Equals("Age")).Value);


                if (age >= requirement.MinimumAge)
                {
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }

            return Task.CompletedTask;

        }
    }

}
