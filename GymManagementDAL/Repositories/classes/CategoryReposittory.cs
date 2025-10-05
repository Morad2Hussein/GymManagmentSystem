using GymManagementDAL.Data.Context;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.classes
{
    internal class CategoryReposittory : ICategoryReposittory
    {
        private readonly GymManagementDbContext _dbContext;
        public CategoryReposittory(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Category category)
        {
            _dbContext.Categories.Add(category);
            return _dbContext.SaveChanges();
        }

        public int DeleteCategory(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return -1;
            _dbContext.Categories.Remove(category);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAll()=>_dbContext.Categories.ToList();


        public Category GetById(int id)=> _dbContext.Categories.Find(id);

        public int Update(Category category)
        {
            _dbContext.Categories.Update(category);
            return _dbContext.SaveChanges();
        }
    }
}
