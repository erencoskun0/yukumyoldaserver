using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using yukumyolda.Application.Features.Commands.AuthCommands;
using yukumyolda.Application.Features.Results.AuthResults;
using yukumyolda.Domain.Entities;
using yukumyolda.Persistence;

namespace yukumyolda.Application.Features.Handlers.AuthHandlers
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly YukumYoldaContext _context;

        public UserRegisterHandler(UserManager<User> userManager, RoleManager<Role> roleManager, YukumYoldaContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<Unit> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber))
                throw new Exception("Bu numara zaten kullanımda.");

            if (await _userManager.FindByEmailAsync(request.Email) is not null)
                throw new Exception("Bu E-mail'de bir kullanıcı zaten var");

            var user = new User
            {
                Name = request.Name,
                Surname = request.SurName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = Guid.NewGuid().ToString()
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(" - ", result.Errors.Select(e => e.Description));
                throw new Exception($"Kullanıcı oluşturulamadı: {errors}");
            }

            var selectedRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (selectedRole == null)
                throw new Exception("Belirtilen roleId ile bir rol bulunamadı.");

            var addToRoleResult = await _userManager.AddToRoleAsync(user, selectedRole.Name);
            if (!addToRoleResult.Succeeded)
            {
                var errors = string.Join(" | ", addToRoleResult.Errors.Select(e => e.Description));
                throw new Exception($"Rol ataması başarısız oldu: {errors}");
            }

            return Unit.Value;
        }
    }
}
