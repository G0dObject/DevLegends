using DevLegends.Core.Settings;

namespace DevLegends.Services.Interfaces
{
    public interface IConfigurationService : IService
	{
		public JwtSetting GetJwtSettings();
	}
}
