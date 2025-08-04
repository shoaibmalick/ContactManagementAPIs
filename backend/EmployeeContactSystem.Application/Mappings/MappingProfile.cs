using AutoMapper;
using EmployeeContactSystem.Application.DTO;
using EmployeeContactSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeContactSystem.Application.Mappings
{
    /// <summary>
    /// AutoMapper configuration profile to map between entities and DTOs.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to DTO
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyName));

            CreateMap<Company, CompanyDto>();

            // DTO to Entity
            CreateMap<CreateEmployeeDto, Employee>();

            CreateMap<UpdateEmployeeDto, Employee>()
            .ForMember(dest => dest.CompanyID, opt => opt.Condition(src => src.CompanyID.HasValue))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateEmployeeDto, Employee>()
            .ForMember(dest => dest.CompanyID, opt => opt.Condition(src => src.CompanyID.HasValue))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
