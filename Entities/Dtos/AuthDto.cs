using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Entities.Dtos
{
    public class AuthDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public IFormFile image { get; set; }
    }
}
