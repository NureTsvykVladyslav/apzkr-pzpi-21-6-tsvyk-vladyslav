namespace ApexiBee.Application.DTO
{
    public class NewOrderData
    {
        public string Description { get; set; } = null!;

        public Guid ManagerId { get; set; }

        public Guid BeekeeperId { get; set; }
    }
}
