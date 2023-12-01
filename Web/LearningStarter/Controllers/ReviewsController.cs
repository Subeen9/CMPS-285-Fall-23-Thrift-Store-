using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace LearningStarter.Controllers;
[ApiController]
[Route("api/Reviews")]

public class ReviewsController : ControllerBase
{
    
    private readonly DataContext _dataContext;

    public ReviewsController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    [HttpGet]
        public IActionResult GetAll()
    {
        var response = new Response();
        var data = _dataContext
            .Set<Reviews>()
            .Select(Reviews => new ReviewsGetDto
            {
                Id = Reviews.Id,
                UserId = Reviews.UserId,    
                Comments = Reviews.Comments,
                ProductId = Reviews.ProductId,
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
            .Set<Reviews>()
            .Select(Reviews => new ReviewsGetDto
            {
                Id = Reviews.Id,
                UserId = Reviews.UserId,
                Comments = Reviews.Comments,
                ProductId = Reviews.ProductId,
            })

            .FirstOrDefault(Reviews => Reviews.Id == id);
        response.Data = data;
        return Ok(response);
    }
    [HttpPost]
    public IActionResult Create([FromBody] ReviewsCreateDto createDto)
    {
        var response = new Response();
        if (createDto.ProductId == null)
        {
            response.AddError(nameof(createDto.ProductId), " Product Id cannot be null");
        }
        if(createDto.Ratings < 0)
        {
            response.AddError(nameof(createDto.Ratings), " Ratings cannot be negative");
        }
        if (createDto.UserId == null)
        {
            response.AddError(nameof(createDto.UserId), " Ratings cannot be null");
        }
        if (response.HasErrors)
        {
            return BadRequest(response);
        }
        var ReviewsToCreate = new Reviews
        {
            Id = createDto.Id,
            UserId = createDto.UserId,
            Comments = createDto.Comments,
            ProductId = createDto.ProductId,
            Ratings = createDto.Ratings,    

        };
        _dataContext.Set<Reviews>().Add(ReviewsToCreate);
        _dataContext.SaveChanges();
        var ReviewsToReturn = new ReviewsGetDto
        {
          Id = ReviewsToCreate.Id,
          UserId = ReviewsToCreate.UserId,
          Comments = ReviewsToCreate.Comments,
          ProductId=ReviewsToCreate.ProductId,  
        };
        response.Data= ReviewsToReturn;
        return Created("", response);
    }
    [HttpPut("{id}")]
    public IActionResult Update([FromBody] ReviewsUpdateDto updateDto, int id)
    {
        var response = new Response();
        if(updateDto.ProductId == null)
        {
            response.AddError(nameof(updateDto.ProductId), " Product Id cannot be null");
        }
        if (updateDto.Ratings < 0)
        {
            response.AddError(nameof(updateDto.Ratings), " Ratings cannot be negative");
        }
        if (updateDto.UserId == null)
        {
            response.AddError(nameof(updateDto.UserId), " Ratings cannot be null");
        }

        var ReviewsToUpdate = _dataContext.Set<Reviews>()
             .FirstOrDefault(x => x.Id == id);
        if (ReviewsToUpdate.Id == null)
        {
            response.AddError("id", "Product not found");
        }
        if (response.HasErrors)
        {
            return BadRequest(response);
        }
        ReviewsToUpdate.Id = updateDto.Id;
        ReviewsToUpdate.ProductId = updateDto.ProductId;
        ReviewsToUpdate.Comments = updateDto.Comments;
        ReviewsToUpdate.Ratings = updateDto.Ratings;
        ReviewsToUpdate.UserId = updateDto.UserId;
        _dataContext.SaveChanges();
        var ReviewsToReturn = new ReviewsGetDto
        {
            Id = ReviewsToUpdate.Id,
            UserId = ReviewsToUpdate.UserId,
            Comments = ReviewsToUpdate.Comments,
            Ratings = updateDto.Ratings,
            ProductId = updateDto.ProductId,
        };
        response.Data= ReviewsToReturn;
        return Ok(response);
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    { 
        var response = new Response();  
        var ReviewsToDelete = _dataContext.Set<Reviews>()
            .FirstOrDefault(Reviews => Reviews.Id == id);
        if(ReviewsToDelete.Id == null )
        {
            response.AddError("id", "Product not found");
        }
        if(response.HasErrors)
        {
            return BadRequest(response);    
        }
        _dataContext.Set<Reviews>().Remove(ReviewsToDelete);
        _dataContext.SaveChanges();
        response.Data = true; 
        return Ok(response);
    }
    
}

