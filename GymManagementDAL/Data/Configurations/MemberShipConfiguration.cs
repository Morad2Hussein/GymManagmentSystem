using GymManagementDAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GymManagementDAL.Data.Configurations
{
    internal class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            #region Properties
            // StartDate AS A CreateAT 
            builder.Property(P => P.CreateAt)
                   .HasColumnName("StartDate")
                    .HasDefaultValue("GETDATE()");
            //Composite Primary Key
            builder.HasKey(p => new { p.PlanId, p.MemberId });
            //  Ignore THE id
            builder.Ignore(p => p.Id);
            #endregion
        }
    }
}
