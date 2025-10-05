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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            #region Properties 
            //  Name varchar With Max Length 50
            builder.Property(P => P.Name)
                   .HasColumnType("varchar")   
                   .HasMaxLength(50);

            #endregion

        }
    }
}
