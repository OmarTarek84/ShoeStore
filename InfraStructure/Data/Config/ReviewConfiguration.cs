

using Core.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraStructure.Data.Config
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(a => a.Product)
                .WithMany(s => s.Reviews)
                .HasForeignKey(f => f.ProductId);

            builder.HasKey(k => new { k.ProductId, k.UserId });

            builder.Property(f => f.CreateDate).HasDefaultValueSql("getdate()");

            builder.Property(u => u.Rating)
            .HasConversion<int>()
            .HasMaxLength(1);
        }
    }
}
