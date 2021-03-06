﻿using System;
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
            token = RemoveBearerPrefix(token);
            var tokenData = new JwtSecurityTokenHandler().ReadJwtToken(token);
            long id = Convert.ToInt64(tokenData.Payload.Where(x => x.Key == "nameid").FirstOrDefault().Value);
            return id;
        }

        public string GetUserRoleFromToken(string token)
        {
            token = RemoveBearerPrefix(token);
            JwtSecurityToken tokenData = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string role = tokenData.Payload.Where(x => x.Key == "role").FirstOrDefault().Value.ToString();
            return role;
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

            claim = new Claim(ClaimTypes.Role, userModel.Role);
            claims.Add(claim);

            return claims;
        }

        private string RemoveBearerPrefix(string token)
        {
            token = token.Substring(7);
            return token;
        }
    }
}
