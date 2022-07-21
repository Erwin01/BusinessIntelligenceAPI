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
    public class ServerController : ControllerBase
    {

        // Database context
        private readonly AplicationDbContext _aplicationDbContext;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aplicationDbContext"></param>
        public ServerController(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }



        /// <summary>
        /// Get All Servers
        /// </summary>
        /// <returns> Servers Oder by Id </returns>
        [HttpGet]
        public IActionResult GetAll() 
        {
            var response = _aplicationDbContext.Servers.OrderBy(s => s.Id).ToList();

            return Ok(response);
        }




        /// <summary>
        /// Get Server by Id
        /// </summary>
        /// <returns> Servers Oder by Id </returns>
        [HttpGet("{id}", Name = "GetServer")]
        public IActionResult GetById(int id)
        {
            var response = _aplicationDbContext.Servers.Find(id);

            return Ok(response);
        }



        /// <summary>
        /// Update Message Server
        /// </summary>
        /// <param name="id"></param>
        /// <returns> id </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ServerMessage serverMessage) 
        {
            var server = _aplicationDbContext.Servers.Find(id);

            if (server == null)
            {
                return NotFound();
            }

            //if (server.IsOnline == true)
            //{
            //    server.IsOnline = true;
            //    await _aplicationDbContext.SaveChangesAsync();
            //}

            //if (server.IsOnline == false)
            //{
            //    server.IsOnline = false;
            //    await _aplicationDbContext.SaveChangesAsync();
            //}
            //Server found by Id -refactor: Move into a service
            if (serverMessage.Payload == true)
            {
                server.IsOnline = true;
                await _aplicationDbContext.SaveChangesAsync();
            }

            if (serverMessage.Payload == false)
            {
                server.IsOnline = false;
                await _aplicationDbContext.SaveChangesAsync();
            }

            await _aplicationDbContext.SaveChangesAsync();

            //return Ok(server);
            return new ContentResult(); // sin contenido

        }

    }
}
