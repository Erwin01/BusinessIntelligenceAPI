using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightAPI.Models
{
    public class AplicationDbContext : DbContext
    {

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Server> Servers { get; set; }


        // IMPORTANT POINT: When the database instances are created from the command in the models in the identity column.
        // place the label to be auto incrementable: [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        // Add-Migration "Wherever"
        // update-database

        // Yes, the error continues from sql server, remove the auto-incremental from the design and not place the identity column in the model.

    }
}
