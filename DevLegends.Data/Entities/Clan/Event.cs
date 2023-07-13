namespace DevLegends.Data.Entities.Clan
{
	internal class Event : IEntityBase
	{
		public int Id { get; set; }
		public DateTime StartAt { get; set; }
		public DateTime EndAt { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
		public int SizeLimit { get; set; }
		public required List<Party.Party> Parties { get; set; }
	}
}
