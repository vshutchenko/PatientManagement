using AutoMapper;
using PatientManagement.DataAccess.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Services.Patient;
internal class PatientService : IPatientService
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public Patient? FindPatientById(Guid id)
    {
        var patientEntity = _patientRepository.FindById(id);

        if (patientEntity == null)
        {
            return null;
        }

        return _mapper.Map<Patient>(patientEntity);
    }

    public Patient CreatePatient(Patient patient)
    {
        var patientEntity = _mapper.Map<PatientEntity>(patient);
        var createdEntity = _patientRepository.Create(patientEntity);

        return _mapper.Map<Patient>(createdEntity);
    }

    public void UpdatePatient(Patient patient)
    {
        var patientEntity = _mapper.Map<PatientEntity>(patient);
        _patientRepository.Update(patientEntity);
    }

    public void DeletePatient(Guid id)
    {
        _patientRepository.Delete(id);
    }

    public IEnumerable<Patient> FindPatientsByDate(SearchParameter searchParameter)
    {
        var patientEntities = _patientRepository.FindByCondition(searchParameter.Condition);
        return _mapper.Map<IEnumerable<Patient>>(patientEntities);
    }
}
