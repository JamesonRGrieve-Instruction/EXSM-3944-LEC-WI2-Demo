using System.ComponentModel.DataAnnotations;

namespace EXSM3944_Demo.Models
{
    public class Product
    {
        public int ID { get; set; }
        [StringLength(20, MinimumLength = 2, ErrorMessage = "That's no good!")]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
