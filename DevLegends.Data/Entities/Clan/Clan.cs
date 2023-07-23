namespace DevLegends.Data.Entities.Clan
{
	public class Clan : IEntityBase
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }

		public virtual required List<Party.Party> Parties { get; set; }
		public virtual required List<Player.Player> Members { get; set; }
		public virtual required List<Event> Events { get; set; }

	}
}
