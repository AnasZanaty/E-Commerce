 using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Data.ViewModel
{
    public class LoginVM //FOR taking only two from user identity
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "This Field is Reqired!")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "This Field is Reqired!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}