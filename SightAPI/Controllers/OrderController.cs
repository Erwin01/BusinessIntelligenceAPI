using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SightAPI.Models;
using SightAPI.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        // Database context
        private readonly AplicationDbContext _aplicationDbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aplicationDbContext"></param>
        public OrderController(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }



        /// <summary>
        ///  Get Page Number And Results By Page
        ///  Route: api/order/pageNUmber/pageSize
        /// </summary>
        /// <returns> Only page results </returns>
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult GetByPagine(int pageIndex, int pageSize)
        {

            // Include the customer in that order descending
            var data = _aplicationDbContext.Orders
                .Include(o => o.Customer)
                .OrderByDescending(c => c.Placed);
            
            
            // Create pagination response generic
            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);


            // Total count equals our dated back count
            var totalCount = data.Count();


            // total number page
            // Total count of results and divide it by the page size and calculate the total number of pages
            var totalPages = Math.Ceiling((double)totalCount / pageSize);


            // Response new object and a page is defined in it and it is established that it is equal to our page
            // that we obtain from the paginated method, that is, its response
            // Pagine And Total Number Page
            var response = new
            {
                Page = page,
                TotalPages = totalCount
            };

            return Ok(response);
        }



        /// <summary>
        /// Get By State
        /// </summary>
        /// <returns> State </returns>
        [HttpGet("ByState")]
        public IActionResult GetByState() 
        {

            // We get our orders that are in our contact orders and make sure to include the customer's order
            var orders = _aplicationDbContext.Orders.Include(o => o.Customer).ToList();

            // An aggregate query type is done using ordering and grouping by.
            // Group by order customer status and display in a list.
            var groupResult = orders
                .GroupBy(o => o.Customer.State)
                .ToList()
                .Select(group => new
                {
                    State = group.Key,
                    Total = group.Sum(x => x.Total)
                })
                .OrderByDescending(response => response.Total)
                .ToList();


            return Ok(groupResult);
        }



        /// <summary>
        /// Get orders grouped by customers
        /// </summary>
        /// <returns> Get orders grouped by customers </returns>
        [HttpGet("ByCustomer/{number}")]
        public IActionResult GetOrderByCustomers(int number)
        {

            // We get our orders that are in our contact orders and make sure to include the customer's order
            var orders = _aplicationDbContext.Orders.Include(o => o.Customer).ToList();

            // An aggregate query type is done using ordering and grouping by.
            // Group by order customer status and display in a list.
            var groupResult = orders
                .GroupBy(o => o.Customer.Id)
                .ToList()
                .Select(group => new
                {
                    Name = _aplicationDbContext.Customers.Find(group.Key).Name,
                    Total = group.Sum(x => x.Total)
                })
                .OrderByDescending(response => response.Total)
                .Take(number)
                .ToList();


            return Ok(groupResult);
        }



        /// <summary>
        /// Get orders by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOrder/{id}", Name = "GetOrder")]
        public IActionResult GetOrderById(int id) 
        {

            var order = _aplicationDbContext.Orders
                .Include(o => o.Customer)
                .First(o => o.Id == id);

            return Ok(order);
        }
    }
}
