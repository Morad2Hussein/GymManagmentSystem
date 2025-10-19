using GymManagementDAL.Data.Context;

using GymManagementDAL.Models.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GymManagementDAL.Data.DataSeed
{
    public static class GymDataSeeding
    {
        public static bool SeedData(GymManagementDbContext dbContext)
        {
            try
            {
                var hasPlans = dbContext.Plans.Any();
                var hasCategories = dbContext.Categories.Any();
                if (hasCategories && hasPlans) return false;

                if (!hasPlans)
                {
                    var plans = LoadDataFromJsonFile<Plan>("Plans.json");
                    if (plans.Any()) dbContext.Plans.AddRange(plans);
                }
                if (!hasCategories)
                {
                    var categories = LoadDataFromJsonFile<Category>("Categories.json");
                    if (categories.Any()) dbContext.Categories.AddRange(categories);
                }


                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Seeding failed: " + ex.Message);
                return false;
            }
        }

        private static List<T> LoadDataFromJsonFile<T>(string FileName)
        {
            //\07 MVC\Session 02\Code\GymManagementSystemSolution\GymManagementPL\wwwroot\Files\
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FileName);
            if (!File.Exists(path))
                throw new FileNotFoundException();
            var jsonData = File.ReadAllText(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(jsonData, options) ?? new List<T>();
        }

    }
}


        //public static bool SeedData(GymManagementDbContext dbContext)
        //{
        //    try
        //    {
        //        bool HasCategories = dbContext.Categories.Any();
        //        bool HasPlans = dbContext.Plans.Any();
        //        if (HasCategories && HasPlans) return false;

        //        if (!HasCategories)
        //        {
        //            var Categories = LoadDataFromJsonFile<Category>("categories.json");
        //            dbContext.Categories.AddRange(Categories);
        //        }

        //        if (!HasPlans)
        //        {
        //            var Plans = LoadDataFromJsonFile<Plan>("plans.json");
        //            dbContext.Plans.AddRange(Plans);
        //        }

        //        int RowsAffected = dbContext.SaveChanges();
        //        return RowsAffected > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Seeding Failed : {ex}");
        //        return false;
        //    }
        //}

        //private static List<T> LoadDataFromJsonFile<T>(string fileName)
        //{
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);

        //    if (!File.Exists(filePath)) throw new FileNotFoundException();

        //    string Data = File.ReadAllText(filePath);
        //    var Options = new JsonSerializerOptions()
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };

        //    Options.Converters.Add(new JsonStringEnumConverter());
        //    return JsonSerializer.Deserialize<List<T>>(Data, Options) ?? new List<T>();

        //}