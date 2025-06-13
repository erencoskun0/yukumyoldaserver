using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using yukumyolda.Application.Tokens;
using yukumyolda.Domain.Entities;
using yukumyolda.Infrastructure.Tokens;

namespace yukumyolda.Infrastructure.Tokens;

public class TokenService : ITokenService
{

    private UserManager<User> userManager { get; }
    private TokenSettings tokenSettings { get; }
    public TokenService(IOptions<TokenSettings> options, UserManager<User> userManager)
    {
        this.tokenSettings = options.Value;

        this.userManager = userManager;
    }



    public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
    {
        var claims = new List<Claim>(){
            new Claim("jti", Guid.NewGuid().ToString()),
            new Claim("userId", user.Id.ToString()),
            new Claim("email", user.Email ),
            new Claim("phoneNumber", user.PhoneNumber ),

            };
        foreach (var role in roles)
        {
            claims.Add(new Claim("role", role));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret));

        var token = new JwtSecurityToken(
            issuer: tokenSettings.Issuer,
            audience: tokenSettings.Audience,
            expires: DateTime.Now.AddMinutes(tokenSettings.TokenValidityInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        await userManager.AddClaimsAsync(user, claims);
        return token;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPricipalFromExpiredToken(string? token)
    {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Secret)),
            ValidateLifetime = false
        };
        JwtSecurityTokenHandler tokenHandler = new();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token Bulunamadı");
        }
        return principal;

    }
}
