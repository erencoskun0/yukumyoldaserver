using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using yukumyolda.Application.Features.Commands.AuthCommands;

namespace yukumyolda.Application.Features.Validators.AuthValidators
{
    public class UserLoginValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginValidator() { 
            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Email veya Telefon numarasından en az biri doldurulmalıdır.");
             
            When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("Geçerli bir email adresi giriniz.");
            });
             
            When(x => !string.IsNullOrEmpty(x.PhoneNumber), () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .Matches(@"^\+?[0-9]{10,15}$")
                    .WithMessage("Geçerli bir telefon numarası giriniz.");
            });

             
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4)
                .WithMessage("Parola en az 4 karakter olmalıdır.");
        }
    }
    }
 
