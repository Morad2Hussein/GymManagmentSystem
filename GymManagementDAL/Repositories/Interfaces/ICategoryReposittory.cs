using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface ICategoryReposittory
    {
        //getAll
        IEnumerable<Category> GetAll();
        // GETBYID
        Category GetById(int id);
        // Add //  Insert
        int Add(Category category);
        // Update
        int Update(Category category);
        // Delete 
        int DeleteCategory(int id);
    }
}
