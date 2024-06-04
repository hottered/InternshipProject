using SharedDll;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Position
{
    public record UserPositionCreateRequest(
        [Required(ErrorMessage = Constants.CaptionRequired)]
        [StringLength(50, ErrorMessage = Constants.CaptionErrorMessage)]
        string Caption,

        [Required(ErrorMessage = Constants.DescRequired)]
        [StringLength(50, ErrorMessage = Constants.DescErrorMessage)]
        string Description
        );
}
