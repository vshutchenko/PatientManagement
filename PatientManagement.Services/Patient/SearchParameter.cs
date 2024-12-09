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
    public Expression<Func<PatientEntity, bool>> Condition { get; set; }

    private SearchParameter(Expression<Func<PatientEntity, bool>> condition)
    {
        Condition = condition;
    }

    public static bool TryParse(IEnumerable<string> values, out SearchParameter? searchParameter)
    {
        try
        {
            var conditions = new List<Expression<Func<PatientEntity, bool>>>();

            foreach (var value in values)
            {
                var conditionStr = value.Substring(0, 2).ToLower();
                var dateStr = value.Substring(2);

                if (!DateTime.TryParse(dateStr, out DateTime parsedDate))
                {
                    throw new ArgumentException("Invalid date format");
                }

                bool hasTimeComponent = parsedDate.TimeOfDay != TimeSpan.Zero;

                Expression<Func<PatientEntity, bool>> condition = conditionStr switch
                {
                    "eq" => patient => hasTimeComponent
                        ? patient.BirthDate == parsedDate
                        : patient.BirthDate.Date == parsedDate.Date,

                    "ne" => patient => hasTimeComponent
                        ? patient.BirthDate != parsedDate
                        : patient.BirthDate.Date != parsedDate.Date,

                    "gt" => patient => hasTimeComponent
                        ? patient.BirthDate > parsedDate
                        : patient.BirthDate.Date > parsedDate.Date,

                    "lt" => patient => hasTimeComponent
                        ? patient.BirthDate < parsedDate
                        : patient.BirthDate.Date < parsedDate.Date,

                    "ge" => patient => hasTimeComponent
                        ? patient.BirthDate >= parsedDate
                        : patient.BirthDate.Date >= parsedDate.Date,

                    "le" => patient => hasTimeComponent
                        ? patient.BirthDate <= parsedDate
                        : patient.BirthDate.Date <= parsedDate.Date,

                    "sa" => patient => hasTimeComponent
                        ? patient.BirthDate > parsedDate
                        : patient.BirthDate.Date > parsedDate.Date,

                    "eb" => patient => hasTimeComponent
                        ? patient.BirthDate < parsedDate
                        : patient.BirthDate.Date < parsedDate.Date,

                    "ap" => patient => hasTimeComponent
                        ? patient.BirthDate >= parsedDate.AddDays(-1) && patient.BirthDate <= parsedDate.AddDays(1)
                        : patient.BirthDate.Date >= parsedDate.Date.AddDays(-1) && patient.BirthDate.Date <= parsedDate.Date.AddDays(1),

                    _ => throw new ArgumentException($"Invalid operator: {conditionStr}")
                };

                conditions.Add(condition);
            }



            var combinedCondition = conditions.Aggregate((current, next) => CombineExpressions(current, next));

            searchParameter = new SearchParameter(combinedCondition);
            return true;
        }
        catch (Exception)
        {
            searchParameter = null;
            return false;
        }
    }

    private static Expression<Func<PatientEntity, bool>> CombineExpressions(
        Expression<Func<PatientEntity, bool>> expr1,
        Expression<Func<PatientEntity, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(PatientEntity), "patient");

        var combined = Expression.AndAlso(
            Expression.Invoke(expr1, parameter),
            Expression.Invoke(expr2, parameter));

        return Expression.Lambda<Func<PatientEntity, bool>>(combined, parameter);
    }
}
