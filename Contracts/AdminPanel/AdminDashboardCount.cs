using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.AdminPanel
{
    public class AdminDashboardCount
    {
        public long LeaveRequests { get; set; }
        public long Employees { get; set; }
        public long LeaveRequestsOnWait { get; set; }
    }
}
