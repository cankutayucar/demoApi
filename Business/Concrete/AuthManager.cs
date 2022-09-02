using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValid;
using Core.Aspects.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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

        [ValidationAspects(typeof(UserValidator))]
        public IResult Register(AuthDto authDto)
        {

            IResult result = BusinessRules.Run(CheckIfEmailExist(authDto.Email), CheckImageSizeIsLessThanOneMb(2));
            if (!result.Success)
            {
                return result;
            }
            _userService.add(authDto);
            return new SuccessResult("kulanıcı kaydı başarıyla tamamlandı");
        }

        public string Login(LoginAuthDto authDto)
        {
            User user = _userService.GetByEmail(authDto.Email);
            var verifyPassword =
                HashingHelper.VerifyPasswordHash(authDto.Password, user.PasswordHash, user.PasswordSalt);
            return verifyPassword ? "kullacısı girişi başarılı" : "kullanıcı bilgileri hatalı";
        }



        private IResult CheckIfEmailExist(string email)
        {
            var user = _userService.GetByEmail(email);
            if (user != null)
            {
                return new ErrorResult("Bu mail adresi daha önce kullanılmış");
            }
            return new SuccessResult();
        }

        private IResult CheckImageSizeIsLessThanOneMb(int imgSize)
        {
            if (imgSize > 1)
                return new ErrorResult("yüklediğiniz resim boyutu en fazla 1mb olmalıdır");
            return new SuccessResult();
        }

    }
}
