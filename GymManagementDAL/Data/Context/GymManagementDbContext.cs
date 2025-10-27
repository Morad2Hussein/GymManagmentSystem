using GymManagementDAL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagementDAL.Data.Context
{
    public class GymManagementDbContext : IdentityDbContext<ApplicationUser>
    {
        public GymManagementDbContext(DbContextOptions options) : base( options )
        {


        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(
                Eb =>
                {
                    Eb.Property(e => e.FirstName)
                      .HasColumnType("varchar")
                       .HasMaxLength(50);
                
                    Eb.Property(e => e.LastName)
                      .HasColumnType("varchar")
                       .HasMaxLength(50);
                
                }

                );

        }
        #region 
   
        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet <Session> Sessions { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet <MemberSession> MemberSessions { get; set; }
        public DbSet <MemberShip> MemberShips { get; set; }

        #endregion
    }
}
