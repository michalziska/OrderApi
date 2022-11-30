using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SystemOrder.Domain.Models;
using SystemOrder.Domain.Services;
using SystemOrder.Resources;

namespace SystemOrder.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public ProductController(IOrderService orderService, IMapper mapper)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		[HttpGet]
		[Route("FindProduct")]
		public async Task<IActionResult> FindProductById(int id)
		{
			var result = await _orderService.FindProductAsync(id);

			if (!result.Success)
			{
				return BadRequest(result);
			}

			return Ok(result.Resource);
		}


		[HttpPost]
		[Route("AddProduct")]
		[ProducesResponseType(typeof(Product), 201)]
		[ProducesResponseType(typeof(Product), 400)]
		public async Task<IActionResult> PostProductAsync([FromForm] SaveProductResource resource)
		{

			var product = _mapper.Map<SaveProductResource, Product>(resource);
			var result = await _orderService.SaveProductAsync(product);

			if (!result.Success)
			{
				return BadRequest(result);
			}

			return Ok(result.Resource);
		}

		[HttpPut("UpdateProduct")]
		[ProducesResponseType(typeof(Product), 201)]
		[ProducesResponseType(typeof(Product), 400)]
		public async Task<IActionResult> PutProductAsync(int id, [FromForm] SaveProductResource resource)
		{
			var product = _mapper.Map<SaveProductResource, Product>(resource);
			var result = await _orderService.UpdateProductAsync(id, product);

			if (!result.Success)
			{
				return BadRequest(result);
			}

			return Ok(result.Resource);
		}

		[HttpDelete("DeleteProduct")]
		[ProducesResponseType(typeof(Product), 200)]
		[ProducesResponseType(typeof(Product), 400)]
		public async Task<IActionResult> DeleteProductAsync(int id)
		{
			var result = await _orderService.DeleteProductAsync(id);

			if (!result.Success)
			{
				return BadRequest(result);
			}

			return Ok(result.Resource);
		}

		[HttpGet("ListAllProducts")]
		[ProducesResponseType(typeof(IEnumerable<Product>), 200)]
		public async Task<IEnumerable<Product>> ListAllProducts()
		{
			var result = await _orderService.ListProductsAsync();

			if (result.Count() == 0)
			{
				return new List<Product>() { };
			}

			return result;
		}

		[HttpGet("TheMostSellProductsByCategories")]
		[ProducesResponseType(typeof(IEnumerable<Product>), 200)]
		public async Task<IEnumerable<TopProductsByCategoriesModel>> TheMostSellProductsByCategories()
		{
			var result = await _orderService.TheMostSellProductsByCategories();

			if (result.Count() == 0)
			{
				return new List<TopProductsByCategoriesModel>() { };
			}

			return result;
		}
	}
}
