using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LeaderId { get; set; }
        public int? ProjectId { get; set; }
    }
}
