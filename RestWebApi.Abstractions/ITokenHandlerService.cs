using System.Collections.Generic;
using System.Security.Claims;

namespace RestWebApi.Abstractions
{
    public interface ITokenHandlerService
    {
        string GenerateJwtToken(ITokenParameters parameters);
        IEnumerable<Claim> DecodeTokenClaims(string header);
    }
}