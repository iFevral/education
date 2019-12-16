using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogic.Models.Users;
using Store.Presentation.Helpers.Interface;

namespace Store.Presentation.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        public string GenerateToken(UserModelItem userModel, double expirationTime, string secretKey, bool isAccess)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var claims = isAccess 
                ? GetAccessClaims(userModel) 
                : GetRefreshClaims(userModel);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                                                            SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public long GetUserIdFromToken(string token)
        {
            JwtSecurityToken refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            long id = Convert.ToInt64(refreshToken.Payload.Where(x => x.Key == "nameid").FirstOrDefault().Value);
            return id;
        }

        private ICollection<Claim> GetRefreshClaims(UserModelItem userModel)
        {
            var claims = new List<Claim>();

            var claim = new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims.Add(claim);

            claim = new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString());
            claims.Add(claim);

            return claims;
        }

        private ICollection<Claim> GetAccessClaims(UserModelItem userModel)
        {
            var claims = GetRefreshClaims(userModel);

            var claim = new Claim(ClaimTypes.Email, userModel.Email);
            claims.Add(claim);

            claim = new Claim(ClaimTypes.Role, userModel.Roles.First());
            claims.Add(claim);

            return claims;
        }
    }
}
