using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Employee
{
    public record EmployeeUpdateRequest( 
        string FirstName,
        string LastName,
        string Address,
        string IDNumber,
        int DaysOffNumber,
        int PositionId
        );
}

    