using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EXSM3944_Demo.Models
{
    public class Industry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ValidateNever]
        public virtual IEnumerable<Job> Jobs { get; set; }
    }
}
