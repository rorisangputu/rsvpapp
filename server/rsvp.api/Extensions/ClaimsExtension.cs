using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace rsvp.api.Extensions
{
    public static class ClaimsExtension
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.GivenName)?.Value ?? string.Empty;

            if (claim == null)
            {
                throw new InvalidOperationException("GivenName claim not found in the JWT");
            }

            return claim;
        }
    }
}