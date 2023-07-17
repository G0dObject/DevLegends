using DevLegends.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DevLegends.Services.Extensions
{
	public static class AuthenticationDependencyInjection
	{
		public static IServiceCollection AddAuthenticationDependency(this IServiceCollection services)
		{
			Core.JwtSetting setting = services.BuildServiceProvider().GetRequiredService<IConfigurationService>().GetJwtSettings();

			_ = services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidAudience = setting.Audience,
					ValidIssuer = setting.Issuer,
					IssuerSigningKey = setting.SecurityKey,
					ValidateLifetime = false
				};
			});
			services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();
			return services;
		}

	}

}
