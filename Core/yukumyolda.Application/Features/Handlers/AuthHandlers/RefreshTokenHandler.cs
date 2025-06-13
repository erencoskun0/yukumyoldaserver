using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using yukumyolda.Application.Features.Commands.AuthCommands;
using yukumyolda.Application.Features.Results.AuthResults;
using yukumyolda.Application.Tokens;
using yukumyolda.Domain.Entities;
using System.Security.Claims;
namespace yukumyolda.Application.Features.Handlers.AuthHandlers
{
    public class RefreshTokenHandler:IRequestHandler<RefreshTokenCommand, RefreshTokenResults>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenResults> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {

            var principal = _tokenService.GetPricipalFromExpiredToken(request.AccessToken);
            string userId = principal.FindFirstValue("userId");

            var user = await _userManager.FindByIdAsync(userId);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new Exception("Oturum süresi sona ermiştir. Lütfen tekrar giriş yapınız.");

            JwtSecurityToken newAccessToken = await _tokenService.CreateToken(user, roles);

            string newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);
            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
            };
        }
    }
}
