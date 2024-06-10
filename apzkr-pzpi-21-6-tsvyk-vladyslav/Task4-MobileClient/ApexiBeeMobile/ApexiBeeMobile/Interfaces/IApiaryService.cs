using ApexiBeeMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApexiBeeMobile.Interfaces
{
    public interface IApiaryService
    {
        Task<IEnumerable<Apiary>> GetUserApiaries();

        Task<IEnumerable<Hive>> GetApiaryHives(Guid apiaryId);
    }
}
