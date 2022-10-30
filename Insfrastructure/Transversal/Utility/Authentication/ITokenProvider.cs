using System.Collections.Generic;
using System.Security.Claims;

namespace IFramework.Infrastructure.Utility.Authentication
{
    public interface ITokenProvider
    {
        string CreateToken(string userId);
        bool ValidateToken(string authToken);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}