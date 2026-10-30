using System;
using System.Collections.Generic;
using System.Text;

namespace Cykeluthyrning
{
    public class RegularBike
    {
        public string Name { get; set; }
        public int BikeNumber { get; set; }
        public bool RegularAvailable { get; set; }
        public int? RentedByCustomerNumber { get; set; }
        public decimal BikeHourPrice { get; set; }
        public RegularBike(string name, int bikeNumber, bool regularAvailable, decimal bikeHourPrice)
        {
            Name = name;
            BikeNumber = bikeNumber;
            RegularAvailable = regularAvailable;
            BikeHourPrice = bikeHourPrice;
        }
    }
}