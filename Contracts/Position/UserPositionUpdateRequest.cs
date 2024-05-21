using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Position
{
    public record UserPositionUpdateRequest(
        
        int Id,

        [Required(ErrorMessage = "Caption is required.")]
        [StringLength(50, ErrorMessage = "Caption can't be longer than 50 characters.")]
        string Caption,

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(50, ErrorMessage = "Description can't be longer than 50 characters.")]
        string Description
        
            
        );
}
