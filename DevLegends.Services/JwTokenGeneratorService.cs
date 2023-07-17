using DevLegends.Core;
using DevLegends.Services.DependencyInjection;
using DevLegends.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DevLegends.Services
{
	internal class JwTokenGenerator : ITokenGeneratorService
	{
		private readonly IConfigurationService _configuration;

		public JwTokenGenerator(IConfigurationService configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(List<Claim> claims)
		{
			JwtSetting setting = _configuration.GetJwtSettings();

			SigningCredentials credentials = new(setting.SecurityKey, SecurityAlgorithms.HmacSha256);

			JwtSecurityToken token = new(
				setting.Issuer,
				setting.Audience,
				claims: claims,
				expires: DateTime.Now.AddDays(7),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
