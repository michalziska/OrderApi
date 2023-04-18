using AutoMapper;
using Moq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemOrder.Controllers;
using SystemOrder.Domain.Models;
using SystemOrder.Domain.Services;
using SystemOrder.Domain.Services.Communication;
using Xunit;

namespace UnitTestProject
{
    public class UnitTestController
    {
        private readonly Mock<IOrderService> orderService;
        private readonly IMapper _mapper;

        public UnitTestController()
        {
            orderService = new Mock<IOrderService>();
        }

        [Fact]
        public async Task GetOrderList()
        {
            //arrange
            var orderList = GetOrder();
            orderService.Setup(x => x.ListOrdersAsync()).ReturnsAsync(orderList);
            var orderController = new OrderController(orderService.Object, _mapper);

            //act
            var orderResult = await orderController.ListAllOrders();

            //assert
            Assert.NotNull(orderResult);
            Assert.Equal(GetOrder().Count(), orderResult.Count());
        }

        [Fact]
        public async Task GetOrderById()
        {
            //arrange
            var orderList = GetOrder();
            orderService.Setup(x => x.FindOrderAsync(1)).ReturnsAsync(new OrderResponse(orderList.First()));
            var orderController = new OrderController(orderService.Object, _mapper);

            //act
            var orderResult = await orderController.FindOrderById(1);

            //assert
            Assert.NotNull(orderResult);
        }

        [Fact]
        public async Task GetProductList()
        {
            //arrange
            var productList = GetProducts();
            //orderService.Setup(x => x.ListProductsAsync()).ReturnsAsync(productList);
            var productController = new ProductController(orderService.Object, _mapper);

            //act
            var products = new List<Product>();
            await foreach (var product in productController.ListAllProducts(new SystemOrder.Resources.ProductResourceParameters()))
            {
                products.Add(product);
            }

            //assert
            Assert.NotNull(products);
        }

        private IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();

            products.Add(
            new Product
            {
                ProductId = 1,
                Name = "Mango",
                Price = 5,
                Category = ECategory.Grocery,
                Orders = GetOrder().ToList()
            });

            return products;
        }

        private IEnumerable<Order> GetOrder()
        {
            var orders = new List<Order>();

            orders.Add(
                new Order
                {
                    OrderId = 1,
                    DateOfCreation = DateTime.Now,
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductId = 1,
                            Name= "Apple watch SE",
                            Category = ECategory.HighEnd,
                            Price= 10,
                        },
                        new Product
                        {
                            ProductId = 2,
                            Name= "Apple watch series 8",
                            Category = ECategory.HighEnd,
                            Price= 20,
                        },
                    }
                });

            foreach (var order in orders)
            {
                yield return order;
            }
        }

    }
}
