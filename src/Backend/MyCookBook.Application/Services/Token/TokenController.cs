using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyCookBook.Application.Services.Token
{
    public class TokenController
    {
        private const string EmailAlias = "eml";
        private readonly double _tokenLifetimeInMinutes;
        private readonly string _securitykey;

        public TokenController(double tokenLifetimeInMinutes, string securitykey)
        {
            _tokenLifetimeInMinutes = tokenLifetimeInMinutes;
            _securitykey = securitykey;
        }

        public string GenerateToken(string userEmail) 
        {
            var claims = new List<Claim> 
            { 
                 new Claim(EmailAlias,userEmail)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenLifetimeInMinutes),
                SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public void ValidateToken(string token) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters 
            {
                RequireExpirationTime = true,
                IssuerSigningKey = SimetricKey(),
                ClockSkew = new TimeSpan(0),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            tokenHandler.ValidateToken(token, validationParameters, out _);
        }

        private SymmetricSecurityKey SimetricKey()
        {
            var symmetricKey = Convert.FromBase64String(_securitykey);
            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}
