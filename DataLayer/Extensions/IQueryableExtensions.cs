using Contracts.Employee;
using Contracts.Filter;
using Contracts.Position;
using Contracts.Request;
using DataLayer.Models;
using DataLayer.Models.Position;
using DataLayer.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<UserRequest> Filter(this IQueryable<UserRequest> queryable, UserRequestFilter filter)
        {
            if (filter.SearchString is null)
            {
                return queryable;
            }

            var searchString = filter.SearchString.ToLower();

            return queryable.Where(x =>
                x.CommentEmployee.ToLower().Contains(searchString) ||
                x.LeaveType == filter.LeaveType);
        }

        public static IQueryable<Employee> Filter(this IQueryable<Employee> queryable, EmployeeFilter filter)
        {
            if (filter.SearchString is null)
            {
                return queryable;
            }

            var searchString = filter.SearchString.ToLower();

            return queryable.Where(x =>
                x.FirstName!.ToLower().Contains(searchString) ||
                x.LastName!.ToLower().Contains(searchString));
        }

        public static IQueryable<UserPosition> Filter(this IQueryable<UserPosition> queryable, UserPositionFilter filter)
        {
            if (filter.SearchString is null)
            {
                return queryable;
            }

            var searchString = filter.SearchString.ToLower();

            return queryable.Where(x =>
                x.Caption!.ToLower().Contains(searchString) ||
                x.Description!.ToLower().Contains(searchString));
        }
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, FilterBase filter)
        {
            filter.PageNumber = filter.PageNumber is null ? 1 : filter.PageNumber;
            filter.PageSize = filter.PageSize is null ? 3 : filter.PageSize;

            return queryable
                .Skip((int)((filter.PageNumber - 1) * filter.PageSize))
                .Take((int)filter.PageSize);
        }
    }
}
