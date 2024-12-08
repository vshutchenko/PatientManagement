using PatientManagement.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.DataAccess.Patient;
internal class PatientRepository : IPatientRepository
{
    private PatientDbContext _context;

    public PatientRepository(PatientDbContext context)
    {
        _context = context;
    }

    public void Create(PatientEntity patient)
    {
        _context.Add(patient);
    }

    public void Delete(Guid id)
    {
        var patientToDelete = _context.Patients.FirstOrDefault(p => p.Id == id);
        if (patientToDelete != null)
        {
            _context.Remove(patientToDelete);
        }
    }

    public IEnumerable<PatientEntity> FindByCondition(Expression<Func<PatientEntity, bool>> expression)
    {
        return _context.Patients.Where(expression).ToList();
    }

    public void Update(PatientEntity patient)
    {
        _context.Update(patient);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
