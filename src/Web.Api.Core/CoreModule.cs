using Autofac;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.UseCases;

namespace Web.Api.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CadastrarAlunoUseCase>().As<ICadastrarAlunoUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<CadastrarProfessorUseCase>().As<ICadastrarProfessorUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<CadastrarGradeUseCase>().As<ICadastrarGradeUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<MatricularAlunoUseCase>().As<IMatricularAlunoUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<DesmatricularAlunoUseCase>().As<IDesmatricularAlunoUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<ObterGradeInfoUseCase>().As<IObterGradeInfoUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<ObterProfessorInfoUseCase>().As<IObterProfessorInfoUseCase>().InstancePerLifetimeScope();
        }

    }
}
