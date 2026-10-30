using System;
using System.Collections.Generic;
using System.Text;

namespace Cykeluthyrning
{
    public class ElectricBike : RegularBike                 // ärver Name, BikeNumber, BikeHourPrice, RentedByCustomerNumber från RegularBike
    {
        public bool ElectricAvailable { get; set; }
 
        public int BatteryLevel { get; set; }

        public ElectricBike(string name, int bikeNumber, bool electricAvailable, int batteryLevel, decimal bikeHourPrice)
            : base(name, bikeNumber, electricAvailable, bikeHourPrice)
        {
            ElectricAvailable = electricAvailable;
            BatteryLevel = batteryLevel;
        }
        public void ChargeBike()
        {
            BatteryLevel = 100;
        }
        public void UseBattery(int rentedHours)
        {
            BatteryLevel = Math.Max(0, BatteryLevel - rentedHours * 10);
        }
    }
}
