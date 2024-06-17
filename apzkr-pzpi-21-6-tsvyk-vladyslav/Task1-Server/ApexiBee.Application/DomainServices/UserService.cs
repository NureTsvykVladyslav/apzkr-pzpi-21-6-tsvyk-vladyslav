using ApexiBee.Application.Exceptions;
using ApexiBee.Application.Interfaces;
using ApexiBee.Application.Services;
using ApexiBee.Domain.Models;
using ApexiBee.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexiBee.Application.DomainServices
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<UserAccount> GetUserAccountById(Guid id)
        {
            var userAccount = await this.unitOfWork.UserRepository.GetByIdAsync(id);
            if (userAccount == null)
            {
                throw new NotFoundException(id, "user account");
            }

            return userAccount;
        }

        public async Task UpdateUser(UserAccount userAccount)
        {
            var foundUser = await unitOfWork.UserRepository.GetByIdAsync(userAccount.Id);
            if (foundUser == null)
            {
                throw new NotFoundException(userAccount.Id, "user");
            }

            foundUser.FirstName = userAccount.FirstName;
            foundUser.LastName = userAccount.LastName;

            unitOfWork.UserRepository.Update(foundUser);
            await unitOfWork.SaveAsync();
        }
    }
}
