using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SystemOrder.Attributes;
using SystemOrder.Domain.Models;
using SystemOrder.Domain.Services;
using SystemOrder.Extensions;
using SystemOrder.Resources;

namespace SystemOrder.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public ProductController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("FindProduct")]
        public async Task<IActionResult> FindProductById(int id)
        {
            var result = await _orderService.FindProductAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }


        [HttpPost]
        [Route("AddProduct")]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostProductAsync([FromForm] SaveProductResource resource, CancellationToken cancellationToken)
        {
            //var attrNameValidation = ValidationHelper.GetValidator<SaveProductResource>(nameof(resource.Name));
            //var attrDescValidation = ValidationHelper.GetValidator<SaveProductResource>(nameof(resource.Description));


            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _orderService.SaveProductAsync(product, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Created($"products/{result.Resource.ProductId}", result.Resource);
        }

        [HttpPut("UpdateProduct")]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutProductAsync(int id, [FromForm] SaveProductResource resource)
        {
            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _orderService.UpdateProductAsync(id, product);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        [HttpDelete("DeleteProduct")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var result = await _orderService.DeleteProductAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }

        [HttpGet("ListAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async IAsyncEnumerable<Product> ListAllProducts([FromQuery] ProductResourceParameters productResourceParameters)
        {
            var result = await _orderService.ListProductsAsync(productResourceParameters);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                currentPage = result.CurrentPage,
                totalPages = result.TotalPages,
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            if (result.Count() == 0)
            {
                yield return new Product { };
            }

            foreach (var product in result)
            {
                yield return product;
            }
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

        //Rozpracovane..
        [HttpGet("ListAllProductsCsv")]
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        public async Task<IActionResult> ListAllProductsCsv()
        {
            var result = await _orderService.ListProductsAsync(new ProductResourceParameters());
            var res = result.ToCsv(";").SelectMany(s => Encoding.UTF8.GetBytes(s)).ToArray();

            //if (result.Count() == 0)
            //{
            //	return BadRequest(new ErrorResource("An error occurred when processing ListAllProductsCsv"));
            //}

            //MemoryStream stream = new MemoryStream();
            //StreamWriter writer = new StreamWriter(stream);
            //writer.Write(result.ToCsv(";"));
            //writer.Flush();
            //stream.Position = 0;

            //var http = new HttpResponseMessage(HttpStatusCode.OK);
            //http.Content = new StreamContent(stream);
            //http.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            //http.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.csv" };
            //return http;

            return File(res, "text/csv;charset=utf-8", "export.csv");
        }
    }
}
