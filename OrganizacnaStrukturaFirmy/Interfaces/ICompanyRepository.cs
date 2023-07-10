using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        bool CompanyExists(int companyId);
        Company? GetCompany(int companyId);
        bool CreateCompany(Company company);
        bool UpdateCompany(Company company);
        bool DeleteCompany(Company company);
        bool Save();
    }
}
