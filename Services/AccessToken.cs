using System;
using System.IdentityModel.Tokens.Jwt;

namespace Services
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public DateTime expires_in_date { get; set; }

        public AccessToken(JwtSecurityToken securityToken)
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            token_type = "Bearer";
            refresh_token = "1111asfv";
            expires_in = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
            expires_in_date = securityToken.ValidTo;
        }
    }
}
