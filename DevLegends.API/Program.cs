using DevLegends.Services.DependencyInjection;
using DevLegends.Services.Extensions;

namespace DevLegends.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			_ = builder.Services.AddControllers();
			_ = builder.Services.AddEndpointsApiExplorer();
			_ = builder.Services.AddSwaggerGen();
			_ = builder.Services.ServicesRegister();
			_ = builder.Services.AddIdentityDependency();
			_ = builder.Services.AddAuthenticationDependency();

			WebApplication app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				_ = app.UseSwagger();
				_ = app.UseSwaggerUI();
				_ = app.UseDeveloperExceptionPage();
			}

			_ = app.UseHealthChecks("/alive");
			_ = app.UseHttpsRedirection();
			_ = app.UseHttpLogging();
			_ = app.UseAuthentication();
			_ = app.UseAuthorization();


			app.Run();

		}
	}
}