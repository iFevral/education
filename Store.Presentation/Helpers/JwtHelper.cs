using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Models.Users;
using Store.Presentation.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentation.Helpers
{
    public class AuthTokenProviderOptions
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public int JwtExpireMinutes { get; set; }
        public TimeSpan AccessTokenExpiration { get; set; } = TimeSpan.FromMinutes(10);
        public TimeSpan RefreshTokenExpiration { get; set; } = TimeSpan.FromDays(60);
    }
    public static class JwtHelper
    {
        public static TokenModel GenerateJwtToken(UserModelItem user, IConfiguration _configuration)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            //Access token options
            var accessClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Roles.First()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var accessKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var accessCreds = new SigningCredentials(accessKey, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                accessClaims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtExpireMinutes"])),
                signingCredentials: accessCreds
            );


            //Refresh token options
            var refreshClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var refreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var refreshCreds = new SigningCredentials(refreshKey, SecurityAlgorithms.HmacSha256);

            var refreshToken = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                refreshClaims,
                expires: DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"])),
                signingCredentials: refreshCreds
            );


            return new TokenModel
            {
                AccessToken = handler.WriteToken(accessToken),
                RefreshToken = handler.WriteToken(refreshToken)
            };
        }
    }
}
