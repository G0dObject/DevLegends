using DevLegends.Data.Entities.User;
using DevLegends.DTO.Request.Authorization;
using DevLegends.DTO.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLegends.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly Services.Interfaces.IAuthenticationService _authentication;
		private readonly SignInManager<User> _manager;

		public AuthenticationController(Services.Interfaces.IAuthenticationService authentication, SignInManager<User> manager)
		{
			_manager = manager;
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


		[Route("/signin-google")]
		public IActionResult Signin(string returnUrl)
		{
			return new ChallengeResult(
				GoogleDefaults.AuthenticationScheme,
				new AuthenticationProperties
				{
					RedirectUri = Url.Action(nameof(GoogleCallback), new { returnUrl })
				});
		}

		[Route("/signin-callback")]
		public int GoogleCallback(string returnUrl)
		{
			return 1;

		}
	}
}



