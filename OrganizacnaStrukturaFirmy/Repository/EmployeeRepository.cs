using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Employee> GetEmployees() 
        {
            return _context.Employees.OrderBy(e => e.Id).ToList();
        }

        public bool EmployeeExists(int employeeId)
        {
            return _context.Employees.Any(e => e.Id == employeeId);
        }

        public Employee? GetEmployee(int employeeId)
        {
            return _context.Employees.FirstOrDefault(e => e.Id == employeeId);
        }

        public bool CreateEmployee(Employee employee)
        {
            _context.Add(employee);
            return Save();
        }

        public bool UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
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