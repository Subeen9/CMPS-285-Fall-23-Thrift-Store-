using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LearningStarter.Entities;

    public class ProductReviews
    {
        public int Id{ get; set; }
        public int ProductId {  get; set; }
        public Product Product { get; set; }
        public int ReviewsId {  get; set; } 
        public int ReviewsQuantity {  get; set; }
        public Reviews Reviews { get; set; } // Is not a column in the database but used to access the Review Id.
        public double Ratings { get; set; }
        public string Comments { get; set; }

}
    public class ProductReviewsGetDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ReviewsQuantity { get; set; }
    public int ReviewsId { get; set; }
        public Reviews Reviews { get; set; }
    public double Ratings { get; set; }
    public string Comments { get; set; }
}
public class ProductReviewsEntityTypeConfiguration : IEntityTypeConfiguration<ProductReviews>
{
    public void Configure(EntityTypeBuilder<ProductReviews> builder)
    {
        builder.ToTable("ProductReviews");
        builder.HasOne(x => x.Product)
            .WithMany(x => x.Reviews);

        builder.HasOne(x => x.Reviews)
            .WithMany();
    }


}


