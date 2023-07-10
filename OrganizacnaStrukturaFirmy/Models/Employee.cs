namespace OrganizacnaStrukturaFirmy.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Degree { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //objekty do ktorych vstupuje Employee ako veduci (FK: LeaderId)
        public ICollection<Company> Companies { get; set; }
        public ICollection<Division> Divisions { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<Department> Departments { get; set; }

        //firma v ktorej pracuje
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
