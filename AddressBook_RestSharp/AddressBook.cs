using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook_RestSharp
{
    public class AddressBook
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
