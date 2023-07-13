namespace DevLegends.Data.Entities.Party
{
	internal class Party : IEntityBase
	{
		public int Id { get; set; }
		public List<Player.Player>? Players { get; set; }

	}
}
