
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace SpaceAdventures.Application.Common.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int PageNumber { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)    
        {
            PageNumber = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);  // eg. 10 items / 2 items per page = 5 pages
            TotalCount = count;
            Items = items;
        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, // eg. p.2 | size : 5 = 10 
            int pageSize)
        {
            var count = await source.CountAsync();   // 10
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
