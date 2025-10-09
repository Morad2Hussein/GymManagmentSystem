using GymManagementBll.Services.Interfaces;
using GymManagementBll.ViewModels.PlanViewModels;
using GymManagementDAL.Models.Entities;
using GymManagementDAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.Services.Classes
{
    public class PlanServiec : IPlanServiec
    {
        private UnitOfWork _unitOfWork;
         public PlanServiec(UnitOfWork unitOfWork) {
          _unitOfWork = unitOfWork;
        }
        #region GetAll
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any()) return [];
            return Plans.Select(p => new PlanViewModel()
            {
                id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                IsActive = p.IsVctive,
                Price = p.Pirce,

            });
        }
        #endregion
        #region GetByID

        public PlanViewModel? GetPlanById(int id)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (Plan is null) return null;
            return new PlanViewModel() { 
              id = Plan.Id,
              Name = Plan.Name,
              Description = Plan.Description,
              DurationDays = Plan.DurationDays,
              IsActive = Plan.IsVctive,
              Price = Plan.Pirce,
            
            };
        }
        #endregion
        #region ToggleStatus

        public bool ToggleStatus(int id)
        {
            var PlanRepo = _unitOfWork.GetRepository<Plan>();
            var Plan = PlanRepo.GetById(id);
            if (Plan is null || Plan.IsVctive == false || HasMemberActive(id)) return false;
            Plan.IsVctive = Plan.IsVctive == true ? false : true;
            Plan.UpdateAt = DateTime.Now;
            try
            {
                PlanRepo.Update(Plan);
                return _unitOfWork.SaveChanges() >0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region Update View Model

        public UpdatePlanViewModel UpdatePlanById(int  PlanId)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null  || Plan.IsVctive == false || HasMemberActive(PlanId)) return null;
            return new UpdatePlanViewModel()
            {
                Name = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Pirce,
            };



        } 
        public bool UpdatePlan(int id, UpdatePlanViewModel planViewModel)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if(Plan is null || HasMemberActive(id) ) return false;
            try
            {
                Plan.Name = planViewModel.Name;
                Plan.Description = planViewModel.Description;
                Plan.DurationDays = planViewModel.DurationDays;
                Plan.Pirce = planViewModel.Price;
                Plan.UpdateAt = DateTime.Now;
                _unitOfWork.GetRepository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }



        #endregion
        #region Helper Function
       private bool HasMemberActive(int planId)
        {
            var ActiveMember = _unitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.PlanId == planId && ms.Status=="Active");
            return ActiveMember.Any();
        } 

        #endregion




    }
}
