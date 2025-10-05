using GymManagementDAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            #region Properties 
            // The BookingDate AS a CreateAt In BaseEntity 
            builder.Property(P => P.CreateAt)
                    .HasColumnName("BookingDate")
                    .HasDefaultValue("GETDATE()");
            //Composite Primary Key
            builder.Ignore(p => p.Id);
            builder.HasKey(P => new { P.MemberId, P.SessionId });

            #endregion
        }
    }
}
