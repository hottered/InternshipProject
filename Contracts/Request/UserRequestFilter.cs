using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public class UserRequestFilter : FilterBase
    {
        public string? SearchString { get; set; }

        public string? CommentEmployee {  get; set; }

        public string? LeaveType { get; set; }
    }
}
