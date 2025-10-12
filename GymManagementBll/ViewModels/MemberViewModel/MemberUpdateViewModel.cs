using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBll.ViewModels.MemberViewModels
{
    public class MemberUpdateViewModel
    {
        public String Name { get; set; } = null!;
        public String? Photo { get; set; }


        #region Email
        [Required(ErrorMessage = "Email is Required Please Enter Your Email Address.")] // Email is required 
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Email Must Not Exceed 100 Characters.")] // Max 100 char
        [EmailAddress(ErrorMessage = "Please Enter A Valid Email Address.")] // Validtion 
        [DataType(DataType.EmailAddress)] // UI HINT
        public string Email { get; set; } = null!;
        #endregion

        #region PhoneNumber 
        [Required(ErrorMessage = "Phone is Required Please Enter Your Phone.")]
        [Phone(ErrorMessage = "Invliod Phone Format ")]
        [RegularExpression(@"^(011|012|015)\d{8}$", ErrorMessage = "Phone Number Must Be Valid Egyption PhoneNumber")]
        [DataType(DataType.PhoneNumber)] // UI HINT TO MAKE USER KeyBoard AS A Formate Number 
        public string Phone { get; set; } = null!;
        #endregion



        #region Address
        [Required(ErrorMessage = "Building Number Is Required ")]
        [Range(1, 900, ErrorMessage = "Building Number be Between 1 to 900")]
        public int BuildingNumber { get; set; }
        [Required(ErrorMessage = "Street Is Required ")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "You Must Write Your Street Between 3 and  150 Charaters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street an Contain Only Letters And Spaces ")]
        public string Street { get; set; } = null!;
        [Required(ErrorMessage = "Building Number Is Required ")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "You Must Write Your City Between 3 and  150 Charaters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City an Contain Only Letters And Spaces ")]
        public string City { get; set; } = null!;

        #endregion
    }
}