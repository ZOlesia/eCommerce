using System.ComponentModel.DataAnnotations;
namespace eCommerce.Models
{
    public class NewProductViewModel
    {
        [Required(ErrorMessage = "Name field is required")]
        [Display(Name = "Name:")]
        public string productname { get; set; }
        [Required(ErrorMessage = "Image field is required")]
        [Display(Name = "Image (url):")]
        public string image { get; set; }
        [Required(ErrorMessage = "Description field is required")]
        [Display(Name = "Description")]
        public string description { get; set; }
        [Required(ErrorMessage = "Quantity field is required")]
        [Display(Name = "Initial Quantity:")]
        public int quantity { get; set; }
    }
}