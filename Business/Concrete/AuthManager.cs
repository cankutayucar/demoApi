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
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.Abstract.IResult;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        public AuthManager(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }


        [ValidationAspects(typeof(UserValidator))]
        public IResult Register(AuthDto authDto)
        {
            IResult result = BusinessRules.Run(
                CheckIfEmailExist(authDto.Email),
                CheckImageExtensionsAllow(authDto.image.FileName),
                CheckImageSizeIsLessThanOneMb(authDto.image.Length)
            );
            if (!result.Success)
            {
                return result;
            }
            _userService.add(authDto);
            return new SuccessResult("kulanıcı kaydı başarıyla tamamlandı");
        }

        public IDataResult<Token> Login(LoginAuthDto authDto)
        {
            User user = _userService.GetByEmail(authDto.Email);
            var verifyPassword =
                HashingHelper.VerifyPasswordHash(authDto.Password, user.PasswordHash, user.PasswordSalt);
            if (verifyPassword)
            {
                var token = _tokenHandler.createToken(user, _userService.UserOperationClaims(user.Id));
                return new SuccessDataResult<Token>(token);
            }
            return new ErrorDataResult<Token>("Kullanıcı mail veya şifre hatalı");
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

        private IResult CheckImageSizeIsLessThanOneMb(long imgSize)
        {
            decimal size = Convert.ToDecimal(imgSize * 0.000001);
            if (size > 1)
                return new ErrorResult("yüklediğiniz resim boyutu en fazla 1mb olmalıdır");
            return new SuccessResult();
        }

        private IResult CheckImageExtensionsAllow(string ImageName)
        {
            string fileName = ImageName;
            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> allowFileExtensions = new List<string>()
            {
                ".jpg", ".jpeg", ".gif", ".png"
            };
            if (!allowFileExtensions.Contains(extension))
            {
                return new ErrorResult("eklediğiniz resim türü geçersiz");
            }
            return new SuccessResult();
        }
    }
}
