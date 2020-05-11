using System.ComponentModel.DataAnnotations;

namespace twitterWebUi.ViewModel
{
    public class RegisterVM
    {   
        [Required]
        [Display(Name="Username")]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
        [Required]
        [Compare(nameof(Password),ErrorMessage="Password Mismatch")]
        public string ConfirmPassword { get; set; }
    }
}