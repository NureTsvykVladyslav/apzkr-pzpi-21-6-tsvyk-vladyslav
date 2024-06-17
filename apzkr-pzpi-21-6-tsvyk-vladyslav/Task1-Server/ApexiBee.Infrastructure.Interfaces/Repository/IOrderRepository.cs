using ApexiBee.Domain.Models;

namespace ApexiBee.Infrastructure.Interfaces.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetByIdWithAllDetailsAsync(Guid id);

        IQueryable<Order> GetAllWithAllDetails();
    }
}
