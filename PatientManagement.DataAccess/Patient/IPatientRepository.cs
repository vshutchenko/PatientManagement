﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.DataAccess.Patient;
public interface IPatientRepository
{
    public void Delete(Guid id);
    public PatientEntity Create(PatientEntity patient);
    public void Update(PatientEntity patient);
    public PatientEntity? FindById(Guid id);
    public IEnumerable<PatientEntity> FindByCondition(Expression<Func<PatientEntity, bool>> expression);
}
