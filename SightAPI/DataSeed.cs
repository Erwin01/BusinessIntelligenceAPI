using SightAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightAPI
{

    /// <summary>
    /// Data Seed API
    /// </summary>
    public class DataSeed
    {

        /// <summary>
        /// Context Database
        /// </summary>
        private readonly AplicationDbContext _aplicationDbContext;


        /// <summary>
        /// Constructor API context
        /// </summary>
        public DataSeed(AplicationDbContext aplicationDbContext)
        {
            _aplicationDbContext = aplicationDbContext;
        }


        
        /// <summary>
        /// Public Method In Our Data Initialization Class
        /// Generate programmatically creating type of random customer names and random orders and random servers
        /// </summary>
        /// <param name="numberCustormers"></param>
        /// <param name="numberOrders"></param>
        public void SeedData(int numberCustormers, int numberOrders) 
        {

            // Fill our database if we don't have some data in it
            if (!_aplicationDbContext.Customers.Any())
            {
                SeedCustomers(numberCustormers);
                
                //Insert Seed Customer
                _aplicationDbContext.SaveChanges();
            }

            if (!_aplicationDbContext.Orders.Any())
            {
                SeedOrders(numberOrders);

                //Insert Seed Order
                _aplicationDbContext.SaveChanges();
            }

            if (!_aplicationDbContext.Servers.Any())
            {
                SeedServers();

                //Insert Seed server
                _aplicationDbContext.SaveChanges();
            }

            _aplicationDbContext.SaveChanges();
        }



        /// <summary>
        /// Seed Customer
        /// Add our collection of clients to our contact database for each client on the list we build 
        /// </summary>
        /// <param name="numberCustomers"></param>
        private void SeedCustomers(int numberCustomers) 
        {
            // Get quantity customers object list
            List<Customer> customers = BuildCustomerList(numberCustomers);

            // We iterate the list for each we will save our client in Clients
            foreach (var customer in customers)
            {
                // see cutomers and aggregate customer
                _aplicationDbContext.Customers.Add(customer);
            }

        }


        /// <summary>
        /// Add our collection of orders to our contact database for each orders on the list we build
        /// </summary>
        /// <param name="numberOrders"></param>
        private void SeedOrders(int numberOrders) 
        {
            // Get quantity orders object list
            List<Order> orders = BuildOrderList(numberOrders);

            // We iterate the list for each we will save our order in Orders
            foreach (var order in orders)
            {
                // see orders and aggregate order
                _aplicationDbContext.Orders.Add(order);
            }
        }



        /// <summary>
        ///  Add our collection of servers to our contact database for each servers on the list we build
        /// </summary>
        private void SeedServers()
        {
            List<Server> servers = BuildServerList();

            //  We iterate the list for each we will save our server in Servers
            foreach (var server in servers)
            {
                _aplicationDbContext.Servers.Add(server);
            }
        }


        /// <summary>
        /// Method to build our customer list
        /// Call the list in our initial customers method
        /// </summary>
        /// <param name="numberCustomers"></param>
        /// <returns></returns>
        private List<Customer> BuildCustomerList(int numberCustomers) 
        {

            // customer list
            var customers = new List<Customer>();
            var names = new List<string>();

            // Generate each of the properties to create a number of unique clients in our database
            for (var i = 1; i <= numberCustomers; i++)
            {

                // Pseudo random name for our clients to have some mail and email address and their random status
                var name = Helpers.MakeUniqueCustomerName(names);
                names.Add(name);

                // Add to our customer list 
                customers.Add(new Customer
                {
                    Id = i,
                    Name = name,
                    Email = Helpers.MakeCustomerEmail(name),
                    State = Helpers.GetRandomState()
                });
            }

            return customers;
        }




        /// <summary>
        /// Method to build our order list
        /// Call the list in our initial orders method
        /// </summary>
        /// <param name="numberOrders"></param>
        /// <returns> Orders list </returns>
        private List<Order> BuildOrderList(int numberOrders)
        {

            // order list
            var orders = new List<Order>();
            // random order customer
            var rand = new Random();

            // Generate each of the properties to create a number of unique orders in our database
            for (var i = 1; i <= numberOrders; i++)
            {
                // value 1 is maximun
                var randCustomerId = rand.Next(1, _aplicationDbContext.Customers.Count());
                // Pseudo random Oder placed for our orders
                var placed = Helpers.GetRandomOrderPlaced();
                // Pseudo random Ordr completed for our orders
                var completed = Helpers.GetRandomOrderCompleted(placed);

                var customers = _aplicationDbContext.Customers.ToList();

                // Add to our order list 
                orders.Add(new Order
                {
                    Id = i,
                    Total = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed,
                    Customer = _aplicationDbContext.Customers.First(c => c.Id == randCustomerId)
                });
            }

            return orders;
        }



        /// <summary>
        /// Method to build our server list
        /// Call the list in our initial server method
        /// </summary>
        /// <returns> Return server list fictitious data </returns>
        private List<Server> BuildServerList()
        {
            return new List<Server>()
            {
                new Server
                {
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true
                },
                 new Server
                {
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = false
                },
                  new Server
                {
                    Id = 3,
                    Name = "Dev-Services",
                    IsOnline = true
                },
                  new Server
                {
                    Id = 4,
                    Name = "QA-Web",
                    IsOnline = true
                },
                 new Server
                {
                    Id = 5,
                    Name = "QA-Mail",
                    IsOnline = false
                },
                  new Server
                {
                    Id = 6,
                    Name = "QA-Services",
                    IsOnline = true
                },
                  new Server
                {
                    Id = 7,
                    Name = "Prod-Web",
                    IsOnline = true
                },
                 new Server
                {
                    Id = 8,
                    Name = "Prod-Mail",
                    IsOnline = false
                },
                  new Server
                {
                    Id = 9,
                    Name = "Prod-Services",
                    IsOnline = true
                }
            };
        }

    }
}
