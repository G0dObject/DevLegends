namespace DevLegends.Data.Entities.Player
{
	public class PlayerClass : IEntityBase
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
	}
}
