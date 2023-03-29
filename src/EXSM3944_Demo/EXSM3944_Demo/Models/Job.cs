using System.ComponentModel.DataAnnotations.Schema;

namespace EXSM3944_Demo.Models
{
    public class Job
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public virtual IEnumerable<Person> People { get; set; } 
    }
}
