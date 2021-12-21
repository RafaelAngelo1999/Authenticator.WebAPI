using Authenticador.Infra.Data.Repositories.Base;
using Authenticator.Shared.Injecoes;
using Autofac;
using System.Reflection;

namespace Authenticador.Infra.CrossCutting.IoC
{
	public static class AutoFacExtension
	{
		public static void AddAutofacServiceProvider(this ContainerBuilder builder)
		{
			builder.RegistrarContexto();
			builder.RegistrarServices();
			builder.RegistrarRepositorio();
			builder.RegistrarAppServices();
			builder.RegistrarDependencias();
		}

		private static void RegistrarContexto(this ContainerBuilder builder)
		{
			builder.RegisterType<AppDbContext>().InstancePerLifetimeScope();
		}

		private static void RegistrarAppServices(this ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.Load("HP.Crm.AppService")).AsImplementedInterfaces().InstancePerLifetimeScope();
		}

		private static void RegistrarServices(this ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.Load("HP.Crm.Domain")).AsImplementedInterfaces().InstancePerLifetimeScope();
		}

		private static void RegistrarRepositorio(this ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(Assembly.Load("HP.Crm.Infra.Data")).AsImplementedInterfaces().InstancePerLifetimeScope();
		}

		private static void RegistrarDependencias(this ContainerBuilder builder)
		{
			builder.RegisterType<MultiStatusInjecao>().InstancePerLifetimeScope();
		}
	}
}