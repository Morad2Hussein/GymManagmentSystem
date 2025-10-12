using GymManagementDAL.Data.Context;
using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GymManagementDAL.Data.DataSeeding
{
    public static class GymDbContextSeeding
    {



        public static bool SeedDate(GymManagementDbContext gymManagementDb)
        {
            try
            {
                var HasPlan = gymManagementDb.Plans.Any();
                var HasCategories = gymManagementDb.Categories.Any();
                if (HasPlan || HasCategories) return false;
                if (!HasPlan)
                {
                    var Plans = LoadDataFromJsonFile<Plan>("plans.json");
                    if (Plans.Any())
                        gymManagementDb.AddRange(Plans);
                }
                if (!HasCategories)
                {

                    var Categories = LoadDataFromJsonFile<Category>("categories.json");
                    if (Categories.Any())
                        gymManagementDb.AddRange(Categories);
                }
                return gymManagementDb.SaveChanges() > 0;
            }
            catch (Exception error)
            {

                Console.WriteLine(error.ToString());
                return false;
            }


        }

        private static List<T> LoadDataFromJsonFile<T>(string fileName)
        {
            //F:\pro\GymManagmentSystem\GymManagmentPl\wwwroot\Files\
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
            // هشوف اذا كان الملف موجود ولا لا 
            if (!File.Exists(FilePath)) throw new FileNotFoundException();
            string json = File.ReadAllText(FilePath);
            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
        }
    }
}
