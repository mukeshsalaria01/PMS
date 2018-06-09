using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PMS.Web.UI.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}
