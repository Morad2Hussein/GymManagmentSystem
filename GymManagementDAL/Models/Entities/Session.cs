using GymManagementDAL.Models.Common;


namespace GymManagementDAL.Models.Entities
{
    public class Session : BaseEntity
    {
        #region Properties
        public int Capacity { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #endregion
        #region Relationships 
        #region Session - Categroy
        // Many => 1
        public int CategoryId { get; set; }
        public Category SessionCategory { get; set; }
        #endregion
        #region Trainer - Session
        // 1 to Many 
        public int TrainerId { get; set; }
        public Trainer SessionTrainer { get; set; }

        #endregion
        #region Session - MemberSession 
        // Session M => 1 MemberSession
        public ICollection<MemberSession> MemberSession { get; set; } = null!;
        #endregion

        #endregion

    }
}
