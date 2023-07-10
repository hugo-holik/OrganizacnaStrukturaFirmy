namespace OrganizacnaStrukturaFirmy.Models
{
    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LeaderId { get; set; }
        public Employee? Leader { get; set; }

        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
