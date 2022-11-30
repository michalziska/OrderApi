namespace SystemOrder.Domain.Repositories
{
	public interface IUnitOfWork
	{
		Task CompleteAsync();
	}
}
