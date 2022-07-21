using Microsoft.AspNetCore.Mvc;
using SightAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        // Database context
        private readonly AplicationDbContext _aplicationDbContext;



        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerController(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }



        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns> Customers </returns>
        [HttpGet]
        public IActionResult GetAll()
        {

            var data = _aplicationDbContext.Customers.OrderBy(c => c.Id);

            return Ok(data);
        }



        /// <summary>
        /// Get By Id Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Id Customer </returns>
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetBy(int id)
        {

            var customer = _aplicationDbContext.Customers.Find(id);

            return Ok(customer);
        }



        /// <summary>
        /// Create New Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns> New Customer </returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer) 
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _aplicationDbContext.Customers.Add(customer);
            await _aplicationDbContext.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }


     }
}
