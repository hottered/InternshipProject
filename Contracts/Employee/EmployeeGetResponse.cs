using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Employee
{
    public record EmployeeGetResponse(
            int Id,
            string FirstName,
            string? LastName,
            string? Address,
            string? IDNumber,
            int? DaysOffNumber,
            int? PositionID,
            DateTime? EmploymentStartDate,
            DateTime? EmploymentEndDate
        );
}
