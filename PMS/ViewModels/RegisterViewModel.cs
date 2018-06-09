using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PMS.Web.UI.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<SelectListItem> Roles { get; set; }

        [DisplayName("Role")]
        public string RoleId { get; set; }
    }
}
