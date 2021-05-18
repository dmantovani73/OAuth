using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OAuth.Security.Policies
{
    public class IsAgeGreaterThanAuthorizationHandler : AuthorizationHandler<IsAgeGreaterThan>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAgeGreaterThan requirement)
        {
            var claim = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimName.DateOfBirth);
            if (claim == null)
            {
                context.Fail();
            }
            else
            {
                if (DateTime.TryParseExact(claim.Value, "yyyy-MM-dd", null, DateTimeStyles.None, out var dateOfBirth))
                {
                    var age = (DateTime.Today - dateOfBirth).TotalDays / 365;
                    if (age >= requirement.Age)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
                else
                {
                    context.Fail();
                }
            }

            return Task.CompletedTask;
        }
    }
}
