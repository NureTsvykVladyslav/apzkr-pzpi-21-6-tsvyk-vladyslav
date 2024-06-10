using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexiBee.Domain.Models
{
    public class HubStation : BaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime EquipmentRegistrationDate { get; set; }

        public Guid? SerialDataId { get; set; }
        public SerialData SerialData { get;set; }

        public Guid? ApiaryId { get; set; }

        [JsonIgnore]
        public Apiary Apiary { get; set; }
    }
}
