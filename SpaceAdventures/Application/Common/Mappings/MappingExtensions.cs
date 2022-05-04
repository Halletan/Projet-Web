using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SpaceAdventures.Application.Common.Models;

namespace SpaceAdventures.Application.Common.Mappings
{
    public static class MappingExtensions
    {

        // Extension method to use in order to convert the consumed list into a paginated list 
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
            this IQueryable<TDestination> queryable, int pageNumber, int pageSige)
        {
            return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSige);
        }

        // Extension method of Queryable Extensions - AutoMapper - eg. DTOs

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable,
            IConfigurationProvider configuration)
        {
            return queryable.ProjectTo<TDestination>(configuration).ToListAsync();
        }




    }
}
