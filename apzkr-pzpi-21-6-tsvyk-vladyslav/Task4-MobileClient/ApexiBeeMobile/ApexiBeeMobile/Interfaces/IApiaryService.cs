using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApexiBeeMobile.Models;

namespace ApexiBeeMobile.Interfaces
{
    public interface IApiaryService
    {
        Task<IEnumerable<Apiary>> GetUserApiaries();

        Task<IEnumerable<Hive>> GetApiaryHives(Guid apiaryId);
    }
}
