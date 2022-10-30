using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Utility.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IFramework.Infrastructure.Utility.Authentication
{
    public class TokenProvider : ITokenProvider
    {
        private IOptions<IFrameworkConfig> _configuration;
        public TokenProvider(IOptions<IFrameworkConfig> configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.Value.Token.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, userId)
                }),
                Expires = DateTime.UtcNow.AddSeconds(_configuration.Value.Token.TokenExpireSecond),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration.Value.Token.Audience,
                Issuer = _configuration.Value.Token.Issuer
            };
            var token2 = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token2);
        }
        public bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetTokenValidationParameters();
            SecurityToken validatedToken;
            try
            {
                tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenInfo = handler.ReadJwtToken(token);
            return tokenInfo.Claims;
        }
        private SecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.Token.Secret));
        }
        private TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.Token.Issuer,
                ValidAudience = IoCResolver.Instance.ReleaseInstance<IOptions<IFrameworkConfig>>().Value.Token.Audience,
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
    }
}
