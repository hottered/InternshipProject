using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Position
{
    public class UserPosition
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string ?Caption { get; set; } = default!;

        [MaxLength(512)]
        public string ?Description { get; set; } = default!;

        public List<Employee>? Employee { get; set; } = default!;
    
        public bool IsDeleted { get; set; } = false;
    }
}
