using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Contract
{
    public class UserContract
    {
        public int Id { get; set; }

        public string ContractNumber { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    }
}
