using PatientManagement.DataAccess.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatientManagement.Services.Patient;
public class SearchParameter
{
    private const string ParsingErrorMessage = "Invalid search parameter.";

    public Expression<Func<PatientEntity, bool>> Condition { get; set; }

    private SearchParameter(Expression<Func<PatientEntity, bool>> condition)
    {
        Condition = condition;
    }

    public static SearchParameter Parse(string value)
    {
        try
        {
            var conditionStr = value.Substring(0, 2).ToLower();
            var dateStr = value.Substring(2);
            var parsedDate = DateTime.Parse(dateStr);

            Expression<Func<PatientEntity, bool>> condition = conditionStr switch
            {
                "eq" => patient => patient.BirthDate == parsedDate,
                "ne" => patient => patient.BirthDate != parsedDate,
                "gt" => patient => patient.BirthDate > parsedDate,
                "lt" => patient => patient.BirthDate >= parsedDate,
                "ge" => patient => patient.BirthDate < parsedDate,
                "le" => patient => patient.BirthDate <= parsedDate,
                "sa" => patient => patient.BirthDate > parsedDate,
                "eb" => patient => patient.BirthDate < parsedDate,
                "ap" => patient => patient.BirthDate >= parsedDate.AddDays(-1) && patient.BirthDate <= parsedDate.AddDays(1),
                _ => throw new ArgumentException()
            };

            return new SearchParameter(condition);
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ParsingErrorMessage, ex);
        }
    }
}
