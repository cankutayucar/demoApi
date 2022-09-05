using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValid
{
    public class OtherUserValidator : AbstractValidator<User>
    {
        public OtherUserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(u => u.Email).NotEmpty().WithMessage("email adı boş geçilemez");
            RuleFor(u => u.ImageUrl).NotEmpty().WithMessage("Kullanıcı resmi adı boş geçilemez");
        }
    }
}
