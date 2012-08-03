using System.ComponentModel.DataAnnotations;

namespace Demo.Web.Models
{
    public class LoginModel
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
    }
}