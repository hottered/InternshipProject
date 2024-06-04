using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public record UserRequestCreateRequest(
        DateTime StartDate,
        DateTime EndDate,
        LeaveTypeEnum LeaveType,
        string CommentEmployee,
        int UserId
        );
}
