
using System.ComponentModel.DataAnnotations;


namespace GymManagementBll.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        #region Heigth and Weigth
        [Required(ErrorMessage = "Heigth is Required Plase Enter Your Heigth")]
        [Range(50, 250, ErrorMessage = "Your Heigth May Be Between 50 cm to 250 cm ")]
        public decimal Heigth { get; set; }
        [Required(ErrorMessage = "Weigth is Required Plase Enter Your Weigth")]
        [Range(50, 250, ErrorMessage = "Your Weigth May Be Between 30 km to 500 kg ")]
        public decimal Weight { get; set; }
        #endregion
        #region BloodType
        [Required(ErrorMessage = "Blood Type is Required Plase Enter Your Blood Type")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = " Your Blood Type Must Be  Between 2 and 3  Charaters")]
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; } 

        #endregion

    }
}