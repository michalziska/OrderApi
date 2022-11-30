using SystemOrder.Persistence.Context;

namespace SystemOrder.Persistence.Repositories
{
	public abstract class BaseRepository
	{
		protected readonly OrderDbContext _context;

		public BaseRepository(OrderDbContext context)
		{
			_context = context;
		}
	}
}
