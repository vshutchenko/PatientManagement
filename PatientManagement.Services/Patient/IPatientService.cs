using PatientManagement.DataAccess.Patient;
using System.Linq.Expressions;

namespace PatientManagement.Services.Patient;
public interface IPatientService
{
    Patient? FindPatientById(Guid id);
    Patient CreatePatient(Patient patient);
    void UpdatePatient(Patient patient);
    void DeletePatient(Guid id);
    IEnumerable<Patient> FindPatientsByExpression(Expression<Func<PatientEntity, bool>> expression);
}
