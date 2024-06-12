using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Filter
{
    public class FilterBase
    {
        public string? Sort { get; set; }

        public int? PageSize { get; set; } = 3;

        public int? PageNumber { get; set; } = 1;

        public string? SearchString { get; set; }

    }
}
