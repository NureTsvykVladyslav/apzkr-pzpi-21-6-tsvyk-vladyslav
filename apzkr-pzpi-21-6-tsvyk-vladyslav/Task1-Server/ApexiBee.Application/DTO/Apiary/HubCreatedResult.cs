using ApexiBee.Domain.Models;

namespace ApexiBee.Application.DTO.Apiary
{
    public class HubCreatedResult
    {
        public Guid Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime EquipmentRegistrationDate { get; set; }

        public SerialData SerialData { get; set; } = null!;
    }
}
