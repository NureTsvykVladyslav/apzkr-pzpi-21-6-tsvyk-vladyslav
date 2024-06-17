using System;
using System.Threading.Tasks;
using ApexiBeeMobile.DTO;
using ApexiBeeMobile.Models;

namespace ApexiBeeMobile.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignIn(string username, string password);

        Guid GetUserAccountIdFromToken();

        Task<User> GetUserById(Guid id);

        Task<bool> UpdateUserProfile(UpdateUserModel model);
    }
}
