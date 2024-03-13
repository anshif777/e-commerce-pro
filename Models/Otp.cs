using System.ComponentModel.DataAnnotations;

namespace e_commerce_pro.Models
{
    public class Otp
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public required string OTp { get; set; }

        public DateTime expertime { get; set; }
    }
}
