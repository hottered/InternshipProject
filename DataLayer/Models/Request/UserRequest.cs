using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Request
{
    public class UserRequest
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string LeaveType { get; set; } = default!;

        [MaxLength(512)]
        public string CommentEmployee { get; set; } = default!;

        [MaxLength(512)]
        public string CommentHR { get; set; } = default!;
        public Employee Employee { get; set; } = default!;

        [ForeignKey(nameof(Employee))]
        public int EmployeeID { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
