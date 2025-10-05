using GymManagementDAL.Models.Common;
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
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            // varchar With Max Length 50
            builder.Property(P => P.Name)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);
            //varchar With Max Length 100
            builder.Property(P => P.Email)
                    .HasMaxLength(100);
            //  Make Email Unique
            builder.HasIndex(p => p.Email).IsUnique();            //varchar With Max Length 11 
            builder.Property(P => P.Phone)
                    .HasMaxLength(11);
            // Make Phone AS   Unique
            builder.HasIndex(p => p.Phone).IsUnique();            //Valid  Format Of Email And PhoneNumber 
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("EmailCheck", "Email LIKE '_%@_%._%'");
                t.HasCheckConstraint("PhoneCheck",
                    "Phone LIKE '01%' AND LEN(Phone) = 11 AND Phone NOT LIKE '%[^0-9]%'");
            });

            //Street  & City : varchar With Max Length 30  
            builder.OwnsOne(P => P.Address, AddressBuilder =>
            {
                AddressBuilder.Property(AD=>AD.Street)
                               .HasColumnType("varchar")
                               .HasMaxLength(30);
                AddressBuilder.Property(AD=>AD.City)
                               .HasColumnType("varchar")
                               .HasMaxLength(30);
            });

        }


    }
}
