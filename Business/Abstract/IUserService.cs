using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        void add(AuthDto authDto);
        IResult Update(User user);
        IResult ChangePassword(UserChangePasswordDto userChangePasswordDto);
        IResult Delete(User user);
        IDataResult<List<User>> getList();
        User GetByEmail(string email);
        IDataResult<User> GetById(int id);
        List<OperationClaim> UserOperationClaims(int userId);
    }
}
