using GymManagementDAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberReposittory
    {
        // GetAll
        IEnumerable<Member> GetAll();
        // GET BY ID 
        Member? GetbyId(int id);
        // ADD
        int AddMember(Member member);   
        // Update 
        int Update(Member member);
        // Delete
        int Delete(int id);
    }
}
