namespace DevLegends.Data.Entities.Player
{
	public class Player : IEntityBase
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public User.User? User { get; set; }
		public PlayerClass? Class { get; set; }
		public DateTime CreateTime { get; set; }
	}
}