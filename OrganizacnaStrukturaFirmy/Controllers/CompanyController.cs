using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStrukturaFirmy.Dto;
using OrganizacnaStrukturaFirmy.Interfaces;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companiesDto = _mapper.Map<List<CompanyDto>>(_companyRepository.GetCompanies());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(companiesDto);
        }

        [HttpGet("{companyId}")]
        public IActionResult GetCompany(int companyId)
        {
            Company? company = _companyRepository.GetCompany(companyId);
            if (company == null)
            {
                return NotFound();
            }

            var companyDto = _mapper.Map<CompanyDto>(company);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(companyDto);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyDto companyCreateDto)
        {
            if (companyCreateDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (companyCreateDto.LeaderId != null)
            {
                var employee = _employeeRepository.GetEmployee((int)companyCreateDto.LeaderId);
                if (employee == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. The referenced employee does not exist.");
                    return StatusCode(404, ModelState);
                }
                if (employee.CompanyId != companyCreateDto.Id)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee.");
                    return StatusCode(400, ModelState);
                }
            }

            var company = _mapper.Map<Company>(companyCreateDto);

            if (!_companyRepository.CreateCompany(company))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{companyId}")]
        public IActionResult UpdateCompany(int companyId, [FromBody] CompanyDto updatedCompanyDto)
        {
            if (updatedCompanyDto == null)
            {
                return BadRequest(ModelState);
            }
            if (companyId != updatedCompanyDto.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_companyRepository.CompanyExists(companyId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (updatedCompanyDto.LeaderId != null)
            {
                var employee = _employeeRepository.GetEmployee((int)updatedCompanyDto.LeaderId);
                if (employee == null)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. The referenced employee does not exist.");
                    return StatusCode(404, ModelState);
                }
                if (employee.CompanyId != updatedCompanyDto.Id)
                {
                    ModelState.AddModelError("", "Invalid LeaderId. Leader must be the company's employee.");
                    return StatusCode(400, ModelState);
                }
            }

            var updatedCompany = _mapper.Map<Company>(updatedCompanyDto);

            if (!_companyRepository.UpdateCompany(updatedCompany))
            {
                ModelState.AddModelError("", "Something went wrong updating company");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{companyId}")]
        public IActionResult DeleteCompany(int companyId)
        {
            Company? company = _companyRepository.GetCompany(companyId);
            if (company == null)
            {
                return NotFound();
            }

            var companyToDelete = _companyRepository.GetCompany(companyId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_companyRepository.DeleteCompany(companyToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting company");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted");
        }


    }
}
