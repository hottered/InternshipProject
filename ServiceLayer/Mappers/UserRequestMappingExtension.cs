using Azure.Core;
using Contracts.Position;
using Contracts.Request;
using DataLayer.Models.Position;
using DataLayer.Models.Request;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Mappers
{
    public static class UserRequestMappingExtension
    {
        public static UserRequest ToUserRequest(this UserRequest request, UserRequestUpdateRequest userRequestUpdateRequest)
        {
            if(request is null)
            {
                return null;
            }
            request.StartDate = userRequestUpdateRequest.StartDate;
            request.EndDate = userRequestUpdateRequest.EndDate;
            request.LeaveType = userRequestUpdateRequest.LeaveType;
            request.CommentEmployee = userRequestUpdateRequest.CommentEmployee;
            request.CommentHR = userRequestUpdateRequest.CommentHR;
            request.Approved = userRequestUpdateRequest.Approved;

            return request;
        }
        public static UserRequestUpdateRequest ToUserRequestUpdateRequest(this UserRequest request)
        {
            if (request == null) { return null; }

            return new UserRequestUpdateRequest(
                    request.Id,
                    request.StartDate,
                    request.EndDate,
                    request.LeaveType,
                    request.CommentEmployee,
                    request.CommentHR,
                    request.Approved
                );

        }
        public static UserRequestGetResponse ToUserRequestGetResponse(this UserRequest userRequest)
        {
            if (userRequest == null) { return null; }

            return new UserRequestGetResponse(
                    userRequest.Id,
                    userRequest.Employee.FirstName,
                    userRequest.StartDate,
                    userRequest.EndDate,
                    userRequest.LeaveType,
                    userRequest.Approved,
                    userRequest.EmployeeId
                );
        }
        public static List<UserRequestGetResponse> ToUserRequestGetResponseList(this List<UserRequest> userRequests)
        {
            if (userRequests == null) { return null; }

            return userRequests
                    .Select(userRequest => userRequest.ToUserRequestGetResponse())
                    .ToList();
        }
        public static UserRequest ToUserRequest(this UserRequestCreateRequest userRequest)
        {
            if(userRequest is null)
            {
                return null;
            }

            return new UserRequest
            {
                StartDate = userRequest.StartDate,
                EndDate = userRequest.EndDate,
                LeaveType = userRequest.LeaveType,
                CommentEmployee = userRequest.CommentEmployee,
                EmployeeId = userRequest.UserId
            };
        }
    }
}
