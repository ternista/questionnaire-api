using System.Security.Authentication;
using System.Security.Claims;

namespace Questionnaires.Api.AuthenticationMock
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetIntClaim(ApplicationClaimTypes.UserId);
        }

        public static int GetUserDepartmentId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetIntClaim(ApplicationClaimTypes.DepartmentId);
        }

        private static int GetIntClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var stringValue = claimsPrincipal.FindFirstValue(claimType);
            if (!int.TryParse(stringValue, out var result))
                throw new AuthenticationException($"User doesn't have required claim {claimType}");

            return result;
        }
    }
}