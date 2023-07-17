using DevLegends.Core;

namespace DevLegends.Services.Interfaces
{
    public interface IConfigurationService : IService
	{
		public JwtSetting GetJwtSettings();
	}
}
