using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fushan
{
    public class PaginatedIQueryableExtensions<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int Count { get; private set; }
        public IQueryable<T> Item { get; private set; }

        //public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        //{
        //    PageIndex = pageIndex;
        //    TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        //    this.AddRange(items);
        //}
        public PaginatedIQueryableExtensions(IQueryable<T> item, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            Count = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Item = item;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedIQueryableExtensions<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, bool showAll = false)
        {
            var count = await source.CountAsync();
            var item = showAll ? source : source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new PaginatedIQueryableExtensions<T>(item, count, pageIndex, pageSize);
        }

        //public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        //{
        //    var count = await source.CountAsync();
        //    var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        //    return new PaginatedList<T>(items, count, pageIndex, pageSize);
        //}
    }
}