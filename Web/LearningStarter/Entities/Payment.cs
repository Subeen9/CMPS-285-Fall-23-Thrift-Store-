using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace LearningStarter.Entities;

public class Payment
{
    public int id { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public int InvoiceNumber { get; set; }
    public string Method { get; set; }
    public Order Order { get; set; }
}
public class PaymentGetDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public int InvoiceNumber { get; set; }
    public string Method { get; set; }

}
public class PaymentCreateDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public int InvoiceNumber { get; set; }
    public string Method { get; set; }

}
public class PaymentUpdateDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public int InvoiceNumber { get; set; }
    public string Method { get; set; }

}


public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment");
    }
}

