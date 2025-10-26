using AutoMapper;
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
        private readonly IUnitOfWork _iunitOfWork;
        private readonly IMapper _imapper;
         public PlanServiec(IUnitOfWork iunitOfWork, IMapper mapper) {
          _iunitOfWork = iunitOfWork;
            _imapper = mapper;
        }
        #region GetAll
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _iunitOfWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any()) return [];

            var PlanView = _imapper.Map<IEnumerable<Plan>, IEnumerable<PlanViewModel>>(Plans);
            return PlanView;
        }
        #endregion
        #region GetByID

        public PlanViewModel? GetPlanById(int id)
        {
            var Plan = _iunitOfWork.GetRepository<Plan>().GetById(id);
            if (Plan is null) return null;

            var PlanView = _imapper.Map<Plan, PlanViewModel>(Plan);
            return PlanView;
        }
        #endregion
        #region ToggleStatus

        public bool ToggleStatus(int id)
        {
            var PlanRepo = _iunitOfWork.GetRepository<Plan>();
            var Plan = PlanRepo.GetById(id);
            if (Plan is null || Plan.IsActive == false || HasMemberActive(id)) return false;
            Plan.IsActive = Plan.IsActive == true ? false : true;
            Plan.UpdateAt = DateTime.Now;
            try
            {
                PlanRepo.Update(Plan);
                return _iunitOfWork.SaveChanges() >0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);


                return false;
            }

        }
        #endregion

        #region Update View Model

        public UpdatePlanViewModel UpdatePlanById(int  PlanId)
        {
            var Plan = _iunitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan == null  || HasMemberActive(PlanId)) return null;
            //if (Plan is null  || Plan.IsVctive == false || HasMemberActive(PlanId)) return null;
            return _imapper.Map<UpdatePlanViewModel>(Plan);



        } 
        public bool UpdatePlan(int id, UpdatePlanViewModel planViewModel)
        {
            var Plan = _iunitOfWork.GetRepository<Plan>().GetById(id);
            try
            {
            if(Plan is null || HasMemberActive(id) ) return false;
              
               _imapper.Map<UpdatePlanViewModel>(Plan);
                Plan.UpdateAt = DateTime.Now;
                _iunitOfWork.GetRepository<Plan>().Update(Plan);
                return _iunitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }

        }



        #endregion
        #region Helper Function
       private bool HasMemberActive(int planId)
        {
            var ActiveMember = _iunitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.PlanId == planId && ms.Status=="Active");
            return ActiveMember.Any();
        } 

        #endregion




    }
}
