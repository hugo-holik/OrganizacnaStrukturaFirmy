using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizacnaStrukturaFirmy.Dto;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(employees);
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetEmployee(int employeeId)
        {
            Employee? employee = _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(employeeDto);
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeCreateDto)
        {
            if (employeeCreateDto == null) 
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (employeeCreateDto.CompanyId != null)
            {
                if (!_companyRepository.CompanyExists((int)employeeCreateDto.CompanyId))
                {
                    ModelState.AddModelError("", "Invalid CompanyId. The referenced company does not exist.");
                    return StatusCode(400, ModelState);
                }
            }

            var employeeCreate = _mapper.Map<Employee>(employeeCreateDto);


            if (!_employeeRepository.CreateEmployee(employeeCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(int employeeId, [FromBody]EmployeeDto updatedEmployeeDto)
        {
            if (updatedEmployeeDto == null)
            {
                return BadRequest(ModelState);
            }
            if (employeeId != updatedEmployeeDto.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_employeeRepository.EmployeeExists(employeeId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (updatedEmployeeDto.CompanyId != null)
            {
                if (!_companyRepository.CompanyExists((int)updatedEmployeeDto.CompanyId))
                {
                    ModelState.AddModelError("", "Invalid CompanyId. The referenced company does not exist.");
                    return StatusCode(400, ModelState);
                }
            }

            var updatedEmployee = _mapper.Map<Employee>(updatedEmployeeDto);


            if (!_employeeRepository.UpdateEmployee(updatedEmployee))
            {
                ModelState.AddModelError("", "Something went wrong updating employee");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            Employee? employee = _employeeRepository.GetEmployee(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeToDelete = _employeeRepository.GetEmployee(employeeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_employeeRepository.DeleteEmployee(employeeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting employee");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }


    }
}
