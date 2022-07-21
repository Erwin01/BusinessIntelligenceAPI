﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SightAPI.Models
{
    public class Customer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
    }
}
