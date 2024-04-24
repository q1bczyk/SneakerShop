using API._Controllers;
using API.DTOs.OrderDTOs;
using API.Entities;
using API.Errors;
using API.Extensions;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrderAsync(OrderRequest orderRequest)
        {
            // foreach(var product in orderRequest.Products)
            // {
            //     var stock = await _stockRepository.GetStock(product.StockId);

            //     if(stock == null)
            //         return NotFound("Stock does not exist!");

            //     if(_placOrderExtension.ProductInStock(stock, product) == false)
            //         return BadRequest("Product out of stock!");

            // }
            try
            {
                await _orderService.PlaceOrderAsync(orderRequest);
                return Ok(orderRequest);
            } 
            catch(StockException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}