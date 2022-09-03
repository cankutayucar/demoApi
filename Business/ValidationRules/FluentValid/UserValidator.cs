using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValid
{
    public class UserValidator : AbstractValidator<AuthDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(u => u.Email).NotEmpty().WithMessage("email adı boş geçilemez");
            RuleFor(u => u.image).NotEmpty().WithMessage("Kullanıcı resmi adı boş geçilemez");
            RuleFor(u => u.Password).NotEmpty().WithMessage("şifre adı boş geçilemez");
            RuleFor(u => u.Password).MinimumLength(6).WithMessage("şifre en az 6 karakter olmalıdır");
        }
    }
}
