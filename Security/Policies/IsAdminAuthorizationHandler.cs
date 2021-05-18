using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace OAuth.Security.Policies
{
    public class IsAdminAuthorizationHandler : AuthorizationHandler<IsAdmin>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdmin requirement)
        {
            if (context.User.HasClaim(ClaimName.Role, RoleName.Admin))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
