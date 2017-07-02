using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailShop.Areas.Administrators.Models
{
    public class AdministratorViewModel 
    {

        public string Name { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public string Designation { get; set; }
    }

    public class RegisterAdministratorViewModel : AdministratorViewModel
    {

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }

    public class ManageAdministratorViewModel : AdministratorViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Id { get; set; }
    }




}
