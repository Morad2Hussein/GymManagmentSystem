
using GymManagementBll.Mapping;
using GymManagementBll.Services.AttachmentService;
using GymManagementBll.Services.Classes;
using GymManagementBll.Services.Interfaces;
using GymManagementDAL.Data.Context;
using GymManagementDAL.Data.DataSeed;
using GymManagementDAL.Data.DataSeeding;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.Repositories.classes;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using GymManagementSystemBLL.Services.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymManagementPl
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymManagementDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            #region Services
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(IGenericRepository<>));
            //builder.Services.AddScoped<IPlanReposittory, PlanReposittory>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(au => au.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddAutoMapper(au => au.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService, TrainerServices>();
            builder.Services.AddScoped<IPlanServiec, PlanServiec>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                conf =>
                {
                    conf.User.RequireUniqueEmail = true;

                })
                             .AddEntityFrameworkStores<GymManagementDbContext>();
            builder.Services.ConfigureApplicationCookie(opetions =>
            {
                opetions.LoginPath = "/Account/Login";
                opetions.AccessDeniedPath = "/Account/AccessDenied";
            });
            var app = builder.Build();

            #endregion // “start to add data seeding before any data insert”
            #region Adding Data Seeding 
            //Creates a service scope → so you can resolve scoped services (like DbContext).
            //Applies any pending migrations automatically.
            //Calls your custom GymDbContextSeeding.SeedData() to insert initial data. 
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<GymManagementDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            GymDataSeeding.SeedData(dbContext);
            await IdentityDbContextSeeding.SeedData(roleManager, userManager);


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}/{id?}")
            .WithStaticAssets();


            app.Run();
        }
    }
}
