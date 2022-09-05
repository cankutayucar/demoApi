using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.JWT;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IResult Register(AuthDto authDto);
        IDataResult<Token> Login(LoginAuthDto authDto);
    }
}
