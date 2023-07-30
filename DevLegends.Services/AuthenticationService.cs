using DevLegends.Data.Entities.User;
using DevLegends.DTO.Request.Authorization;
using DevLegends.DTO.Response;
using DevLegends.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DevLegends.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ITokenGeneratorService _tokenGenerator;

		public AuthenticationService(UserManager<User> userManager, ITokenGeneratorService tokenGenerator, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_tokenGenerator = tokenGenerator;
			_signInManager = signInManager;
		}

		private async Task<List<Claim>> GetUserClaims(User user)
		{
			IList<string> userRoles = await _userManager.GetRolesAsync(user);

			List<Claim> authClaims = new()
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
				};

			foreach (string? userRole in userRoles)
			{
				authClaims.Add(new Claim(ClaimTypes.Role, userRole));
			}

			return authClaims;
		}

		public async Task<AuthenticationResponse?> LoginAsync(LoginTransferObject model)
		{
			User? user = await _userManager.FindByNameAsync(model.Username);

			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				List<Claim> authClaims = await GetUserClaims(user);
				return new AuthenticationResponse(_tokenGenerator.GenerateToken(authClaims), StatusCodes.Status200OK);
			}
			return null;
		}

		public async Task<AuthenticationResponse> RegisterAsync(RegisterTransferObject model)
		{
			User userExists = await _userManager.FindByNameAsync(model.Username);

			if (userExists != null)
			{
				return new AuthenticationResponse(null, statuscode: StatusCodes.Status409Conflict);
			}

			User user = new(model.Username)
			{
				Email = model.Email,
				SecurityStamp = Guid.NewGuid().ToString(),
			};

			IdentityResult result =
				await _userManager.CreateAsync(user, model.Password);

			return !result.Succeeded
				? new AuthenticationResponse(null, statuscode: StatusCodes.Status400BadRequest)
				: new AuthenticationResponse(null, statuscode: StatusCodes.Status200OK);
		}

		public async Task<AuthenticationResponse?> ExternalLoginAsync(ExternalLoginInfo info)
		{
			SignInResult result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

			if (result.IsLockedOut)
			{
				return new AuthenticationResponse(StatusCodes.Status403Forbidden);
			}

			if (result.IsNotAllowed)
			{
				return new AuthenticationResponse(StatusCodes.Status403Forbidden);
			}

			if (result.RequiresTwoFactor)
			{
				return new AuthenticationResponse(StatusCodes.Status403Forbidden);
			}

			string? email = info.Principal.FindFirstValue(ClaimTypes.Email);

			if (result.Succeeded)
			{
				User? user = await _userManager.FindByEmailAsync(email);


				if (user == null)
				{
					return null;
				}
			}
			return null;
		}
	}
}
