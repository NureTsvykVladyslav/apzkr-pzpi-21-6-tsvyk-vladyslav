namespace ApexiBee.Application.DTO
{
    public class NewApiaryData
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public Guid BeekeeperUserId { get; set; }
    }
}
