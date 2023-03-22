using System.ComponentModel.DataAnnotations;

namespace EXSM3944_Demo.Models
{
    public class Vehicle
    {
        [Required]
        [StringLength(17, MinimumLength = 17)]
        // TODO: Regular Expression
        public string VIN { get; set; }
        public string UserID { get; set; }
        [Required]
        [Range(1900, 2050)]
        public int ModelYear { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        // TODO: Regular Expression
        public string Manufacturer { get; set; }
        [Required]
        [StringLength(50)]
        public string Model { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Colour { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        public DateTime? SaleDate { get; set; }

    }
}
