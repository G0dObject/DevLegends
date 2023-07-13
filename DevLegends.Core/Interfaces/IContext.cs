namespace DevLegends.Core.Interfaces
{
	public interface IContext
	{
		public int SaveChanges();
		public Task<int> SaveChangesAsync(CancellationToken token);

	}
}
