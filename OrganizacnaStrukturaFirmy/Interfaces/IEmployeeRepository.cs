using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();
        bool EmployeeExists(int employeeId);
        Employee? GetEmployee(int employeeId);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(Employee employee);
        bool Save();
    }
}
