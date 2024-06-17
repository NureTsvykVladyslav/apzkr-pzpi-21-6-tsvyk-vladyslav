using ApexiBee.Domain.Models;

namespace ApexiBee.Infrastructure.Interfaces.Repository
{
    public interface IHiveRepository : IRepository<Hive>
    {
        Task<Hive?> GetByIdWithAllDetailsAsync(Guid id);
    }
}
