using SharedDll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Employee
{
    public record EmployeeCreateRequest
    (
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
    string IdNumber,

    [Required(ErrorMessage = Constants.PositionIdRequired)]
    [Range(1, int.MaxValue, ErrorMessage = Constants.PositionIdRangeErrorMessage)]
    int PositionID,

    [Required(ErrorMessage = Constants.EmailRequired)]
    [EmailAddress(ErrorMessage = Constants.EmailErrorMessage)]
    string Email,

    [Required(ErrorMessage = Constants.PasswordRequired)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = Constants.PasswordErrorMessage)]
    string Password
    );
}
