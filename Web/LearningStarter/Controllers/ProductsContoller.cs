using LearningStarter.Common;
using LearningStarter.Data;
using LearningStarter.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using System.Linq;

namespace LearningStarter.Controllers;
[ApiController]
[Route("/api/Product")]
public class ProductsContoller : ControllerBase
{
    private readonly DataContext _datacontext;
    public ProductsContoller(DataContext dataContext)
    {
        _datacontext = dataContext;

    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var response = new Response();
        var data = _datacontext
            .Set<Product>()
            .Select(product => new ProductGetDto
            {
                Id = product.Id,
                // UserId = product.UserId,
                // CategoriesId = product.CategoriesId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                OrderType = product.OrderType,
                Status = product.Status,
                DateAdded = product.DateAdded,
                Reviews = product.Reviews.Select(x => new ProductReviewsGetDto
                {
                    Id = x.Reviews.Id,
                    ReviewsQuantity= x.ReviewsQuantity,
                    Comments = x.Reviews.Comments,
                    Ratings = x.Reviews.Ratings,
                    
                }).ToList()

            }).ToList();
        response.Data = data;
        return Ok(response);

    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var response = new Response();
        var data = _datacontext
            .Set<Product>()
            .Select(product => new ProductGetDto
            {
                Id = product.Id,
                // UserId= product.UserId,
                // CategoriesId = product.CategoriesId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                OrderType = product.OrderType,
                Status = product.Status,
                DateAdded = product.DateAdded,
                Reviews = product.Reviews.Select(x => new ProductReviewsGetDto
                {
                    Id = x.Reviews.Id,
                    Comments = x.Reviews.Comments

                }).ToList()

            }).ToList()


        .FirstOrDefault(product => product.Id == id);
        response.Data = data;
        return Ok(response);

    }


    [HttpPost]

    public IActionResult Create([FromBody] ProductCreateDto createDto)
    {
        var response = new Response();

        if (string.IsNullOrEmpty(createDto.Name))
        {
            response.AddError(nameof(createDto.Name), "Name must not be empty");

        }

        if (createDto.Price < 0)
        {
            response.AddError(nameof(createDto.Price), "price must be positive");
        }
        if (response.HasErrors)
        {
            return BadRequest(response);
        }
        var productTOcreate = new Product
        {
            Name = createDto.Name,
            // UserId = createDto.UserId,
            //CategoriesId= createDto.CategoriesId,
            Description = createDto.Description,
            Price = createDto.Price,
            Quantity = createDto.Quantity,
            OrderType = createDto.OrderType,
            Status = createDto.Status,
            DateAdded = createDto.DateAdded,
        };

        _datacontext.Set<Product>().Add(productTOcreate);
        _datacontext.SaveChanges();

        var productToReturn = new ProductGetDto
        {
            Id = productTOcreate.Id,
            // UserId= productTOcreate.UserId,
            // CategoriesId = productTOcreate.CategoriesId,
            Name = productTOcreate.Name,
            Description = productTOcreate.Description,
            Price = productTOcreate.Price,
            Quantity = productTOcreate.Quantity,
            OrderType = productTOcreate.OrderType,
            Status = productTOcreate.Status,
            DateAdded = productTOcreate.DateAdded,
        };
        response.Data = productToReturn;
        return Created("", response);
    }
    [HttpPost("{ProductId}/Reviews/{ReviewsId}")]
    public IActionResult AddReviewsToProducts(int ProductsId, int ReviewsId, [FromQuery] int reviewsQuantity)
    {
        var response = new Response();
        var product = _datacontext.Set<Product>()
            .FirstOrDefault(x => x.Id == ProductsId);

        var reviews = _datacontext.Set<Reviews>()
            .FirstOrDefault(x => x.Id == ReviewsId);
        var productreviews = new ProductReviews
        {
            Product = product,
            Reviews = reviews,
            ReviewsQuantity = reviewsQuantity

        };
        _datacontext.Set<ProductReviews>().Add(productreviews);
        _datacontext.SaveChanges();
        response.Data = new ProductGetDto
        {
            Id = product.Id,
            // UserId= product.UserId,
            // CategoriesId = product.CategoriesId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            OrderType = product.OrderType,
            Status = product.Status,
            DateAdded = product.DateAdded,
            Reviews = product.Reviews.Select(x => new ProductReviewsGetDto
            {
                Id = x.Reviews.Id,
                ReviewsQuantity = x.ReviewsQuantity,
                Comments = x.Reviews.Comments,
                Ratings = x.Reviews.Ratings,

            }).ToList()

        };
        return Ok(response);

    }
}


//    [HttpPut("{id}")]

//    public IActionResult Update([FromBody] ProductUpdateDto updateDto, int id)
//    {


//        var response = new Response();
//        if (string.IsNullOrEmpty(updateDto.Name))
//        {
//            response.AddError(nameof(updateDto.Name), "Name must not be empty");

//        }

//        if (updateDto.Price <0)
//        {
//            response.AddError(nameof(updateDto.Price), "price must be positive");
//        }
       
//        var productToUpdate = _datacontext.Set<Product>()
//            .FirstOrDefault(product => product.Id == id);

//        if (productToUpdate == null)
//        {
//            response.AddError("id", "Product not found ");

//        }
//        if (response.HasErrors)
//        {
//            return BadRequest(response);
//        }
//        productToUpdate.Name = updateDto.Name;
//        productToUpdate.Description = updateDto.Description;
//        productToUpdate.Price = updateDto.Price;
//        productToUpdate.OrderType = updateDto.OrderType;
//        productToUpdate.Quantity = updateDto.Quantity;
//        productToUpdate.Status = updateDto.Status;
//        productToUpdate.DateAdded = updateDto.DateAdded;

//        _datacontext.SaveChanges();

//        var productToReturn = new ProductGetDto
//        {
//       //  Id = productToUpdate.Id,
//       //  Name = productToUpdate.Name,
//         Description = productToUpdate.Description,
//         Price = productToUpdate.Price,
//         Status = productToUpdate.Status,
//         DateAdded = productToUpdate.DateAdded,
//         Quantity = productToUpdate.Quantity,

//        };

//        response.Data = productToReturn;
//        return Ok(response);

//    }
//    [HttpDelete ("{id}")]
//    public IActionResult Delete (int id )
//    {

//        var response = new Response();
//        var productToDelete = _datacontext.Set<Product>()
//            .FirstOrDefault (product => product.Id == id);

//        if (productToDelete == null) {
//            response.AddError("id", "Product not found ");
            
//        }
//         if (response.HasErrors) {
//            return BadRequest(response);
//        }


//        _datacontext.Set<Product>().Remove(productToDelete);
//            _datacontext.SaveChanges();

//        response.Data = true;
//        return Ok("deleted successfully");
//    }
      
//}


