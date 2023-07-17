using Microsoft.AspNetCore.Identity;

namespace DevLegends.Data.Entities.User
{
	public class User : IdentityUser<int>
	{
		public User(string userName) : base(userName)
		{
		}
	}
}
