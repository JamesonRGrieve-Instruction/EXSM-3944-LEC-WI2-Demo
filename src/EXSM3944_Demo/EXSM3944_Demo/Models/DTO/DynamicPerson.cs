namespace EXSM3944_Demo.Models.DTO
{
    public class DynamicPerson
    {
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public int PersonJobID { get; set; }
        public string? JobName { get; set; }
        public string? JobDescription { get; set; }
        public int JobIndustryID { get; set; }
        public string? IndustryName { get; set; }
        public string? IndustryDescription { get; set; }

    }
}
