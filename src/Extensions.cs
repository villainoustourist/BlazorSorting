using System;
using System.Linq;
using System.Linq.Expressions;

namespace BlazorSorting
{
    public static class Extensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string column, Direction direction)
        {
            return source.OrderBy(new Parameters { SortBy = column, Direction = direction });
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, Parameters sortModels)
        {
            try
            {
                var expression = source.Expression;
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, sortModels.SortBy);
                var method = sortModels.Direction.Equals(Direction.Descending)
                        ? "OrderByDescending"
                        : "OrderBy";
                expression = Expression.Call(typeof(Queryable), method,
                    new[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                return source.Provider.CreateQuery<T>(expression);
            }
            catch (Exception)
            {
                // ignored if invalid sort (i.e. property doesn't exist)
            }

            return source;
        }

    }
}