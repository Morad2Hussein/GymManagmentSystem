using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        #region Name 
        [Required(ErrorMessage = " You Must Enter Your Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "You Must Write Your Name Between 3 and  50 Charaters ")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name an Contain Only Letters And Spaces ")]
        public string Name { get; set; } = null!;
        #endregion
        #region Description
        [Required(ErrorMessage = " You Must Enter Your Description")]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "You Must Write Your Name Between 10 and  300 Charaters ")]
        public string Description { get; set; } = null!;
        #endregion
        #region DurationDays
        [Required(ErrorMessage = "The Duration Days  is Reqired")]

        [Range(1,365, ErrorMessage = "Duration Days Must Be Between 1 to 365 Days")]
        public int DurationDays { get; set; }

        #endregion
        #region Price
        [Required(ErrorMessage ="The Price Is Required")]
        [Range( 250,10000, ErrorMessage = "Duration Days Must Be Between 250 to 10000 ")]
         
        public decimal Price { get; set; }

        #endregion
    }
}
