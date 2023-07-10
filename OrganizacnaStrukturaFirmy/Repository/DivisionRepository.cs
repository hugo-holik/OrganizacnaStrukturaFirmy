using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OrganizacnaStrukturaFirmy.Repository
{
    public class DivisionRepository : IDivisionRepository
    {
        private readonly DataContext _context;

        public DivisionRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Division> GetDivisions()
        {
            return _context.Divisions.OrderBy(d => d.Id).ToList();
        }
        public bool DivisionExists(int divisionId)
        {
            return _context.Divisions.Any(d => d.Id == divisionId);
        }
        public Division? GetDivision(int divisionId)
        {
            return _context.Divisions.FirstOrDefault(d => d.Id == divisionId);
        }
        public bool CreateDivision(Division division)
        {
            _context.Add(division);
            return Save();
        }
        public bool UpdateDivision(Division division)
        {
            _context.Update(division);
            return Save();
        }
        public bool DeleteDivision(Division division)
        {
            _context.Remove(division);
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
