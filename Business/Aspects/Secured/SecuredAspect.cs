using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IOC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Aspects.Secured
{
    public class SecuredAspect : MethodInterception
    {
        private string[] _roles;
        private readonly IHttpContextAccessor _contextAccessor;

        public SecuredAspect()
        {
            _contextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        public SecuredAspect(string roles)
        {
            _roles = roles.Split(",");
            _contextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if(_roles != null)
            {
                var roleClaims = _contextAccessor.HttpContext.User.ClaimsRoles();
                foreach (var role in _roles)
                {
                    if (roleClaims.Contains(role))
                    {
                        return;
                    }
                }

                new Exception("işlem için yetkiniz bulunamadı");
            }
            var claims = _contextAccessor.HttpContext.User.Claims;
            if (claims.Count() > 0)
            {
                return;
            }
            new Exception("işlem için yetkiniz bulunamadı");
        }
    }
}
