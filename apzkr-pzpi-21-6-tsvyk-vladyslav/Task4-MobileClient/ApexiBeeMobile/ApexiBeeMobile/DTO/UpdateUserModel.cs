using System;
using System.Collections.Generic;
using System.Text;

namespace ApexiBeeMobile.DTO
{
    public class UpdateUserModel
    {
        public Guid UserAccountId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
