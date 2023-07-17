namespace DevLegends.Data.Entities.Quest
{
	internal class Quest : IEntityBase
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
	}
}
