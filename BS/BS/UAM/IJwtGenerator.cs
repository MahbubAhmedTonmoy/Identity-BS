using Core;
using System;
using System.Security.Claims;
using UAM.DTO;

namespace UAM
{
    public interface IJwtGenerator
    {
        public TokenResponse CreateToken(AppUser user, string[] role);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public string GenerateRefreshToken();
    }
}
