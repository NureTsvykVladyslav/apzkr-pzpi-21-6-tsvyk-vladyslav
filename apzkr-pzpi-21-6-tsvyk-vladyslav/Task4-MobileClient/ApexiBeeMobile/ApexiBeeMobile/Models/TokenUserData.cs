using System;
using System.Collections.Generic;
using System.Text;

namespace ApexiBeeMobile.Models
{
    public class TokenUserData
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
    }
}
