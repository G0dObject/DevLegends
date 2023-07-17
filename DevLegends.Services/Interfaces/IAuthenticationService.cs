using DevLegends.DTO.Request.Authorization;
using DevLegends.DTO.Response;

namespace DevLegends.Services.Interfaces
{
	public interface IAuthenticationService : IService
	{
		Task<AuthenticationResponse> LoginAsync(LoginTransferObject login);
		Task<AuthenticationResponse> RegisterAsync(RegisterTransferObject register);
	}
}
