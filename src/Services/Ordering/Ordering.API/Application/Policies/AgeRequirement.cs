using Microsoft.AspNetCore.Authorization;

namespace Ordering.API.Policies
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }
        public int MaximumAge { get; }

        public AgeRequirement(int minimumAge, int maximumAge)
        {
            MinimumAge = minimumAge;
            MaximumAge = maximumAge;
        }
    }

    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type.Equals("Age")))
            {
                var age = int.Parse(context.User.FindFirst(c => c.Type.Equals("Age")).Value);

                if (age >= requirement.MinimumAge && age <= requirement.MaximumAge)
                {
                    context.Succeed(requirement);
                }

                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}