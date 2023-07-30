using System.Security.Claims;

namespace DevLegends.Services.Interfaces
{
	public interface ITokenGeneratorService : IService
	{
		string GenerateToken(List<Claim> claims);
	}
}
