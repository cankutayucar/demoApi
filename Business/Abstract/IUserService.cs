using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        void add(AuthDto authDto);
        List<User> getList();
        User GetByEmail(string email);
    }
}
