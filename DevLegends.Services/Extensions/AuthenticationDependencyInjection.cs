using DevLegends.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
				options.DefaultAuthenticateScheme = CertificateAuthenticationDefaults.AuthenticationScheme;
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddCertificate()
			.AddJwtBearer(options =>
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
			})
			.AddGoogle(options =>
			{
				options.ClientId = "210778926623-0tu74486tptbf9ihb6cnqe979rkb8dub.apps.googleusercontent.com";
				options.ClientSecret = "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";
				options.Scope.Add("profile");
				options.SignInScheme = IdentityConstants.ExternalScheme;
			});


			return services;
		}

	}

}
