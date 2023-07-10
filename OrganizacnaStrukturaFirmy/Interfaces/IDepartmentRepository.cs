using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Interfaces
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetDepartments();
        bool DepartmentExists(int departmentId);
        Department? GetDepartment(int departmentId);
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(Department department);
        bool Save();
    }
}
