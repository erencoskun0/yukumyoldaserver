using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using yukumyolda.Application.Features.Commands.AuthCommands;

namespace yukumyolda.Application.Features.Validators.AuthValidators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad boş olamaz.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.")
                .Matches(@"^[a-zA-ZğüşöçİĞÜŞÖÇ\s-]+$").WithMessage("Ad yalnızca harf, boşluk ve tire (-) içerebilir.");

            RuleFor(x => x.SurName)
                .NotEmpty().WithMessage("Soyad boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.")
                .Matches(@"^[a-zA-ZğüşöçİĞÜŞÖÇ\s-]+$").WithMessage("Soyad yalnızca harf, boşluk ve tire (-) içerebilir.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz. (örn: +905xxxxxxxxx)");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .MaximumLength(60).WithMessage("E-posta en fazla 60 karakter olabilir.")
                .MinimumLength(5).WithMessage("E-posta en az 5 karakter olmalıdır.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Rol bilgisi seçilmelidir.")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir rol ID girilmelidir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MinimumLength(4).WithMessage("Parola en az 4 karakter olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Parola tekrarı boş olamaz.")
                .Equal(x => x.Password).WithMessage("Parola ve Parola Tekrarı eşleşmiyor!");
        }
    }
}
