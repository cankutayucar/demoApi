using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Dal;
using Core.Dal.EntityFramework;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfEntityRepository<,>)).As(typeof(IEntityRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfUserDal>().As<IUserDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().InstancePerLifetimeScope();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().InstancePerLifetimeScope();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>().InstancePerLifetimeScope();

            #region aspect di
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
                new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance(); 
            #endregion
        }
    }
}
