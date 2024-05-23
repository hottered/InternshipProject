using Contracts.Employee;
using Contracts.Position;
using DataLayer.Models;
using DataLayer.Models.Position;

namespace ServiceLayer.Mappers
{
    public static class PositionMappingExtension
    {
        public static UserPosition ToUserPosition(this UserPositionCreateRequest request)
        {
            if(request == null)
            {
                return null;
            }
            return new UserPosition
            {
                Caption = request.Caption,
                Description = request.Description,
            };
        }
        public static UserPosition ToUserPosition(this UserPosition position, UserPositionUpdateRequest updateRequest)
        {
            if (position == null)
            {
                return null;
            }
            position.Caption = updateRequest.Caption;
            position.Description = updateRequest.Description;

            return position;
        }
        public static UserPositionUpdateRequest ToEmployeeUpdateRequest(this UserPosition employee)
        {
            if (employee == null) { return null; }

            return new UserPositionUpdateRequest(
                    employee.Id,
                    employee.Caption, 
                    employee.Description
                );

        }
        public static UserPositionGetResponse ToPositionResponse(this UserPosition position)
        {
            if(position == null)
            {
                return null;
            }
            return new UserPositionGetResponse(
                    position.Caption,
                    position.Description
                );
        }
    }
}
