using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Employee
{
    public record EmployeeGetResponse(
            string FirstName,
            string LastName,
            string Address,
            string IDNumber,
            int DaysOffNumber,
            int PositionID,
            //dodaj requests dto:
            DateTime EmploymentStartDate,
            DateTime EmploymentEndDate
        );
}
