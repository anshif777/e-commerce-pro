using System.ComponentModel.DataAnnotations;

namespace e_commerce_pro.Models
{
    public class UserSingup
        {
            [Key]
            public int Id { get; set; }
        //[Required(ErrorMessage = "Name must be 6 charecters")]
        // [StringLength(6)]
            [Required]
             [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "name should contain only letters")]
            public  string Name { get; set; }
             [Required]
            public  string Email { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 digits")]

        public string PhoneNumber { get; set; }
            [Required]
            public  string Password { get; set; }
           public bool IsBlock { get; set; }

    }
    }
