using ApexiBee.Domain.Models;

namespace ApexiBee.Infrastructure.Interfaces.Repository
{
    public interface IApiaryRepository : IRepository<Apiary>
    {
        IQueryable<Apiary> GetAllWithHives();

        Task<Apiary?> GetByIdWithHivesAsync(Guid id);
    }
}
