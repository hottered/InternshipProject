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
using System.Linq.Expressions;
using System.Reflection;
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

        //public static IQueryable<UserPosition> Filter(this IQueryable<UserPosition> queryable, UserPositionFilter filter)
        //{
        //    if (filter.SearchString is null)
        //    {
        //        return queryable;
        //    }

        //    var searchString = filter.SearchString.ToLower();

        //    return queryable.Where(x =>
        //        x.Caption!.ToLower().Contains(searchString) ||
        //        x.Description!.ToLower().Contains(searchString));
        //}
        public static IQueryable<TEntity> Filter<TEntity, TFilter>(this IQueryable<TEntity> queryable,
            TFilter filter,
            params Expression<Func<TEntity, string>>[] propertySelectors)
            where TEntity : class
            where TFilter : FilterBase
        {
            if (filter.SearchString is null)
            {
                return queryable;
            }

            var searchString = filter.SearchString.ToLower();
            Expression? combinedExpression = null;
            var parameter = Expression.Parameter(typeof(TEntity), "entity");

            foreach (var selector in propertySelectors)
            {
                var propertyAccess = Expression.Invoke(selector, parameter);
                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

                var toLowerExpression = Expression.Call(propertyAccess, toLowerMethod);
                var searchStringExpression = Expression.Constant(searchString);
                var containsExpression = Expression.Call(toLowerExpression, containsMethod, searchStringExpression);

                if (combinedExpression == null)
                {
                    combinedExpression = containsExpression;
                }
                else
                {
                    combinedExpression = Expression.OrElse(combinedExpression, containsExpression);
                }
            }

            if (combinedExpression == null)
            {
                return queryable;
            }

            var lambda = Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);

            return queryable.Where(lambda);
        }

        //public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, FilterBase filter) where T : class
        //{
        //    if (filter == null || string.IsNullOrEmpty(filter.SearchString))
        //    {
        //        return queryable;
        //    }

        //    var classToFilterType = typeof(T);
        //    var propertiesOfClass = classToFilterType.GetProperties();

        //    var parameter = Expression.Parameter(classToFilterType, "x");

        //    Expression? combinedExpression = null;

        //    foreach (var classProperty in propertiesOfClass)
        //    {
        //        if (classProperty.PropertyType != typeof(string)) continue;

        //        var left = Expression.Property(parameter, classProperty);
        //        var right = Expression.Constant(filter.SearchString);

        //        var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //        var expression = Expression.Call(left, method, right);

        //        if (combinedExpression == null)
        //        {
        //            combinedExpression = expression;
        //        }
        //        else
        //        {
        //            combinedExpression = Expression.OrElse(combinedExpression, expression);
        //        }
        //    }

        //    if (combinedExpression == null)
        //    {
        //        return queryable;
        //    }

        //    var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

        //    return queryable.Where(lambda);
        ////}
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
