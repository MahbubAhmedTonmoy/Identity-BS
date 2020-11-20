using System;
using System.Collections.Generic;
using System.Text;

namespace UAM.DTO
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string expires_in { get; set; }
        public string RefreshToken { get; set; }
    }
}
