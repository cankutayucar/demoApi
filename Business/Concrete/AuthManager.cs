using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValid;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Hashing;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public string Register(AuthDto authDto)
        {
            UserValidator validator = new UserValidator();
            ValidationTool.Validate(validator,authDto);

            _userService.add(authDto);
            return "";
        }

        public string Login(LoginAuthDto authDto)
        {
            User user = _userService.GetByEmail(authDto.Email);
            var verifyPassword =
                HashingHelper.VerifyPasswordHash(authDto.Password, user.PasswordHash, user.PasswordSalt);
            return verifyPassword ? "kullacısı girişi başarılı" : "kullanıcı bilgileri hatalı";
        }
    }
}
