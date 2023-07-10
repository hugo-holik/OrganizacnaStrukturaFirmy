using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<Project> GetProjects();
        bool ProjectExists(int projectId);
        Project? GetProject(int projectId);
        bool CreateProject(Project project);
        bool UpdateProject(Project project);
        bool DeleteProject(Project project);
        bool Save();
    }
}
