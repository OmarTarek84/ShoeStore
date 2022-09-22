

using Core.Entities.Cart;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Data.Config
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(s => new { s.ProductId, s.UserId });

            builder.HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId);
        }
    }
}
