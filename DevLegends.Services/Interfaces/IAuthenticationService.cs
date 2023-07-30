using DevLegends.DTO.Request.Authorization;
using DevLegends.DTO.Response;
using Microsoft.AspNetCore.Identity;

namespace DevLegends.Services.Interfaces
{
	public interface IAuthenticationService : IService
	{
		Task<AuthenticationResponse> LoginAsync(LoginTransferObject login);
		Task<AuthenticationResponse> RegisterAsync(RegisterTransferObject register);
		Task<AuthenticationResponse> ExternalLoginAsync(ExternalLoginInfo info);
	}
}
