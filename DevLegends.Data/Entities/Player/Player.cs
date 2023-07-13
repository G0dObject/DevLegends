namespace DevLegends.Data.Entities.Player
{
	internal class Player : IEntityBase
	{
		public int Id { get; set; }
		internal string Name { get; set; } = string.Empty;
		internal required PlayerClass Class { get; set; }
		internal DateTime CreateTime { get; set; }
	}
}