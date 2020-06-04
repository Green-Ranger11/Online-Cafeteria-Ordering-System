using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class MealConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(b => b.Menu).WithMany().HasForeignKey(p => p.MenuId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.Restaurant).WithMany().HasForeignKey(p => p.RestaurantId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(b => b.MealType).WithMany().HasForeignKey(p => p.MealTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}