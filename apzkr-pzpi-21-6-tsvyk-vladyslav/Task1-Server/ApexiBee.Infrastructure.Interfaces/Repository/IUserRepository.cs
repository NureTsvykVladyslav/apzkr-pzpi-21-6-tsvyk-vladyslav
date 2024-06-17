using ApexiBee.Domain.Models;
using ApexiBee.Infrastructure.Interfaces.Repository;

namespace ApexiBee.Infrastructure.Interfaces.Repository_Interfaces
{
    public interface IUserRepository : IRepository<UserAccount>
    {
        Task<UserAccount?> GetByIdWithDetailsAsync(Guid id);

        IQueryable<UserAccount> GetAllWithDetails();
    }
}
