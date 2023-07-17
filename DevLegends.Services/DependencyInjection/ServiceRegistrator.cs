using DevLegends.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Reflection;

namespace DevLegends.Services.DependencyInjection
{
	public static class ServiceRegistrator
	{
		public static IServiceCollection ServicesRegister(this IServiceCollection services)
		{
			Type baseInterface = typeof(IService);

			List<Type> types = Assembly.GetExecutingAssembly().GetTypes().ToList();

			List<Type> classes = types.Where(x => baseInterface.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).ToList();

			List<Type> interfaces = types.Where(x => baseInterface.IsAssignableFrom(x) && x.IsInterface && x != baseInterface).ToList();

			foreach (Type inter in interfaces)
			{
				classes.Where(inter.IsAssignableFrom).ToList().ForEach(type => services.AddScoped(inter, type));
			}

			return services;
		}
	}
}