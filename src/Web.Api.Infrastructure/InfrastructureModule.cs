using Autofac;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Web.Api.Core.Interfaces.Services;
using Web.Api.Infrastructure.Auth;
using Web.Api.Infrastructure.Data.EntityFramework.Repositories;
namespace Web.Api.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtFactory>().As<IJwtFactory>().SingleInstance();
            builder.RegisterType<UsuarioRepository>().As<IUsuarioRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GradeRepository>().As<IGradeRepository>().InstancePerLifetimeScope();
        }
    }
}
