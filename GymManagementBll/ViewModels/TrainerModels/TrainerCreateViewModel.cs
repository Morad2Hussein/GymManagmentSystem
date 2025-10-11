

using GymManagementDAL.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace GymManagementBll.ViewModels.TrainerModels
{
    public class TrainerCreateViewModel
    {
        #region Name
        [Required(ErrorMessage = "The Name is Required ")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Your Name Must Be Between 2 and 50 Charaters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name an Contain Only Letters And Spaces")]
        public string Name { get; set; } = null!;
        #endregion
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
        #region Specialties
        [Required(ErrorMessage = "Specialties Is Requried Please Enter Your Specialties")]
        [EnumDataType(typeof(Specialties))]
        public Specialties Specialties { get; set; } 
        #endregion
        #region Gender
        [Required(ErrorMessage = "Gender Is Requried Please Enter Your Gender")]
        public Gender Gender { get; set; }
        #endregion
        #region Date Of Birth
        [Required(ErrorMessage = "Date Of Birth Is Requried Please Enter Your  DateBrith")]
        public DateOnly DateOfBrith { get; set; }
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
