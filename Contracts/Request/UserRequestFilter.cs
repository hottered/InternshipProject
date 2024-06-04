using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Enums;
using Contracts.Filter;

namespace Contracts.Request
{
    public class UserRequestFilter : FilterBase
    {
        public string? SearchString { get; set; }

        public LeaveTypeEnum? LeaveType { get; set; }
    }
}
