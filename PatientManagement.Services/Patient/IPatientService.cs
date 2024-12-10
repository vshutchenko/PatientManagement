using PatientManagement.DataAccess.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Services.Patient;
public interface IPatientService
{
    Patient? FindPatientById(Guid id);
    Patient CreatePatient(Patient patient);
    void UpdatePatient(Patient patient);
    void DeletePatient(Guid id);
    IEnumerable<Patient> FindPatientsByExpression(Expression<Func<PatientEntity, bool>> expression);
}
