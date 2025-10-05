using GymManagementDAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GymManagementDAL.Data.Configurations
{
    internal class TrainerConfiguration : GymUserConfiguration<Member>, IEntityTypeConfiguration<Trainer>
    {
        public new void Configure(EntityTypeBuilder<Trainer> builder)
        {
            #region Properties 
            // Use The Properties  from GymUserConfiguration
            base.Configure(builder);
            //Default Value Insertion Date 
            builder.Property(P => P.CreateAt)
                   .HasColumnName("HireDate")
                   .HasDefaultValue("GETDATE()");

            #endregion
        }
    }
}
