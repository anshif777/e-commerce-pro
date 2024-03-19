using System.ComponentModel.DataAnnotations;

namespace e_commerce_pro.Models
{
    public class UserSingup
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name should contain only letters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format. It must be 10 digits.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        public bool IsBlock { get; set; }
    }
}

