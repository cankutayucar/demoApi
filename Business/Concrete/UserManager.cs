using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.ObjectPool;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IFileService _fileService;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void add(AuthDto authDto)
        {
            var fileNme = _fileService.FileSave("./Content/img/", authDto.image);


            
            _userDal.Add(CreateUser(authDto,fileNme));
        }


        private User CreateUser(AuthDto authDto, string fileNme)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(authDto.Password, out passwordHash, out passwordSalt);
            User user = new User();
            user.Email = authDto.Email;
            user.Name = authDto.Name;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ImageUrl = fileNme;
            return user;
        }


        public List<User> getList()
        {
            return _userDal.GetAll();
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}
