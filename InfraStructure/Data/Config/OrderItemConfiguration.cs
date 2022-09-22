

using Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(s => new { s.OrderId, s.ProductId });

            builder.HasOne(f => f.Order)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(f => f.OrderId);
        }
    }
}
