using ApexiBee.Domain.Models;

namespace ApexiBee.Infrastructure.Interfaces.Repository
{
    public interface IHubStationRepository : IRepository<HubStation>
    {
        Task<HubStation?> GetByIdWithAllDetailsAsync(Guid id);
    }
}
