using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class MealPlanDayConfiguration : IEntityTypeConfiguration<MealPlanDay>
    {
        public void Configure(EntityTypeBuilder<MealPlanDay> builder)
        {
            builder.HasOne(m => m.Meal).WithMany().HasForeignKey(m => m.MealId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}