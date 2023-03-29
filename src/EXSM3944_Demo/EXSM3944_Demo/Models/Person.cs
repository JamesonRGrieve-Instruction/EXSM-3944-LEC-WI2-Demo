using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXSM3944_Demo.Models
{
    public class Person
    {
        public int ID { get; set; }
        [ValidateNever]
        public string UserID { get; set; }
        public int JobID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ValidateNever]
        public virtual Job Job { get; set; }

    }
}
