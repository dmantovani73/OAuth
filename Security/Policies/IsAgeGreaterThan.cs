using Microsoft.AspNetCore.Authorization;

namespace OAuth.Security.Policies
{
    public class IsAgeGreaterThan : IAuthorizationRequirement
    {
        public int Age { get; init; }
    }
}
