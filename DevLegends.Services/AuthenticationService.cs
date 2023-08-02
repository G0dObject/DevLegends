using DevLegends.Data.Entities.User;
using DevLegends.DTO.Request.Authorization;
using DevLegends.DTO.Response;
using DevLegends.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RussianTransliteration;
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
			SignInResult loginResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

			if (loginResult.IsLockedOut || loginResult.IsNotAllowed || loginResult.RequiresTwoFactor)
			{
				return new AuthenticationResponse(StatusCodes.Status403Forbidden);
			}

			string? email = info.Principal.FindFirstValue(ClaimTypes.Email);
			string? name = info.Principal.FindFirstValue(ClaimTypes.Name);

			User user = NormalizateUser(name, email);

			// Try to find an existing user based on email or name
			User? existingUser = null;
			if (!string.IsNullOrEmpty(email))
			{
				existingUser = await _userManager.FindByEmailAsync(email);
			}
			else if (!string.IsNullOrEmpty(name))
			{
				existingUser = await _userManager.FindByNameAsync(name);
			}

			IdentityResult result;
			if (existingUser != null)
			{
				// If the user exists, add the login info and sign them in
				result = await _userManager.AddLoginAsync(existingUser, info);
				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(existingUser, isPersistent: false);
				}
			}
			else
			{
				// If the user doesn't exist, create a new user and add the login info
				result = await _userManager.CreateAsync(user);
				if (result.Succeeded)
				{
					result = await _userManager.AddLoginAsync(user, info);
					if (result.Succeeded)
					{
						await Console.Out.WriteLineAsync("authorize");
						//TODO: Send an email for email confirmation and add a default role as in the Register action
						await _signInManager.SignInAsync(user, isPersistent: true);
					}
				}
			}
			List<Claim> authClaims = await GetUserClaims(user);
			return new AuthenticationResponse(_tokenGenerator.GenerateToken(authClaims), StatusCodes.Status200OK);

		}

		private User NormalizateUser(string username, string? email)
		{
			string name = RussianTransliterator.GetTransliteration(username.Replace(" ", ""));
			User user = new(name);
			if (email is not null)
			{
				user.Email = email;
			}

			user.SecurityStamp = Guid.NewGuid().ToString();
			return user;

		}


	}
}
