using AutoMapper;
using PatientManagement.Api.ViewModels.Patient;
using PatientManagement.Services.Patient;

namespace PatientManagement.Api.Infrastructure;
internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new PatientNameViewModel
            {
                Id = src.Id,
                Use = src.Use,
                Family = src.Family,
                Given = src.Given
            }))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString().ToLower()));

        CreateMap<PatientViewModel, Patient>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Name.Id))
            .ForMember(dest => dest.Use, opt => opt.MapFrom(src => src.Name.Use))
            .ForMember(dest => dest.Family, opt => opt.MapFrom(src => src.Name.Family))
            .ForMember(dest => dest.Given, opt => opt.MapFrom(src => src.Name.Given))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender, true)))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));
    }
}
