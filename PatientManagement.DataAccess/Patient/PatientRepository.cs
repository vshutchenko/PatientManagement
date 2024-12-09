using Microsoft.EntityFrameworkCore;
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

    public PatientEntity Create(PatientEntity patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();

        return patient;
    }

    public void Delete(Guid id)
    {
        var patientToDelete = _context.Patients.FirstOrDefault(p => p.Id == id);
        if (patientToDelete != null)
        {
            _context.Patients.Remove(patientToDelete);
        }

        _context.SaveChanges();
    }

    public IEnumerable<PatientEntity> FindByCondition(Expression<Func<PatientEntity, bool>> expression)
    {
        return _context.Patients.AsNoTracking().Where(expression).ToList();
    }

    public void Update(PatientEntity patient)
    {
        _context.Patients.Update(patient);
        _context.SaveChanges();
    }

    public PatientEntity? FindById(Guid id)
    {
        return _context.Patients.AsNoTracking().FirstOrDefault(p => p.Id == id);
    } 
}
