using System;
using System.Collections.Generic;
using System.Text;

namespace ApexiBeeMobile.Models
{
    public class Apiary
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid BeekeeperId { get; set; }
    }
}
