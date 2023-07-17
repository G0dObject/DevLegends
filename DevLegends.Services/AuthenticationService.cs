using DevLegends.Data.Entities.User;
using DevLegends.DTO.Request.Authorization;
using DevLegends.DTO.Response;
using DevLegends.Services.DependencyInjection;
using DevLegends.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DevLegends.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<User> _userManager;
		private readonly ITokenGeneratorService _tokenGenerator;

		public AuthenticationService(UserManager<User> userManager, ITokenGeneratorService tokenGenerator)
		{
			_userManager = userManager;
			_tokenGenerator = tokenGenerator;
		}


		public async Task<AuthenticationResponse?> LoginAsync(LoginTransferObject model)
		{
			User? user = await _userManager.FindByNameAsync(model.Username);

			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
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
				return new AuthenticationResponse(_tokenGenerator.GenerateToken(authClaims), 200);


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

			IdentityResult result = await _userManager.CreateAsync(user, model.Password);

			return !result.Succeeded
				? new AuthenticationResponse(null, statuscode: StatusCodes.Status400BadRequest)
				: new AuthenticationResponse(null, statuscode: StatusCodes.Status200OK);
		}


	}
}
