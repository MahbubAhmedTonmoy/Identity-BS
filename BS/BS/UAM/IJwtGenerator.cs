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
        public TokenInformation GetToeknInformation(string token);
    }
}

public class TokenInformation
{
    public string nameid { get; set; }
    public string unique_name { get; set; }
    public string email { get; set; }
    public string role { get; set; }
    public int nbf { get; set; }
    public int exp { get; set; }
    public int iat { get; set; }
    public string Origin { get; set; }
}
