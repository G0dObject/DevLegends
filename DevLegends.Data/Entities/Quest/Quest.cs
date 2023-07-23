namespace DevLegends.Data.Entities.Quest
{
	public class Quest : IEntityBase
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Description { get; set; }
	}
}
