using SpaceAdventures.Application.Common.Models;

namespace SpaceAdventures.Application.Common.Mappings;

public static class MappingExtensions
{
    // Extension method to use in order to convert the consumed list into a paginated list 
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
    {
        return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }
}