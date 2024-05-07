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
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IOrderRepository orderRepository, IMapper mapper)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrderAsync(OrderRequest orderRequest)
        {
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

        [HttpGet("{orderId}")]
        public async Task<ActionResult<List<Order>>> GetOrder(string orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            return Ok(_mapper.Map<OrderResponse>(order));
        }
    }
}