using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var identityUserId = identity?.FindFirst("UserId")?.Value;

            return int.TryParse(identityUserId, out var userId) ? userId : 0; // Return 0 if
        }
        public bool GetUserRole()
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var identityUserRole = identity?.FindFirst("isAdmin")?.Value;
            if (bool.TryParse(identityUserRole, out var parseIdentityUserRole)) 
            {
                return parseIdentityUserRole;
            }

            return false;
        }
    }
}
