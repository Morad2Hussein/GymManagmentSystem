using GymManagementDAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GymManagementDAL.Data.Configurations
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            #region Properties 
            // Capacity   From 1 To 25
            //Date End date must be after start date
            builder.ToTable(TB =>
            {
                TB.HasCheckConstraint("SessionCapacityCheck", "Capacity Between 1 and 25");
                TB.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");
            });
            #endregion
            #region Relationship Configurations  
            // Session Category Raltionship    SE 1 => M CAT
            builder.HasOne(SE => SE.SessionCategory)
                   .WithMany(CAT => CAT.Sessions)
                   .HasForeignKey(SE => SE.CategoryId);
            
            // Session Trainer Raltionship    SE 1 => M TR

            builder.HasOne(SE =>SE.SessionTrainer)
                   .WithMany(TR => TR.Sessions)
                   .HasForeignKey(SE => SE.TrainerId);

            
            #endregion
        }
    }
}
