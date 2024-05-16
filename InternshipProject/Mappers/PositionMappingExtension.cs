using Contracts.Position;
using DataLayer.Models.Position;

namespace InternshipProject.Mappers
{
    public static class PositionMappingExtension
    {
        public static UserPosition ToUserPosition(this PositionCreateRequest request)
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
        public static PositionGetResponse ToPositionResponse(this UserPosition position)
        {
            if(position == null)
            {
                return null;
            }
            return new PositionGetResponse(
                    position.Caption,
                    position.Description
                );
        }
    }
}
