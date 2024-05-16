using DataLayer.Models;
using DataLayer.Models.Position;
using DataLayer.Models.Request;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Employee : IdentityUser<int>
    {
        [MaxLength(50)]
        public string? FirstName { get; set; } = default!;

        [MaxLength(50)]
        public string? LastName { get; set; } = default!;

        [MaxLength(512)]
        public string? Address { get; set; } = default!;

        [MaxLength(50)]
        public string? IDNumber { get; set; } = default!;

        public int? DaysOffNumber { get; set; } = default!;

        public bool ? IsDeleted { get; set; } = false;
        public UserPosition? Position { get; set; } = null!;

        [ForeignKey(nameof(Position))]
        public int? PositionId { get; set; } = null!;
        public List<UserRequest>? Request { get; set; } = new();
        public DateTime EmploymentStartDate { get; set; } = DateTime.UtcNow;

        public DateTime? EmploymentEndDate { get; set; } = DateTime.UtcNow;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime? DateDeleted { get; set; } = DateTime.UtcNow;
    }
}
