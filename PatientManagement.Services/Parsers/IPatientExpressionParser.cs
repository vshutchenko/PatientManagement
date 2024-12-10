using PatientManagement.DataAccess.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Services.Parsers;
public interface IPatientExpressionParser
{
    public bool TryParseExpression(IEnumerable<string> values,
        out Expression<Func<PatientEntity, bool>>? resultExpression);
}
