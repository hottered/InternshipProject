using SharedDll;
using SharedDll.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public record UserRequestCreateRequest(

        [Required(ErrorMessage = Constants.DateStartRequiered)]
        DateTime StartDate,

        [Required(ErrorMessage = Constants.DateEndRequiered)]
        DateTime EndDate,

        LeaveTypeEnum LeaveType,

        string CommentEmployee,

        int UserId
        );
}
