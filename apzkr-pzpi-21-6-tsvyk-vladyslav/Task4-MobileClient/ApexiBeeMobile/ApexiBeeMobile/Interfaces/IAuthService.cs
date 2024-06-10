using ApexiBeeMobile.DTO;
using ApexiBeeMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
