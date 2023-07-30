using DevLegends.Core.Exceptions;
using DevLegends.Core.Settings;
using DevLegends.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace DevLegends.Services
{
    public class ConfigurationService : IConfigurationService
	{
		private readonly IConfiguration _configuration;

		public ConfigurationService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public JwtSetting GetJwtSettings()
		{
			string key = _configuration["Jwt:Key"] ?? throw new SettingNotDefinedException("The Jwt Setting -> Key not defined");
			string? issuer = _configuration["Jwt:Issuer"] ?? throw new SettingNotDefinedException("The Jwt Setting -> Issuer not defined");
			string? audience = _configuration["Jwt:Audience"] ?? throw new SettingNotDefinedException("The Jwt Setting -> Audience not defined");

			SymmetricSecurityKey securityKey = new(SHA256.HashData(Encoding.UTF8.GetBytes(key ?? throw new SettingNotDefinedException("The Jwt Setting -> Key not defined"))));

			return new JwtSetting() { Key = key, Issuer = issuer, Audience = audience, SecurityKey = securityKey };
		}
	}
}
