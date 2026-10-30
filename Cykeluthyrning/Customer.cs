using System;
using System.Collections.Generic;
using System.Text;

namespace Cykeluthyrning
{
    public class Customer
    {
        public string Name {  get; set; }
        public int Age { get; set; }
        public bool IsMember { get; set; } = true;
        public int CustomerNumber { get; set; }
        public Customer (string name, int age, bool isMember, int costumerNumber )
        {
            Name = name;
            Age = age;
            IsMember = isMember;
            CustomerNumber = costumerNumber;
        }
    }
}
