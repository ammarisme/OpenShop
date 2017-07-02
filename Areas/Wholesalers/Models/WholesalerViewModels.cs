using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RetailShop.Models;
namespace RetailShop.Areas.Wholesalers.Models
{
    public class WholesalerViewModel 
    {

        public string WholesalerName { get; set; }

        public string WholesalerAddress { get; set; }

        public float Rating { get; set; }

        public string BusinessPhoneNumber { get; set; }

        public string Status { get; set; }

        public string BRCNumber { get; set; }

        public string Category { get; set; }

        public string Currency { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }
    }

    public class RegisterWholesalerViewModel : WholesalerViewModel
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

    public class ManageWholesalerViewModel : WholesalerViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Id { get; set; }
    }

    public class ShopViewModel
    {
        public Wholesaler Wholesaler { get; set; }
        public IEnumerable<Product> ProductId { get; set; }
    }

    public class PendingConfirmationWholesalerViewModel
    {
        public IEnumerable<Wholesaler> PendingWholesalers { get; set; }
    }

    public class RegisteredWholesalersViewModel
    {
        public IEnumerable<Wholesaler> RegisteredWholesalers { get; set; }
    }

}
