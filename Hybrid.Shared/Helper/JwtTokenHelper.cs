using System.IdentityModel.Tokens.Jwt;

namespace Hybrid.Shared.Helper
{
    public static class JwtTokenHelper
    {
        public static JwtSecurityToken GetJwtToken(string token)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            return jwtToken.ReadJwtToken(token);
        }

        public static DateTime GetTokenExpiryTime(string time)
        {
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(time)).UtcDateTime.ToLocalTime();
        }
    }

}
