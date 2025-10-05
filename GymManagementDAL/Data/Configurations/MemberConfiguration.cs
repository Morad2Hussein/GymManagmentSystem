using GymManagementDAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.Configurations
{
    internal class MemberConfiguration : GymUserConfiguration<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            #region  Properties
            // use Configuration from GymUserConfiguration
            base.Configure(builder);
            // Join Date is automatically set to the insertion date

            builder.Property(P => P.CreateAt)
                    .HasColumnName("JoinDate")
                    .HasDefaultValue("GETDATE()");

            #endregion

        }
    }
}
