using GymManagementDAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymManagementDAL.Data.Context
{
    public class GymManagementDbContext : DbContext
    {
        public GymManagementDbContext(DbContextOptions options) : base( options )
        {


        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=. ; Database = GymManagementSystem ; Trusted_Connection=True;  TrustServerCertificate = True ");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        #region DbSet
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
