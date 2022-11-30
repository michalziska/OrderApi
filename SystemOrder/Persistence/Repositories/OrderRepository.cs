using Microsoft.EntityFrameworkCore;
using SystemOrder.Domain.Models;
using SystemOrder.Domain.Repositories;
using SystemOrder.Persistence.Context;

namespace SystemOrder.Persistence.Repositories
{
	public class OrderRepository : BaseRepository, IOrderRepository
	{
		public OrderRepository(OrderDbContext context) : base(context) { }

		public async Task AddAsync(Order order)
		{
			await _context.Order.AddAsync(order);
		}

		public void Delete(Order order)
		{
			_context.Order.Remove(order);
		}

		public async Task<Order> FindByIdAsync(int id)
		{
			return _context.Order
				.Include(p => p.Products)
				.Where(p => p.OrderId == id)
				.AsNoTracking()
				.SingleOrDefault();
		}

		public async Task<IEnumerable<Order>> ListAsync()
		{
			return _context.Order
				.Include(p => p.Products)
				.AsNoTracking();
		}

		public async Task<IEnumerable<CountOfOrderByMonthsModel>> CountOfOrderByMonths()
		{
			var query = _context.Order
				.Include(o => o.Products).ToList() //Musime pridat metodu .ToList() na toto misto. Potom funguje Include s GroupBy.
				.GroupBy(p => p.DateOfCreation.Month, (month, orders) => new CountOfOrderByMonthsModel
				{
					Month = month,
					OrdersCount = orders.Select(p => p).Count(),
					Orders = orders.Select(order => order)
				})
				.OrderByDescending(order => order.OrdersCount);

			return query;
		}

		public void Update(Order order)
		{
			_context.Order.Update(order);
		}
	}
}
