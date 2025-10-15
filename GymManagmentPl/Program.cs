using AutoMapper;
using GymManagementBll.Mapping;
using GymManagementBll.Services.Classes;
using GymManagementBll.Services.Interfaces;
using GymManagementDAL.Data.Context;
using GymManagementDAL.Data.DataSeeding;
using GymManagementDAL.Repositories.classes;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace GymManagementPl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymManagementDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(IGenericRepository<>));
            //builder.Services.AddScoped<IPlanReposittory, PlanReposittory>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(au => au.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddAutoMapper(au => au.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IAnalyticsService,AnalyticsService>();
            var app = builder.Build();
            // “start to add data seeding before any data insert”
            #region Adding Data Seeding 
            //Creates a service scope → so you can resolve scoped services (like DbContext).
            //Applies any pending migrations automatically.
            //Calls your custom GymDbContextSeeding.SeedData() to insert initial data. 
            using var Scope = app.Services.CreateScope();
            var dbContext = Scope.ServiceProvider.GetRequiredService<GymManagementDbContext>();
            var PendingMigrations = dbContext.Database.GetPendingMigrations();
            if (PendingMigrations?.Any() ?? false)
                dbContext.Database.Migrate();
            GymDbContextSeeding.SeedDate(dbContext);


            #endregion



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
