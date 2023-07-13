using DevLegends.Core.Interfaces;
using DevLegends.Data;
using DevLegends.Services;


namespace DevLegends.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			_ = builder.Services.AddAuthentication();
			_ = builder.Services.AddAuthorization();
			_ = builder.Services.AddControllers();
			_ = builder.Services.AddEndpointsApiExplorer();
			_ = builder.Services.AddSwaggerGen();
			_ = builder.Services.AddScoped<IContext, Context>();
			_ = builder.Services.ServicesRegister();

			WebApplication app = builder.Build();

			app.Services.CreateScope().ServiceProvider.GetRequiredService<IContext>();
			app.Services.CreateScope().ServiceProvider.GetRequiredService<ITestService>().Do();
			if (app.Environment.IsDevelopment())
			{
				_ = app.UseSwagger();
				_ = app.UseSwaggerUI();
			}

			_ = app.UseHttpsRedirection();
			_ = app.UseAuthorization();
			_ = app.UseAuthentication();
			_ = app.MapControllers();
			app.Run();

		}
	}
}