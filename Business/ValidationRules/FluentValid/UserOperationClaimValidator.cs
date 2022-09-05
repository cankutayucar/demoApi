using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValid
{
    public class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(uop => uop.UserId).NotEmpty().WithMessage("yetki ataması için kullanıcı seçiniz");
            RuleFor(uop => uop.OperationClaimId).NotEmpty().WithMessage("yetki ataması için yetki seçiniz");
        }
    }
}
