using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace E_Commerce.Data.ViewModel
{
    public class RegisterVM 
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "This Field is Reqired!")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "This Field is Reqired!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "This Field is Reqired!")]
        [Compare(nameof(Password), ErrorMessage = "Not Identical!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
