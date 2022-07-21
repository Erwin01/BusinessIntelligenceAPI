using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SightAPI.Models
{
    public class ServerMessage
    {

        public int Id { get; set; }
        public bool Payload { get; set; }

    }
}
