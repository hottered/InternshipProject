using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Position
{
    public record PositionGetResponse(
        string Caption, 
        string Description);

}
