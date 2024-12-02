using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Contract
{
    public class UserContractViewModel
    {
        public int EmployeeId { get; set; }

        public string ContractNumber { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
