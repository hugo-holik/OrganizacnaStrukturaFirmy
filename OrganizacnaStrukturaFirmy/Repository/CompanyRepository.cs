using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Company> GetCompanies()
        {
            return _context.Companies.OrderBy(c => c.Id).ToList();
        }

        public bool CompanyExists(int companyId)
        {
            return _context.Companies.Any(c => c.Id == companyId);
        }
        public Company? GetCompany(int companyId)
        {
            return _context.Companies.FirstOrDefault(c => c.Id == companyId);
        }

        public bool CreateCompany(Company company)
        {
            _context.Add(company);
            return Save();
        }
        public bool UpdateCompany(Company company)
        {
            _context.Update(company);
            return Save();
        }

        public bool DeleteCompany(Company company)
        {
            _context.Remove(company);
            return Save();
        }
        public bool Save()
        {
            try
            {
                var saved = _context.SaveChanges();
                return saved > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
