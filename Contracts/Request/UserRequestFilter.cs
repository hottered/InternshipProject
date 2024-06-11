using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Filter;
using SharedDll.Enums;

namespace Contracts.Request
{
    public class UserRequestFilter : FilterBase
    {
        public LeaveTypeEnum? LeaveType { get; set; }
    }
}
