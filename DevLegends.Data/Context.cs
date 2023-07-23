using DevLegends.Data.Entities.Clan;
using DevLegends.Data.Entities.Party;
using DevLegends.Data.Entities.Player;
using DevLegends.Data.Entities.Quest;
using DevLegends.Data.Entities.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevLegends.Data
{
	public class Context : IdentityDbContext<User, Role, int>, IContext
	{
		public DbSet<Player> Players { get; set; }
		public DbSet<PlayerClass> Classes { get; set; }
		public DbSet<Quest> Quests { get; set; }
		public DbSet<Party> Parties { get; set; }
		public DbSet<Clan> Clans { get; set; }
		public DbSet<Event> Events { get; set; }

		public Context(DbContextOptions dbContext) : base(dbContext)
		{
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
