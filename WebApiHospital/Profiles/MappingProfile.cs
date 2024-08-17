using AutoMapper;
using WebApiHospital.DLL.Entites;
using WebApiHospital.Dtos;

namespace WebApiHospital.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore()) 
                .ReverseMap();

            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ReverseMap();
        }
    }

}
