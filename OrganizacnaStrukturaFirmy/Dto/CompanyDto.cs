using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Dto
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LeaderId { get; set; }
    }
}
