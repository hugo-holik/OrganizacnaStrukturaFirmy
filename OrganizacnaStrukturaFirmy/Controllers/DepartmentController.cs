using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStrukturaFirmy.Dto;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDivisionRepository _divisionRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository, IDivisionRepository divisionRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _divisionRepository = divisionRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departmentsDto = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(departmentsDto);
        }

        [HttpGet("{departmentId}")]
        public IActionResult GetDepartment(int departmentId)
        {
            Department? department = _departmentRepository.GetDepartment(departmentId);
            if (department == null)
            {
                return NotFound();
            }

            var departmentDto = _mapper.Map<DepartmentDto>(department);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(departmentDto);
        }

        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDto departmentCreateDto)
        {
            if (departmentCreateDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (departmentCreateDto.ProjectId != null)
            {
                if (!_projectRepository.ProjectExists((int)departmentCreateDto.ProjectId))
                {
                    ModelState.AddModelError("", "Invalid ProjectId. The referenced project does not exist.");
                    return StatusCode(404, ModelState);
                }
            }

            //tento if je validacia ci veduci oddelenia je zamestnancom firmy
            if (departmentCreateDto.LeaderId != null)
            {
                var employee = _employeeRepository.GetEmployee((int)departmentCreateDto.LeaderId);
                if (employee == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. The referenced employee does not exist.");
                    return StatusCode(404, ModelState);
                }

                if (departmentCreateDto.ProjectId == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee, but current Department doesn't belong under any company.");
                    return StatusCode(400, ModelState);
                }
                else
                {
                    var project = _projectRepository.GetProject((int)departmentCreateDto.ProjectId);

                    if (project.DivisionId == null)
                    {
                        ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee, but current Department doesn't belong under any company.");
                        return StatusCode(400, ModelState);
                    }
                    else
                    {
                        var division = _divisionRepository.GetDivision((int)project.DivisionId);

                        if (division.CompanyId == null)
                        {
                            ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee, but current Department doesn't belong under any company.");
                            return StatusCode(400, ModelState);
                        }
                        else
                        {
                            if (employee.CompanyId != division.CompanyId)
                            {
                                ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee.");
                                return StatusCode(400, ModelState);
                            }
                        }
                    }
                }
            }

            var department = _mapper.Map<Department>(departmentCreateDto);

            if (!_departmentRepository.CreateDepartment(department))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{departmentId}")]
        public IActionResult UpdateDepartment(int departmentId, [FromBody] DepartmentDto updatedDepartmentDto)
        {
            if (updatedDepartmentDto == null)
            {
                return BadRequest(ModelState);
            }
            if (departmentId != updatedDepartmentDto.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_departmentRepository.DepartmentExists(departmentId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (updatedDepartmentDto.ProjectId != null)
            {
                if (!_projectRepository.ProjectExists((int)updatedDepartmentDto.ProjectId))
                {
                    ModelState.AddModelError("", "Invalid ProjectId. The referenced project does not exist.");
                    return StatusCode(404, ModelState);
                }
            }

            //tento if je validacia ci veduci oddelenia je zamestnancom firmy
            if (updatedDepartmentDto.LeaderId != null)
            {
                var employee = _employeeRepository.GetEmployee((int)updatedDepartmentDto.LeaderId);
                if (employee == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. The referenced employee does not exist.");
                    return StatusCode(404, ModelState);
                }

                if (updatedDepartmentDto.ProjectId == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee, but current Department doesn't belong under any company.");
                    return StatusCode(400, ModelState);
                }
                else
                {
                    var project = _projectRepository.GetProject((int)updatedDepartmentDto.ProjectId);

                    if (project.DivisionId == null)
                    {
                        ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee, but current Department doesn't belong under any company.");
                        return StatusCode(400, ModelState);
                    }
                    else
                    {
                        var division = _divisionRepository.GetDivision((int)project.DivisionId);

                        if (division.CompanyId == null)
                        {
                            ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee, but current Department doesn't belong under any company.");
                            return StatusCode(400, ModelState);
                        }
                        else
                        {
                            if (employee.CompanyId != division.CompanyId)
                            {
                                ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee.");
                                return StatusCode(400, ModelState);
                            }
                        }
                    }
                }
            }

            var updatedDepartment = _mapper.Map<Department>(updatedDepartmentDto);

            if (!_departmentRepository.UpdateDepartment(updatedDepartment))
            {
                ModelState.AddModelError("", "Something went wrong updating Department");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartment(int departmentId)
        {
            Department? department = _departmentRepository.GetDepartment(departmentId);
            if (department == null)
            {
                return NotFound();
            }

            var departmentToDelete = _departmentRepository.GetDepartment(departmentId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_departmentRepository.DeleteDepartment(departmentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Department");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
