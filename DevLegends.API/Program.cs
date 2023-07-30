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
			_ = builder.Services.AddCors(options => options
			.AddPolicy("Development", (policy) => policy
			.WithOrigins("https://localhost:3000")
			.AllowAnyMethod()
			.AllowAnyHeader()));
			_ = builder.Services.AddAuthenticationDependency();
			_ = builder.Services.AddDbDependency(builder.Configuration);

			WebApplication app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				_ = app.UseSwagger();
				_ = app.UseSwaggerUI();
				_ = app.UseDeveloperExceptionPage();
			}

			_ = app.UseHttpsRedirection();
			_ = app.UseHttpLogging();
			_ = app.UseCors("Development");
			_ = app.MapControllers();
			_ = app.UseAuthentication();
			_ = app.UseAuthorization();

			app.Run();

		}
	}
}