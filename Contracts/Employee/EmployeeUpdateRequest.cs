using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Employee
{
    public record EmployeeUpdateRequest
    (
        [Required(ErrorMessage = "ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID must be a positive integer.")]
        int Id,

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name can't be longer than 50 characters.")]
         string FirstName ,

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name can't be longer than 50 characters.")]
         string LastName ,

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(100, ErrorMessage = "Address can't be longer than 100 characters.")]
         string Address,

        [Required(ErrorMessage = "ID Number is required.")]
        [StringLength(20, ErrorMessage = "ID Number can't be longer than 20 characters.")]
         string IDNumber,

        [Required(ErrorMessage = "Days Off Number is required.")]
        [Range(0, 365, ErrorMessage = "Days Off Number must be between 0 and 365.")]
         int DaysOffNumber,

        [Required(ErrorMessage = "Position ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Position ID must be a positive integer.")]
         int PositionId
    );
}

