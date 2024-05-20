using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Employee
{
    public record EmployeeCreateRequest(
        string FirstName,
        string LastName,
        string Address,
        string IdNumber,
        int PositionID,
        string Email,
        string Password
        );
}
