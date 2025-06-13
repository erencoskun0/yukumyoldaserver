using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using yukumyolda.Domain.Entities;

namespace yukumyolda.Application.Tokens;

public interface ITokenService
{
    Task<JwtSecurityToken> CreateToken(User user, IList<string> roles);
    string GenerateRefreshToken();

    ClaimsPrincipal? GetPricipalFromExpiredToken(string? token);
}
