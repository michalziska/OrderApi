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
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

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
            //throw new Exception("Toto");

            var result = await _orderService.FindOrderAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        [HttpPost]
        [Route("AddOrder")]
        [ProducesResponseType(typeof(Order), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostOrderAsync([FromBody] SaveOrderResource resource, CancellationToken cancellationToken)
        {

            var product = _mapper.Map<SaveOrderResource, Order>(resource);
            var result = await _orderService.SaveOrderAsync(product, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        [HttpPut("UpdateOrder")]
        [ProducesResponseType(typeof(Order), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutOrderAsync(int id, [FromBody] SaveOrderResource resource)
        {
            var order = _mapper.Map<SaveOrderResource, Order>(resource);
            var result = await _orderService.UpdateOrderAsync(id, order);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        [HttpDelete("DeleteOrder")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

    }
}
