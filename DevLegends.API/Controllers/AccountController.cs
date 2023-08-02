using DevLegends.Data.Entities.User;
using DevLegends.DTO.Request.Authorization;
using DevLegends.DTO.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevLegends.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly Services.Interfaces.IAuthenticationService _authentication;
		private readonly SignInManager<User> _signInManager;

		public AccountController(Services.Interfaces.IAuthenticationService authentication, SignInManager<User> signInManage)
		{
			_authentication = authentication;
			_signInManager = signInManage;
		}

		[HttpPost]
		[Route("Login")]
		public async Task<AuthenticationResponse> LoginAsync(LoginTransferObject model)
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
		public IActionResult Get()
		{
			return Ok();
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("ExternalAuth")]
		public IActionResult ExternalLogin(string provider)
		{
			try
			{
				AuthenticationProperties properties =
					_signInManager.ConfigureExternalAuthenticationProperties(provider, Url.Action(nameof(ExternalLoginCallback)));
				return Challenge(properties, provider);
			}
			catch (Exception e)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, e.ToString());
			}
		}
		[HttpGet]
		[AllowAnonymous]
		[Route("ExternalAuthCallback")]
		public async Task<IActionResult?> ExternalLoginCallback()
		{
			ExternalLoginInfo? user = await _signInManager.GetExternalLoginInfoAsync();
			if (user == null)
			{
				return RedirectToAction(nameof(ExternalLogin));
			}

			AuthenticationResponse result = await _authentication.ExternalLoginAsync(user);
			return Redirect($"https://localhost:3000/token/{result.Token}");
		}
	}
}


