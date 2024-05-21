using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Position
{
    public record UserPositionGetResponse(
        string Caption, 
        string Description);

}
