using Microsoft.IdentityModel.Tokens;

namespace DevLegends.Core
{
	public class JwtSetting
	{
		public string? Key { get; set; }
		public string? Issuer { get; set; }
		public string? Audience { get; set; }
		public SymmetricSecurityKey? SecurityKey { get; set; }
	}
}
