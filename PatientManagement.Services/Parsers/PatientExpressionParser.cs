using PatientManagement.DataAccess.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Services.Parsers;
internal class PatientExpressionParser : IPatientExpressionParser
{
    public bool TryParseExpression(IEnumerable<string> values,
        out Expression<Func<PatientEntity, bool>>? resultExpression)
    {
        try
        {
            var expressions = new List<Expression<Func<PatientEntity, bool>>>();

            foreach (var value in values)
            {
                var conditionStr = value.Substring(0, 2).ToLower();
                var dateStr = value.Substring(2);

                var parsedDate = DateTime.Parse(dateStr);
                var currentExpression = GetExpression(parsedDate, conditionStr);

                expressions.Add(currentExpression);
            }

            resultExpression = expressions.Aggregate((current, next) => CombineExpressions(current, next));

            return true;
        }
        catch (Exception)
        {
            resultExpression = null;
            return false;
        }
    }

    private Expression<Func<PatientEntity, bool>> GetExpression(DateTime parsedDate, string conditionStr)
    {
        bool hasTime = parsedDate.TimeOfDay != TimeSpan.Zero;

        Expression<Func<PatientEntity, bool>> condition = conditionStr switch
        {
            "eq" => patient => hasTime
                ? patient.BirthDate == parsedDate
                : patient.BirthDate.Date == parsedDate.Date,

            "ne" => patient => hasTime
                ? patient.BirthDate != parsedDate
                : patient.BirthDate.Date != parsedDate.Date,

            "gt" => patient => hasTime
                ? patient.BirthDate > parsedDate
                : patient.BirthDate.Date > parsedDate.Date,

            "lt" => patient => hasTime
                ? patient.BirthDate < parsedDate
                : patient.BirthDate.Date < parsedDate.Date,

            "ge" => patient => hasTime
                ? patient.BirthDate >= parsedDate
                : patient.BirthDate.Date >= parsedDate.Date,

            "le" => patient => hasTime
                ? patient.BirthDate <= parsedDate
                : patient.BirthDate.Date <= parsedDate.Date,

            "sa" => patient => hasTime
                ? patient.BirthDate > parsedDate
                : patient.BirthDate.Date > parsedDate.Date,

            "eb" => patient => hasTime
                ? patient.BirthDate < parsedDate
                : patient.BirthDate.Date < parsedDate.Date,

            "ap" => patient => hasTime
                ? patient.BirthDate >= parsedDate.AddDays(-1) && patient.BirthDate <= parsedDate.AddDays(1)
                : patient.BirthDate.Date >= parsedDate.Date.AddDays(-1) && patient.BirthDate.Date <= parsedDate.Date.AddDays(1),

            _ => throw new ArgumentException($"Invalid operator: {conditionStr}")
        };

        return condition;
    }

    private Expression<Func<PatientEntity, bool>> CombineExpressions(
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
