using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValid;
using Core.Aspects.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.Messages;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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

        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        public void add(AuthDto authDto)
        {
            var fileNme = _fileService.FileSave("./Content/img/", authDto.image);


            
            _userDal.Add(CreateUser(authDto,fileNme));
        }

        [ValidationAspects(typeof(OtherUserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Message.User.Update());
        }

        public IResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var user = _userDal.Get(u => u.Id == userChangePasswordDto.UserId);
            var checkPassword = HashingHelper.VerifyPasswordHash(userChangePasswordDto.OldPassword, user.PasswordHash,
                user.PasswordSalt);
            if (!checkPassword)
            {
                return new ErrorResult(Message.User.WrongPassword());
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword,out passwordHash,out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDal.Update(user);
            return new SuccessResult(Message.User.ChangedPassword());
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Message.User.Delete());
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }

        public List<OperationClaim> UserOperationClaims(int userId)
        {
            return _userDal.UserOperationClaims(userId);
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

        public IDataResult<List<User>> getList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}
