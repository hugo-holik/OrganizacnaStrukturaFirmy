using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStrukturaFirmy.Dto;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : Controller
    {
        private readonly IDivisionRepository _divisionRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public DivisionController(IDivisionRepository divisionRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _divisionRepository = divisionRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDivisions()
        {
            var divisionsDto = _mapper.Map<List<DivisionDto>>(_divisionRepository.GetDivisions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(divisionsDto);
        }

        [HttpGet("{divisionId}")]
        public IActionResult GetDivision(int divisionId)
        {
            Division? division = _divisionRepository.GetDivision(divisionId);
            if (division == null)
            {
                return NotFound();
            }

            var divisionDto = _mapper.Map<DivisionDto>(division);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(divisionDto);
        }

        [HttpPost]
        public IActionResult CreateDivision([FromBody] DivisionDto divisionCreateDto)
        {
            if (divisionCreateDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (divisionCreateDto.LeaderId != null)
            {
                var employee = _employeeRepository.GetEmployee((int)divisionCreateDto.LeaderId);
                if (employee == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. The referenced employee does not exist.");
                    return StatusCode(404, ModelState);
                }
                if (employee.CompanyId != divisionCreateDto.CompanyId)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee.");
                    return StatusCode(400, ModelState);
                }
            }

            var division = _mapper.Map<Division>(divisionCreateDto);

            if (!_divisionRepository.CreateDivision(division))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{divisionId}")]
        public IActionResult UpdateDivision(int divisionId, [FromBody] DivisionDto updatedDivisionDto)
        {
            if (updatedDivisionDto == null)
            {
                return BadRequest(ModelState);
            }
            if (divisionId != updatedDivisionDto.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_divisionRepository.DivisionExists(divisionId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (updatedDivisionDto.LeaderId != null)
            {
                var employee = _employeeRepository.GetEmployee((int)updatedDivisionDto.LeaderId);
                if (employee == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. The referenced employee does not exist.");
                    return StatusCode(404, ModelState);
                }
                if (employee.CompanyId != updatedDivisionDto.CompanyId)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee.");
                    return StatusCode(400, ModelState);
                }
            }

            var updatedDivision = _mapper.Map<Division>(updatedDivisionDto);

            if (!_divisionRepository.UpdateDivision(updatedDivision))
            {
                ModelState.AddModelError("", "Something went wrong updating division");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{divisionId}")]
        public IActionResult DeleteDivision(int divisionId)
        {
            Division? division = _divisionRepository.GetDivision(divisionId);
            if (division == null)
            {
                return NotFound();
            }

            var divisionToDelete = _divisionRepository.GetDivision(divisionId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_divisionRepository.DeleteDivision(divisionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting division");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }
    }
}
