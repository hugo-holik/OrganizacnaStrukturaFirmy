using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStrukturaFirmy.Dto;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDivisionRepository _divisionRepository;
        private readonly IMapper _mapper;

        public ProjectController(IProjectRepository projectRepository, IEmployeeRepository employeeRepository, IDivisionRepository divisionRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
            _divisionRepository = divisionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Project>))]
        public IActionResult GetProjects()
        {
            var projectsDto = _mapper.Map<List<ProjectDto>>(_projectRepository.GetProjects());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(projectsDto);
        }

        [HttpGet("{projectId}")]
        public IActionResult GetProject(int projectId)
        {
            Project? project = _projectRepository.GetProject(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var projectDto = _mapper.Map<ProjectDto>(project);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(projectDto);
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectDto projectCreateDto)
        {
            if (projectCreateDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (projectCreateDto.DivisionId != null)
            {
                if (!_divisionRepository.DivisionExists((int)projectCreateDto.DivisionId))
                {
                    ModelState.AddModelError("", "Invalid DivisionId. The referenced division does not exist.");
                    return StatusCode(404, ModelState);
                }
            }

            //tento if je validacia ci veduci projektu je zamestnancom firmy
            if (projectCreateDto.LeaderId != null)
            {
                var employee = _employeeRepository.GetEmployee((int)projectCreateDto.LeaderId);
                if (employee == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. The referenced employee does not exist.");
                    return StatusCode(404, ModelState);
                }

                if (projectCreateDto.DivisionId == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee, but current Project doesn't belong under any company");
                    return StatusCode(400, ModelState);
                }
                else
                {
                    var division = _divisionRepository.GetDivision((int)projectCreateDto.DivisionId);
                    if (division.CompanyId != employee.CompanyId)
                    {
                        ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee.");
                        return StatusCode(400, ModelState);
                    }
                }
            }

            var project = _mapper.Map<Project>(projectCreateDto);

            if (!_projectRepository.CreateProject(project))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{projectId}")]
        public IActionResult UpdateProject(int projectId, [FromBody] ProjectDto updatedProjectDto)
        {
            if (updatedProjectDto == null)
            {
                return BadRequest(ModelState);
            }
            if (projectId != updatedProjectDto.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_projectRepository.ProjectExists(projectId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (updatedProjectDto.DivisionId != null)
            {
                if (!_divisionRepository.DivisionExists((int)updatedProjectDto.DivisionId))
                {
                    ModelState.AddModelError("", "Invalid DivisionId. The referenced division does not exist.");
                    return StatusCode(404, ModelState);
                }
            }

            //tento if je validacia ci veduci oddelenia je zamestnancom firmy
            if (updatedProjectDto.LeaderId != null)
            {
                var employee = _employeeRepository.GetEmployee((int)updatedProjectDto.LeaderId);
                if (employee == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. The referenced employee does not exist.");
                    return StatusCode(404, ModelState);
                }

                if (updatedProjectDto.DivisionId == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee, but current Project doesn't belong under any company");
                    return StatusCode(400, ModelState);
                }
                else
                {
                    var division = _divisionRepository.GetDivision((int)updatedProjectDto.DivisionId);
                    if (division.CompanyId != employee.CompanyId)
                    {
                        ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee.");
                        return StatusCode(400, ModelState);
                    }
                }
            }

            var updatedProject = _mapper.Map<Project>(updatedProjectDto);

            if (!_projectRepository.UpdateProject(updatedProject))
            {
                ModelState.AddModelError("", "Something went wrong updating Project");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            Project? project = _projectRepository.GetProject(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var projectToDelete = _projectRepository.GetProject(projectId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_projectRepository.DeleteProject(projectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Project");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
