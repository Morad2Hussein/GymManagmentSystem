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
            // Varchar With Max Length 50
            builder.Property(P => P.Name)
                   .HasColumnType("Varchar")
                   .HasMaxLength(50);
            //Varchar With Max Length 100
            builder.Property(P => P.Email)
                    .HasMaxLength(100);
            //  Make Email Unique
            builder.HasIndex(P => P.Email).IsUnique();
            //Varchar With Max Length 11 
            builder.Property(P => P.Phone)
                    .HasMaxLength(11);
            // Make Phone AS   Unique
            builder.HasIndex(P=>P.Phone).IsUnique();
            //Valid  Format Of Email And PhoneNumber 
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("EmailCheck", "Email LIKE '_%@_%._%'");
                t.HasCheckConstraint("PhoneNumberCheck",
                 "PhoneNumber LIKE '01%' AND LEN(PhoneNumber) = 11 AND PhoneNumber NOT LIKE '%[^0-9]%'");

            });
            //Street  & City : Varchar With Max Length 30  
            builder.OwnsOne(P => P.Address, AddressBuilder =>
            {
                AddressBuilder.Property(AD=>AD.Street)
                               .HasColumnType("Varchar")
                               .HasMaxLength(30);
                AddressBuilder.Property(AD=>AD.City)
                               .HasColumnType("Varchar")
                               .HasMaxLength(30);
            });

        }

        internal void Configure(EntityTypeBuilder<Trainer> builder)
        {
            throw new NotImplementedException();
        }
    }
}
