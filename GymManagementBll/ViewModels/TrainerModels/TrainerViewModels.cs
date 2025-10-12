using GymManagementDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.ViewModels.TrainerModels
{
    public class TrainerViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Specialties { get; set; } = null!;
        public string? Address { get; set; }
        public string? DateOfBirth { get; set; }
    }
}
