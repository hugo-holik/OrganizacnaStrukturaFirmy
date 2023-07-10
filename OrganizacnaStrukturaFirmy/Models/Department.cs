namespace OrganizacnaStrukturaFirmy.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LeaderId { get; set; }
        public Employee? Leader { get; set; }

        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
