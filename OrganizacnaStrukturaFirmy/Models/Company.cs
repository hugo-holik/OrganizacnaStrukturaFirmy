namespace OrganizacnaStrukturaFirmy.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LeaderId { get; set; }
        public Employee? Leader { get; set; }

        public ICollection<Division> Divisions { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
