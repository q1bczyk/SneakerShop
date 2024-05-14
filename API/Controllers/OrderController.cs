using System.IdentityModel.Tokens.Jwt;
using API._Controllers;
using API.DTOs.OrderDTOs;
using API.Entities;
using API.Errors;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IOrderRepository orderRepository, IContactRepository contactRepository, IMapper mapper)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        [Authorize(Policy = "RequireUserRole")]
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrderAsync(OrderRequest orderRequest)
        {
            var userId = HttpContext.User.FindFirst("userId")?.Value;
            var email = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Name)?.Value;

            try
            {
                await _orderService.PlaceOrderAsync(orderRequest, userId, email);
                return Ok(orderRequest);
            } 
            catch(ControlledException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [Authorize(Policy = "RequireModeratorRole")]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(string orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            return Ok(_mapper.Map<OrderResponse>(order));
        }

        [Authorize(Policy = "RequireModeratorRole")]
        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders(int page, int pageSize)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(page, pageSize);
            return Ok(_mapper.Map<List<OrderResponse>>(orders));
        }

        [Authorize(Policy = "RequireModeratorRole")]
        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderResponse>> HandleOrder(OrderStatusRequest orderStatusRequest, string orderId)
        {
            try
            {
                var order = await _orderService.ChangeOrderStatusAsync(orderStatusRequest.Status, orderId);
                return Ok(_mapper.Map<OrderResponse>(order));
            }
            catch(ControlledException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}