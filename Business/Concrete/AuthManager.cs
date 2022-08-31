using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
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

        public void Register(AuthDto authDto)
        {
            _userService.add(authDto);
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
