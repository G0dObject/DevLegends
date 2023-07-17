using DevLegends.Services.Interfaces;
using System.Security.Claims;

namespace DevLegends.Services.DependencyInjection
{
	public interface ITokenGeneratorService : IService
	{
		string GenerateToken(List<Claim> claims);
	}
}
