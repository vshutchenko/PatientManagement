﻿using AutoMapper;
using Newtonsoft.Json;
using PatientManagement.Api.ViewModels.Patient;
using PatientManagement.DataAccess.Patient;
using PatientManagement.Services.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        CreateMap<UpdatePatientViewModel, Patient>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Name.Id))
            .ForMember(dest => dest.Use, opt => opt.MapFrom(src => src.Name.Use))
            .ForMember(dest => dest.Family, opt => opt.MapFrom(src => src.Name.Family))
            .ForMember(dest => dest.Given, opt => opt.MapFrom(src => src.Name.Given))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender, true)))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

        CreateMap<CreatePatientViewModel, Patient>()
            .ForMember(dest => dest.Use, opt => opt.MapFrom(src => src.Name.Use))
            .ForMember(dest => dest.Family, opt => opt.MapFrom(src => src.Name.Family))
            .ForMember(dest => dest.Given, opt => opt.MapFrom(src => src.Name.Given))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender, true)))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));
    }
}
