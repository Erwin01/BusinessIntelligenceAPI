using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SightAPI
{

    /// <summary>
    /// Helper class that constructs the client name
    /// </summary>
    public class Helpers
    {

        /// <summary>
        /// Class to have a private static random
        /// </summary>
        private static Random _rand = new Random();



        /// <summary>
        /// Get method a random item from a list of strings
        /// </summary>
        /// <param name="items"></param>
        /// <returns> We will return items and pass in the integer and it will be the number of items we have </returns>
        private static string GetRandom(IList<string> items) 
        {
            return items[_rand.Next(items.Count)];
        }



        /// <summary>
        /// Static method to make the name of clients
        /// </summary>
        /// <returns>  We return the prefix plus a solution </returns>
        internal static string MakeUniqueCustomerName(List<string> names)
        {
            // Generating a number of customer names
            var maxNames = commercialPrefix.Count * commercialSuffix.Count;

            // If the names that count are equal to the maximum names
            if (names.Count >= maxNames)
            {
                // Exception no valid!
                throw new System.InvalidOperationException("Maximun number of unique names exceeded");
            }

            // Get random commercial prefix and suffix
            var prefix = GetRandom(commercialPrefix);
            var suffix = GetRandom(commercialSuffix);

            var commercialName = prefix + suffix;

            // Check if names contains the full name
            if (names.Contains(commercialName))
            {
                MakeUniqueCustomerName(names);
            }

            return commercialName;
        }



        /// <summary>
        /// Static method to make the email of clients
        /// </summary>
        /// <returns> We return the string of the customer's email </returns>
        internal static string MakeCustomerEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com";
        }



        /// <summary>
        /// Static method to make the state of customers
        /// </summary>
        /// <returns> We return the string of the customer's state </returns>
        internal static string GetRandomState() 
        {
            return GetRandom(usaStates);
        }


        /// <summary>
        /// static method to make the Order Total of Order
        /// </summary>
        /// <returns> Return a random and value between 100 - 50000 </returns>
        internal static decimal GetRandomOrderTotal() 
        {
            return _rand.Next(100, 5000);
        }



        /// <summary>
        /// static method to make the Order Placed of Order
        /// </summary>
        /// <returns> Get a random date an order was placed  </returns>
        internal static DateTime GetRandomOrderPlaced() 
        {
            // Date now
            var end = DateTime.Now;
            // Date historic order between now and 90 days
            var start = end.AddDays(-90);

            // lapsus time
            TimeSpan possibleSpan = end - start;

            // lapsus new - hours, minutes and seconds
            // Get random number of minutes is zero and number possible of minutes |  Interval of time possible
            TimeSpan newSpan = new TimeSpan(0,  _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return start + newSpan;
        }



        /// <summary>
        /// static method to make the Order Completed of Order
        /// </summary>
        /// <returns> Get a random date an order was completed  </returns>
        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            // Whether the order date should be null or not
            var now = DateTime.Now;

            // Time delivered minimun
            var minLeadTime = TimeSpan.FromDays(7);

            // Past time  
            var timePassed = now - orderPlaced;

            // If the requested time is less than our minimum delivery time. The order will not be completed
            if (timePassed < minLeadTime)
            {
                return null;
            }

            // It is still in our delivery time | 7 and 14 days
            return orderPlaced.AddDays(_rand.Next(7, 14));
        }



        /// <summary>
        /// Generate random addresses of a different type and use states from another country | U.S.A
        /// </summary>
        private static readonly List<string> usaStates = new List<string>()
        {
            // State codes
            "AK", "AL", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
        };



        /// <summary>
        /// List of strings for business prefixes
        /// </summary>
        private static readonly List<string> commercialPrefix = new List<string>()
        {
            // List of fictitious businesses prefixes
            "ABC",
            "MainSt",
            "Sales",
            "Enterprise",
            "Ohready",
            "Budget",
            "Magic",
            "Peak",
            "Llanero",
            "Forgiven",
            "XYZ",
            "Family",
        };


        /// <summary>
        /// List of strings for business suffixes
        /// </summary>
        private static readonly List<string> commercialSuffix = new List<string>()
        {
            // List of fictitious businesses suffixes
            "Corporation",
            "Col",
            "Goods",
            "Hotels",
            "Planners",
            "Automotive",
            "Ayios",
            "Bakery",
            "Fortsale",
            "Books",
            "Foods",
            "Transit",
        };

    }
}
