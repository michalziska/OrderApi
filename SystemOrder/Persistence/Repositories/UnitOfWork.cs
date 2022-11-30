using SystemOrder.Domain.Repositories;
using SystemOrder.Persistence.Context;

namespace SystemOrder.Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly OrderDbContext _context;

		public UnitOfWork(OrderDbContext context)
		{
			_context = context;
		}

		public Task CompleteAsync()
		{
			return _context.SaveChangesAsync();
		}
	}
}
