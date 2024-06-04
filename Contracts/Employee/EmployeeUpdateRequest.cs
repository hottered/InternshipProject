using SharedDll;
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
        [Required(ErrorMessage = Constants.IdRequired)]
        [Range(1, int.MaxValue, ErrorMessage = Constants.IdRangeErrorMessage)]
        int Id,

        [Required(ErrorMessage = Constants.FirstNameRequired)]
        [StringLength(50, ErrorMessage = Constants.FirstNameErrorMessage)]
        string FirstName,

        [Required(ErrorMessage = Constants.LastNameRequired)]
        [StringLength(50, ErrorMessage = Constants.LastNameErrorMessage)]
        string LastName,

        [Required(ErrorMessage = Constants.AddressRequired)]
        [StringLength(100, ErrorMessage = Constants.AddressErrorMessage)]
        string Address,

        [Required(ErrorMessage = Constants.IdNumberRequired)]
        [StringLength(20, ErrorMessage = Constants.IdNumberErrorMessage)]
        string IDNumber,

        [Required(ErrorMessage = Constants.DaysOffNumberRequired)]
        [Range(0, 365, ErrorMessage = Constants.DaysOffNumberRangeErrorMessage)]
        int DaysOffNumber,

        [Required(ErrorMessage = Constants.PositionIdRequired)]
        [Range(1, int.MaxValue, ErrorMessage = Constants.PositionIdRangeErrorMessage)]
        int PositionId
    );
}

