﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        void Register(AuthDto authDto);
        string Login(LoginAuthDto authDto);
    }
}
