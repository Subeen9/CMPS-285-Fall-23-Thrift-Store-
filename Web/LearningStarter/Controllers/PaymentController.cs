using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LearningStarter.Controllers;
[ApiController]
[Route("api/Payment")]

public class PaymentController : ControllerBase
{
    private readonly DataContext _dataContext;
    public PaymentController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var response = new Response();
        var data = _dataContext
            .Set<Payment>()
            .Select(payment => new PaymentGetDto
            {
               Id = payment.id,
               UserId = payment.UserId,
               InvoiceNumber = payment.InvoiceNumber,
               Method = payment.Method,
               OrderId = payment.OrderId
            })
            .ToList();
        response.Data = data;
        return Ok(response);

    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var response = new Response();
        var data = _dataContext
            .Set<Payment>()
            .Select(payment => new PaymentGetDto
            {
                Id = payment.id,
                UserId = payment.UserId,
                InvoiceNumber = payment.InvoiceNumber,
                Method = payment.Method,
                OrderId = payment.OrderId
            })
            .FirstOrDefault(payment => payment.Id == id);
        response.Data = data;
        return Ok(response);
    }
    [HttpPost]
    public IActionResult Create([FromBody] PaymentCreateDto CreateDto)
    {
        var response = new Response();
        if(string.IsNullOrEmpty(CreateDto.Method))
        {
            response.AddError(nameof(CreateDto.Method), "Payment method must be defined");
        }
       
        if (response.HasErrors)
        {
            return BadRequest(response);
        }
        var PaymentToCreate = new Payment
        {
            id = CreateDto.Id,
            UserId = CreateDto.UserId,
            InvoiceNumber = CreateDto.InvoiceNumber,
            Method = CreateDto.Method,
            OrderId = CreateDto.OrderId

        };
        _dataContext.Set<Payment>().Add(PaymentToCreate);
        _dataContext.SaveChanges();
        var PaymentToReturn = new PaymentGetDto()
        {
            Id = PaymentToCreate.id,
            UserId = PaymentToCreate.UserId,
            Method = PaymentToCreate.Method,
            OrderId = PaymentToCreate.OrderId,
            InvoiceNumber = PaymentToCreate.InvoiceNumber
        };
        response.Data = PaymentToReturn;
        return Created("", response);

    }
    [HttpPut("{id}")]
    public IActionResult Update([FromBody] PaymentUpdateDto UpdateDto, int id)
    {
        var response = new Response();
        if (string.IsNullOrEmpty(UpdateDto.Method))
        {
            response.AddError(nameof(UpdateDto.Method), "Payment method must be defined");
        }
       
        if (response.HasErrors)
        {
            return BadRequest(response);
        }
        var PaymentToUpdate = _dataContext.Set<Payment>()
            .FirstOrDefault(Payment => Payment.id == id);
        PaymentToUpdate.OrderId =UpdateDto.OrderId;
        PaymentToUpdate.Method = UpdateDto.Method;
        PaymentToUpdate.InvoiceNumber = UpdateDto.InvoiceNumber;
        PaymentToUpdate.UserId = UpdateDto.UserId;
        PaymentToUpdate.id = UpdateDto.Id;
        _dataContext.SaveChanges();
        var PaymentToReturn = new PaymentGetDto()
        {
            Id = UpdateDto.OrderId,
            UserId = UpdateDto.UserId,
            Method = UpdateDto.Method,
            OrderId = UpdateDto.OrderId,
            InvoiceNumber = UpdateDto.InvoiceNumber

        };
        response.Data = PaymentToReturn;
        return Ok(response);

    }
  
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var response = new Response();
        var PaymentToDelete = _dataContext.Set<Payment>()
            .FirstOrDefault(payment => payment.id == id);
        if (PaymentToDelete.id == null)
        {
            response.AddError("id", "Product not found");
        }
        if (response.HasErrors)
        {
            return BadRequest(response);
        }
        _dataContext.Set<Payment>().Remove(PaymentToDelete);
        _dataContext.SaveChanges(true);
        response.Data = true;
        return Ok(response);
    }
}
