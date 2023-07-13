using DevLegends.Core.Interfaces;
using DevLegends.Data.Entities.Clan;
using DevLegends.Data.Entities.Party;
using DevLegends.Data.Entities.Player;
using DevLegends.Data.Entities.Quests;
using DevLegends.Data.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevLegends.Data
{
	public class Context : IdentityDbContext<User, Role, int>, IContext
	{
		private DbSet<Player> Players { get; set; }
		private DbSet<PlayerClass> Classes { get; set; }
		private DbSet<Quest> Quests { get; set; }
		private DbSet<Party> Parties { get; set; }
		private DbSet<Clan> Clans { get; set; }
		private DbSet<Event> Events { get; set; }

		public Context()
		{
			_ = Database.EnsureDeleted();
			_ = Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			_ = optionsBuilder.UseSqlite("DataSource=DevLegends.db");
			_ = optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information, Microsoft.EntityFrameworkCore.Diagnostics.DbContextLoggerOptions.Level);
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}
