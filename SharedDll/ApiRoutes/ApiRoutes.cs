using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDll.ApiRoutes
{
    public static class ApiRoutes
    {
        //HttpClient
        public const string BaseAddress = "https://localhost:7082/api";
        public const string RandomUsers = "/Users";

        public const string EditUserById = "/users";
        public const string RetrieveUsers = "/random-users/add";
        public const string AllUsers = "/users/all";
        public const string CreateUser = "/users/new";
        public const string EditUser = "/users/{id}/edit";
        public const string DeleteUser = "/users/{id}/delete";


        //POSITIONS
        public const string EditUserPositionById = "/user-positions";
        public const string AllUserPositions = "/user-positions/all";
        public const string CreateUserPosition = "/user-positions/new";
        public const string EditUserPosition = "/user-positions/{id}/edit";
        public const string DeleteUserPosition = "/user-positions/{id}/delete";


        //REQUESTS
        public const string CreateUserRequest = "/user-requests/new";
        public const string AllUserRequests = "/user-requests/all";
        public const string AllUserRequestsForUser = "/user-requests/user/{id}";
        public const string DeleteUserRequest = "/user-requests/{id}/delete";
        public const string EditUserRequest = "/user-requests/{id}/edit";
        public const string ApproveUserRequest = "/user-requests/{id}/approve";
        public const string RejectUserRequest = "/user-requests/{id}/reject";
    }
}
