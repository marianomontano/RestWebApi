using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestWebApi.Abstractions;

namespace RestWebApi.Services
{
    public class TokenHandlerService : ITokenHandlerService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenHandlerService(IOptionsMonitor<JwtConfig> optionsMonitor, JwtSecurityTokenHandler tokenHandler)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenHandler = tokenHandler;
        }

        public string GenerateJwtToken(ITokenParameters parameters)
        {
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", parameters.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, parameters.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, parameters.UserName)
                }),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = _tokenHandler.WriteToken(token);
            
            return jwtToken;
        }

        public IEnumerable<Claim> DecodeTokenClaims(string header)
        {
            string authHeader = header.Replace("Bearer ", "");
            var token = (JwtSecurityToken) _tokenHandler.ReadToken(authHeader);
            return token.Claims;
        }
    }
}