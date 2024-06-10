using System;
using System.Collections.Generic;
using System.Text;

namespace ApexiBeeMobile.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}