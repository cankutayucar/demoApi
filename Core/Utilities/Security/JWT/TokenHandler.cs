using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.JWT
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token createToken(User user, List<OperationClaim> operationClaims)
        {
            Token token = new Token();
            // security key symetric
            SymmetricSecurityKey securitykey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            // şifrelenmiş kimlik 
            SigningCredentials credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature);
            // token ayarları
            token.Expiration = DateTime.Now.AddMinutes(60);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: token.Expiration,
                claims: SetClaims(user, operationClaims),
                notBefore: DateTime.Now,
                signingCredentials: credentials
            );
            //token oluşturucu sınıfından örnek
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //token üretim
            token.AccessToken = handler.WriteToken(securityToken);
            return token;
        }


        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddName(user.Name);
            claims.AddRoles(operationClaims.Select(o => o.Name).ToArray());
            return claims;
        }

    }
}
