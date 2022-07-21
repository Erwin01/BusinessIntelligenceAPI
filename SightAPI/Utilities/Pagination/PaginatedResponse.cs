using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightAPI.Utilities.Pagination
{
    public class PaginatedResponse<T>
    {


        /// <summary>
        /// Constructor
        /// Paginated response with an enumerable collection of some data type
        /// </summary>
        public PaginatedResponse(IEnumerable<T> data, int index, int length)
        {

            // We take a certain number of results from a query and omit the page index minus the total page length.
            // And take the length of the page and call a list.
            // [1] page, 10 results | We are looking for the first page where we have 10 rows
            Data = data
                .Skip((index - 1) * length)
                .Take(length)
                .ToList();

            // Equal number total
            Total = data.Count();

        }



        /// Total count of the data it loops through
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
