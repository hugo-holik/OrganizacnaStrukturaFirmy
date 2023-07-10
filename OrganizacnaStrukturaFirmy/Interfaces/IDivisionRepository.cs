using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Interfaces
{
    public interface IDivisionRepository
    {
        ICollection<Division> GetDivisions();
        bool DivisionExists(int divisionId);
        Division? GetDivision(int divisionId);
        bool CreateDivision(Division division);
        bool UpdateDivision(Division division);
        bool DeleteDivision(Division division);
        bool Save();
    }
}
