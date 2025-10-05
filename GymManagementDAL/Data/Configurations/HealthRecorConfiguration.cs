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
    internal class HealthRecorConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            #region Relationships
            builder.ToTable("Members")
                    .HasKey(H=>H.Id);
            builder.HasOne<Member>()
                   .WithOne(H => H.HealthRecord)
                   .HasForeignKey<HealthRecord>(H => H.Id); // SHEARED PRIMARY KEY 
            builder.Ignore(H => H.CreateAt);

            #endregion
        }
    }
}
