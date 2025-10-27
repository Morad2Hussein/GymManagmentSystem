using GymManagementDAL.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeeding
{
    public static class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                // check if i have users or not 
                var HasUsers = userManager.Users.Any();
                // check if i have roles or not 
                var HasRoles = roleManager.Roles.Any();

                // i have data , so i can not add seeding data
                if (HasUsers && HasRoles) return false;
                // I Need to add a data of role first 
                if (!HasRoles)
                {
                    var Roles = new List<IdentityRole>() {
                      new  (){ Name="SuperAdmin"},
                      new  (){ Name="Admin"}
                    };
                    // check and add the data 
                    foreach (var Role in Roles)
                    {
                        if (!roleManager.RoleExistsAsync(Role.Name!).Result)
                        {
                            roleManager.CreateAsync(Role).Wait();
                        }

                    }

                }
                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Morad",
                        LastName = "Mohamed",
                        UserName = "MoradMohamed",
                        Email = "MoradMohamed@gmail.com",
                        PhoneNumber = "01282265359",
                    };
                    userManager.CreateAsync(MainAdmin, "P@sswOrd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();
                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Ahmed",
                        LastName = "Mohamed",
                        UserName = "AhmedMohamed",
                        Email = "AhmedMohamed@gmail.com",
                        PhoneNumber = "01232265359",
                    };
                    userManager.CreateAsync(Admin, "P@sswOrd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seed Failed {ex}");
                return false;
            }
        }

    }
}
