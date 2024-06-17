using ApexiBee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexiBee.Application.Interfaces
{
    public interface IUserService
    {
        Task UpdateUser(UserAccount userAccount);

        Task<UserAccount> GetUserAccountById(Guid id);
    }
}