using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LearningStarter.Controllers;
    [ApiController]
[Route("api/order")]

    public class OrdersController : Controller
    {
        private readonly DataContext _dataContext;
        public OrdersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();
            var data = _dataContext.Set<Order>().Select(order => new OrderGetDto
            {
                Id = order.Id,
                UserId = order.UserId,
                PaymentId = order.PaymentId,
                Price = order.Price,
                Quantity = order.Quantity,
                Date = order.Date,
                Status = order.Status

            }).ToList();
            response.Data = data;
            return Ok(response);

        }
    [HttpGet("{id}")]
    public IActionResult GetById (int id)
        {
            var response = new Response();
            var data = _dataContext
                .Set<Order>()
                .Where(order => order.UserId == id)
                .Select(order => new OrderGetDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    PaymentId = order.PaymentId,
                    Price = order.Price,
                    Quantity = order.Quantity,
                    Date = order.Date,
                    Status = order.Status
                }).FirstOrDefault(order => order.Id == id);
            response.Data= data;
            return Ok(response);    

        }
        [HttpPost]
        public IActionResult Create([FromBody] OrderCreateDto orderCreateDto)
        {
            var response = new Response();
            if (orderCreateDto.Price < 0)
            {
                response.AddError(nameof(orderCreateDto.Price), "Invalid Price");
            }
            if (response.HasErrors)
            {
                return BadRequest(response);
            }
            var orderToCreate = new Order
            {
                UserId = orderCreateDto.UserId,
                PaymentId = orderCreateDto.PaymentId,
                Price = orderCreateDto.Price,
                Quantity = orderCreateDto.Quantity,
                Date = orderCreateDto.Date,
                Status = orderCreateDto.Status
            };
            _dataContext.Set<Order>().Add(orderToCreate);

            _dataContext.SaveChanges();

            var orderToResponse = new OrderGetDto
            {
               Id = orderToCreate.Id,
                UserId = orderToCreate.UserId,
                PaymentId = orderToCreate.PaymentId,
                Price = orderToCreate.Price,
                Quantity = orderToCreate.Quantity,
                Date = orderToCreate.Date,
                Status = orderToCreate.Status
            };
            response.Data = orderToResponse;
            return Created("", response);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] OrderUpdateDto updateDto, int id)
        {
            var response = new Response();
            if (updateDto.Price < 0)
            {
                response.AddError(nameof(updateDto.Price), "Invalid Price");

            }
            var orderToUpdate = _dataContext.Set<Order>()
                .FirstOrDefault(order => order.Id == id);
            if (orderToUpdate == null) {
                response.AddError("id", "Order not found");
            }
            if (response.HasErrors) { 
            return BadRequest(response);
            }
            orderToUpdate.Quantity= updateDto.Quantity;
            orderToUpdate.Status= updateDto.Status; 
            orderToUpdate.Price= updateDto.Price;
            orderToUpdate.PaymentId= updateDto.PaymentId;
            orderToUpdate.Date= updateDto.Date;
            _dataContext.SaveChanges();

            var orderToUpdateResponse = new OrderGetDto
            {
                Id = orderToUpdate.Id,
                UserId = orderToUpdate.UserId,
                PaymentId = orderToUpdate.PaymentId,
                Price = orderToUpdate.Price,
                Quantity = orderToUpdate.Quantity,
                Date = orderToUpdate.Date,
                Status = orderToUpdate.Status
            };

            response.Data = orderToUpdateResponse;
            return Ok(response);

               }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = new Response();
            var orderToDelete = _dataContext.Set<Order>()       
                .FirstOrDefault (order => order.Id == id);

            if (orderToDelete == null) {
                response.AddError("id", "Order not found");
            }
            if (response.HasErrors) { 
                return BadRequest(response);
            };
            _dataContext.Set<Order>().Remove(orderToDelete);    
            _dataContext.SaveChanges();

            response.Data = true;
            return Ok("Deleted Successfully");
        }

    }

