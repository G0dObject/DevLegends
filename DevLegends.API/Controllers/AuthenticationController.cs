using DevLegends.DTO.Request.Authorization;
using DevLegends.DTO.Response;
using DevLegends.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevLegends.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthenticationService _authentication;

		public AuthenticationController(IAuthenticationService authentication)
		{
			_authentication = authentication;
		}

		[HttpPost]
		[Route("Login")]
		public async Task<AuthenticationResponse> Login(LoginTransferObject model)
		{
			return await _authentication.LoginAsync(model);
		}

		[HttpPost]
		[Route("Register")]
		public async Task<AuthenticationResponse> RegisterAsync(RegisterTransferObject model)
		{
			return await _authentication.RegisterAsync(model);
		}

		[HttpGet]
		[Route("AuthTest")]
		[Authorize]
		public string Get()
		{
			return $"You authorized {User.Identity.Name}";
		}
	}
}

