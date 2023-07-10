using AutoMapper;
using OrganizacnaStrukturaFirmy.Dto;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();

            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();

            CreateMap<Division, DivisionDto>();
            CreateMap<DivisionDto, Division>();

            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();

        }
    }
}
