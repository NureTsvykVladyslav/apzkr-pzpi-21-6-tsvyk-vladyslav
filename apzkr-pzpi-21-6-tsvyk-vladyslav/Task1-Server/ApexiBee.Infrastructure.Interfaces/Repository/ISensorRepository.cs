using ApexiBee.Domain.Models;

namespace ApexiBee.Infrastructure.Interfaces.Repository
{
    public interface ISensorRepository : IRepository<Sensor>
    {
        Task<Sensor?> GetByIdWithAllDetailsAsync(Guid id);
    }
}
