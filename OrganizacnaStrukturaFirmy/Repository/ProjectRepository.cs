using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Project> GetProjects()
        {
            return _context.Projects.OrderBy(p => p.Id).ToList();
        }
        public bool ProjectExists(int projectId)
        {
            return _context.Projects.Any(p => p.Id == projectId);
        }
        public Project? GetProject(int projectId)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == projectId);
        }
        public bool CreateProject(Project project)
        {
            _context.Add(project);
            return Save();
        }
        public bool UpdateProject(Project project)
        {
            _context.Update(project);
            return Save();
        }
        public bool DeleteProject(Project project)
        {
            _context.Remove(project);
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
