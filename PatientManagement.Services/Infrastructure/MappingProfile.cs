using AutoMapper;
using Newtonsoft.Json;
using PatientManagement.DataAccess.Patient;
using PatientManagement.Services.Patient;

namespace PatientManagement.Services.Infrastructure;
internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient.Patient, PatientEntity>()
            .ForMember(dest => dest.Given, opt => opt.MapFrom(
                src => src.Given != null ? JsonConvert.SerializeObject(src.Given) : null))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(
                src => src.Gender.ToString().ToLower()));

        CreateMap<PatientEntity, Patient.Patient>()
            .ForMember(dest => dest.Given, opt => opt.MapFrom(
                src => !string.IsNullOrEmpty(src.Given)
                ? JsonConvert.DeserializeObject<List<string>>(src.Given)
                : null))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(
                src => !string.IsNullOrEmpty(src.Gender)
                ? Enum.Parse<Gender>(src.Gender, true)
                : (Gender?)null));
    }
}
