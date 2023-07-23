using DevLegends.Data.Entities.Clan;
using DevLegends.Data.Entities.Party;
using DevLegends.Data.Entities.Player;
using DevLegends.Data.Entities.Quest;
using Microsoft.EntityFrameworkCore;

namespace DevLegends.Data
{
	public interface IContext
	{
		public DbSet<Player> Players { get; set; }
		public DbSet<PlayerClass> Classes { get; set; }
		public DbSet<Quest> Quests { get; set; }
		public DbSet<Party> Parties { get; set; }
		public DbSet<Clan> Clans { get; set; }
		public DbSet<Event> Events { get; set; }


		public int SaveChanges();
		public Task<int> SaveChangesAsync(CancellationToken token);

	}
}
