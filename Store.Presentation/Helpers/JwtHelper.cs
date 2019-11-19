using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Models.Users;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace Store.Presentation.Helpers
{
    public class JwtHelper
    {
        public static Claim[] GetAccessClaims(UserModelItem user)
        {
            return new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Roles.First())
                    };
        }

        public static Claim[] GetRefreshClaims(UserModelItem user)
        {
            return new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };
        }

        public static string GenerateJwtAccessToken(UserModelItem user, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetAccessClaims(user)),
                Expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(configuration["AccessTokenExpireMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtKey"])),
                                                            SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateJwtRefreshToken(UserModelItem user, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetRefreshClaims(user)),
                Expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(configuration["RefreshTokenExpireMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtKey"])),
                                                            SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetUserIdFromToken(string token)
        {
            JwtSecurityToken refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return refreshToken.Payload.Where(x => x.Key == "nameid").FirstOrDefault().Value.ToString();
        }
    }
}
