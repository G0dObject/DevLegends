using DevLegends.Core.Interfaces;
using DevLegends.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevLegends.Services.Extensions
{
	public static class DbDependencyInjection
	{
		public static IServiceCollection AddDbDependency(this IServiceCollection services, IConfiguration configuration, bool IsDevelopment = false)
		{
			string? connectionString = configuration.GetConnectionString("SqLite");
			_ = services.AddDbContext<Context>(option =>
			{
				_ = option.UseSqlite(connectionString);
			});

			_ = services.AddScoped<IContext>(provider =>
			  provider.GetService<Context>() ?? new Context(new DbContextOptions<Context>()));

			return services;
		}
	}
}
