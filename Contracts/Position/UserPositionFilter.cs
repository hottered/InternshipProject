using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Filter;

namespace Contracts.Position
{
    public class UserPositionFilter : FilterBase
    {
        public string? SearchString { get; set; }
    }
}
