using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce_pro.Models
{
    public class Products
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public bool Islisted { get; set; }

        public int Stock {  get; set; }

        public int CategaryId { get; set; }

        [ForeignKey("CategaryId")]
        [ValidateNever]
        public CategorieS CategorieS { get; set; }

        [ValidateNever]
        public List<string> imagUrl { get; set; }
    }
}
