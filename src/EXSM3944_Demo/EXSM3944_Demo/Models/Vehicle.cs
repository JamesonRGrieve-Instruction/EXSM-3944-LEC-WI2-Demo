using System.ComponentModel.DataAnnotations;

namespace EXSM3944_Demo.Models
{
    public class Vehicle
    {
        [Required]
        [StringLength(17, MinimumLength = 17)]
        //https://stackoverflow.com/questions/30314850/vin-validation-regex
        [RegularExpression(@"^[A-HJ-NPR-Za-hj-npr-z\d]{8}[\dX][A-HJ-NPR-Za-hj-npr-z\d]{2}\d{6}$")]
        public string VIN { get; set; }
        public string UserID { get; set; }
        [Required]
        [Range(1900, 2050)]
        public int ModelYear { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"[A-Za-Z- ]{3,}")]
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
