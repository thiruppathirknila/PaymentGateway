using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Gateway.Models
{
  public  class ContactInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
