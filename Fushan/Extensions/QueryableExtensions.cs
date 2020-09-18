using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fushan.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByMember, bool isDesc)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));
            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);
            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                isDesc ? "OrderByDescending" : "OrderBy",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(orderBy);
        }

        public static IQueryable<TSource> Where<TSource, TFilter>(this IQueryable<TSource> source, TFilter? filter,
            Expression<Func<TSource, bool>> predicate) where TFilter : struct
        {
            if (filter.HasValue)
            {
                source = source.Where(predicate);
            }

            return source;
        }

        // Where extension for string filters
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, string filter,
            Expression<Func<TSource, bool>> predicate)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                source = source.Where(predicate);
            }

            return source;
        }

        // Where extension for string filters
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Guid filter,
            Expression<Func<TSource, bool>> predicate)
        {
            if (filter != null && filter != Guid.Empty)
            {
                source = source.Where(predicate);
            }

            return source;
        }

        // Where extension for collection filters
        public static IQueryable<TSource> Where<TSource, TFilter>(this IQueryable<TSource> source, IEnumerable<TFilter> filter,
            Expression<Func<TSource, bool>> predicate)
        {
            if (filter != null && filter.Any())
            {
                source = source.Where(predicate);
            }

            return source;
        }

        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, bool isFiltered,
            Expression<Func<TSource, bool>> predicate)
        {
            if (isFiltered)
            {
                source = source.Where(predicate);
            }

            return source;
        }
    }
}