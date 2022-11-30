using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SystemOrder.Domain.Models;
using SystemOrder.Domain.Services;
using SystemOrder.Resources;

namespace SystemOrder.Controllers
{
	public class OrderController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrderController(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		//[HttpGet]
		//[Route("FindProduct")]
		//public async Task<IActionResult> FindProductById(int id)
		//{
		//	var result = await _orderService.FindProductAsync(id);

		//	if (!result.Success)
		//	{
		//		return BadRequest(result);
		//	}

		//	return Ok(result.Resource);
		//}


		//[HttpPost]
		//[Route("AddProduct")]
		//[ProducesResponseType(typeof(Product), 201)]
		//[ProducesResponseType(typeof(Product), 400)]
		//public async Task<IActionResult> PostProductAsync([FromForm] SaveProductResource resource)
		//{

		//	var product = _mapper.Map<SaveProductResource, Product>(resource);
		//	var result = await _orderService.SaveProductAsync(product);

		//	if (!result.Success)
		//	{
		//		return BadRequest(result);
		//	}

		//	return Ok(result.Resource);
		//}

		//[HttpPut("UpdateProduct")]
		//[ProducesResponseType(typeof(Product), 201)]
		//[ProducesResponseType(typeof(Product), 400)]
		//public async Task<IActionResult> PutProductAsync(int id, [FromForm] SaveProductResource resource)
		//{
		//	var product = _mapper.Map<SaveProductResource, Product>(resource);
		//	var result = await _orderService.UpdateProductAsync(id, product);

		//	if (!result.Success)
		//	{
		//		return BadRequest(result);
		//	}

		//	return Ok(result.Resource);
		//}

		//[HttpDelete("DeleteProduct")]
		//[ProducesResponseType(typeof(Product), 200)]
		//[ProducesResponseType(typeof(Product), 400)]
		//public async Task<IActionResult> DeleteProductAsync(int id)
		//{
		//	var result = await _orderService.DeleteProductAsync(id);

		//	if (!result.Success)
		//	{
		//		return BadRequest(result);
		//	}

		//	return Ok(result.Resource);
		//}

		//[HttpGet("ListAllProducts")]
		//[ProducesResponseType(typeof(IEnumerable<Product>), 200)]
		//public async Task<IEnumerable<Product>> ListAllProducts()
		//{
		//	var result = await _orderService.ListProductsAsync();

		//	if (result.Count() == 0)
		//	{
		//		return new List<Product>() { };
		//	}

		//	return result;
		//}

		[HttpGet("CountOfOrderByMonths")]
		[ProducesResponseType(typeof(IEnumerable<Order>), 200)]
		public async Task<IEnumerable<CountOfOrderByMonthsModel>> CountOfOrderByMonths()
		{
			var result = await _orderService.CountOfOrderByMonths();

			if (result.Count() == 0)
			{
				return new List<CountOfOrderByMonthsModel>() { };
			}

			return result;
		}

		[HttpGet("ListAllOrders")]
		[ProducesResponseType(typeof(IEnumerable<Order>), 200)]
		public async Task<IEnumerable<Order>> ListAllOrders()
		{
			var result = await _orderService.ListOrdersAsync();

			if (result.Count() == 0)
			{
				return new List<Order>() { };
			}

			return result;
		}

		[HttpGet]
		[Route("FindOrder")]
		public async Task<IActionResult> FindOrderById(int id)
		{
			var result = await _orderService.FindOrderAsync(id);

			if (!result.Success)
			{
				return BadRequest(result);
			}

			return Ok(result.Resource);
		}

		[HttpPost]
		[Route("AddOrder")]
		[ProducesResponseType(typeof(Order), 201)]
		[ProducesResponseType(typeof(Order), 400)]
		public async Task<IActionResult> PostOrderAsync([FromBody] SaveOrderResource resource)
		{

			var product = _mapper.Map<SaveOrderResource, Order>(resource);
			var result = await _orderService.SaveOrderAsync(product);

			if (!result.Success)
			{
				return BadRequest(result);
			}

			return Ok(result.Resource);
		}

		[HttpPut("UpdateOrder")]
		[ProducesResponseType(typeof(Order), 201)]
		[ProducesResponseType(typeof(Order), 400)]
		public async Task<IActionResult> PutOrderAsync(int id, [FromBody] SaveOrderResource resource)
		{
			var order = _mapper.Map<SaveOrderResource, Order>(resource);
			var result = await _orderService.UpdateOrderAsync(id, order);

			if (!result.Success)
			{
				return BadRequest(result);
			}

			return Ok(result.Resource);
		}

		[HttpDelete("DeleteOrder")]
		[ProducesResponseType(typeof(Product), 200)]
		[ProducesResponseType(typeof(Product), 400)]
		public async Task<IActionResult> DeleteOrderAsync(int id)
		{
			var result = await _orderService.DeleteOrderAsync(id);

			if (!result.Success)
			{
				return BadRequest(result);
			}

			return Ok(result.Resource);
		}

	}
}
