using System.ComponentModel.DataAnnotations;

namespace e_commerce_pro.Models
{
    public class CategorieS
    {
        [Key]
        public int Id { get; set; }

   
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required]

        public bool IsList { get; set; }
    }
}
