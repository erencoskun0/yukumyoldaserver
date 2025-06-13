using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using yukumyolda.Application.Features.Commands.AuthCommands;
using yukumyolda.Application.Features.Results.AuthResults;
using yukumyolda.Application.Tokens;
using yukumyolda.Domain.Entities;

namespace yukumyolda.Application.Features.Handlers.AuthHandlers
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, UserLoginResults>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public UserLoginHandler(UserManager<User> userManager, IConfiguration configuration, ITokenService tokenService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<UserLoginResults> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            //User user = await _userManager.FindByEmailAsync(request.Email);
            User user = await _userManager.Users
                .FirstOrDefaultAsync(u =>
                 u.Email == request.Email || u.PhoneNumber == request.PhoneNumber);

            if (user is not null)
            {
                bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!checkPassword)
                {
                    throw new Exception("Kullanıcı bilgileri hatalı!");
                }
                else
                {
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    JwtSecurityToken token = await _tokenService.CreateToken(user, roles);

                    string refreshToken = _tokenService.GenerateRefreshToken();

                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int RefreshTokenValidityInDays);

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(RefreshTokenValidityInDays);

                    await _userManager.UpdateAsync(user);
                    await _userManager.UpdateSecurityStampAsync(user);
                    string _token = new JwtSecurityTokenHandler().WriteToken(token);

                    await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

                    return new UserLoginResults()
                    {
                        Token = _token,
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo

                    };
                }
            }
            else
            {
                throw new Exception("Kullanıcı bilgileri hatalı!");
            }
        }
    }
}
