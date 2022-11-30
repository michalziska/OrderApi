using SystemOrder.Domain.Models;

namespace SystemOrder.Domain.Repositories
{
	public interface IOrderRepository
	{
		Task<IEnumerable<Order>> ListAsync();
		Task AddAsync(Order order);
		Task<Order> FindByIdAsync(int id);
		void Update(Order order);
		void Delete(Order order);

		Task<IEnumerable<CountOfOrderByMonthsModel>> CountOfOrderByMonths();
	}
}
