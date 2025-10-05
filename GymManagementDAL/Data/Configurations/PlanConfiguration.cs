

using GymManagementDAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    internal class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            #region Properties 
            //  Name varchar With Max Length 50
            builder.Property(P => P.Name)
                   .HasColumnType("varchar")
                    .HasMaxLength(50);
            //Description varchar With Max Length 200

            //Price Stored as a decimal with 10 digits in total and 2 digits after the decimal point 
            builder.Property(P => P.Pirce)
                    .HasPrecision(10, 2);

            //Duration Days From 1 To 365
            builder.ToTable(TB =>
                {
                    TB.HasCheckConstraint( "PlandurationCheck", "DurationDays Between 1 and 365");
                }

                );
            #endregion
        }
    }
}
