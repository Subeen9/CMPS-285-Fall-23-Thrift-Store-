using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace LearningStarter.Entities;


    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
      
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }

        public DateTimeOffset DateAdded { get; set; }

      public List<ProductReviews> Reviews {  get; set; }
        public Product Products { get; set; }

    }

    public class ProductGetDto
    {
        public int Id { get; set; }
       // public int UserId { get; set; }
       // public int CategoriesId { get; set; }


        public string Name { get; set; }
        
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public List<ProductReviewsGetDto> Reviews { get; set; }
        
         

    }
    public class ProductCreateDto
    {
       
        public string Name { get; set; }
       // public int UserId { get; set; }
       // public int CategoriesId { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DateAdded { get; set; }



    }
    public class ProductUpdateDto
    {
       
        public string Name { get; set; }
       // public int UserId { get; set; }
      //  public int CategoriesId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DateAdded { get; set; }

    }
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

        }


    }


