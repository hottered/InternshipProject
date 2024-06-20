using SharedDll.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public record UserRequestGetResponse(
        int Id,
        string EmployeeFirstName,
        DateTime StartDate,
        DateTime EndDate,
        LeaveTypeEnum LeaveType,
        RequestApprovalEnum Approval,
        int UserId
        );
}
