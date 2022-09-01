using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        string Register(AuthDto authDto);
        string Login(LoginAuthDto authDto);
    }
}
