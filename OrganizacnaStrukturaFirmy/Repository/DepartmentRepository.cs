using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Department> GetDepartments()
        {
            return _context.Departments.OrderBy(d => d.Id).ToList();
        }
        public bool DepartmentExists(int departmentId)
        {
            return _context.Departments.Any(d => d.Id == departmentId);
        }
        public Department? GetDepartment(int departmentId)
        {
            return _context.Departments.FirstOrDefault(d => d.Id == departmentId);
        }
        public bool CreateDepartment(Department department)
        {
            _context.Add(department);
            return Save();
        }
        public bool UpdateDepartment(Department department)
        {
            _context.Update(department);
            return Save();
        }
        public bool DeleteDepartment(Department department)
        {
            _context.Remove(department);
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
