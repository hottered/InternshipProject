using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Employee
{
    public class EmployeeFilter : FilterBase
    {
        public string? SearchString { get; set; }
    }
}
