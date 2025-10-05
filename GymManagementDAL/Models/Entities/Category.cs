using GymManagementDAL.Models.Common;


namespace GymManagementDAL.Models.Entities
{
    public class Category :BaseEntity
    {
        #region Properties
        public string Name { get; set; } = null!;
        #endregion
        #region Relationships 
        // Category 1--  M Session
        public ICollection<Session> Sessions { get; set; } = null!;
        #endregion
    }
}
