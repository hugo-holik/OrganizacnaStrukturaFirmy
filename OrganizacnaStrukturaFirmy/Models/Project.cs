namespace OrganizacnaStrukturaFirmy.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LeaderId { get; set; }
        public Employee? Leader { get; set; }

        public int? DivisionId { get; set; }
        public Division? Division { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}
