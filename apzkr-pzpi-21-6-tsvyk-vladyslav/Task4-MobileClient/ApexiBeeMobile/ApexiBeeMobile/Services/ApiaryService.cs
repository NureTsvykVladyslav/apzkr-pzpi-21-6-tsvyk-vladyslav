using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ApexiBeeMobile.Services
{
    public class ApiaryService : ServiceBase, IApiaryService
    {
        private readonly IAuthService _authService;

        private readonly string apiUrl = Preferences.Get("ApiUrl", string.Empty) + "/api/Apiary";

        public ApiaryService()
        {
            this._authService = DependencyService.Get<IAuthService>();
        }

        public async Task<IEnumerable<Hive>> GetApiaryHives(Guid apiaryId)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"{apiUrl}/hives/{apiaryId}");
            return JsonSerializer.Deserialize<IEnumerable<Hive>>(result, options);
        }

        public async Task<IEnumerable<Apiary>> GetUserApiaries()
        {
            Guid userAccountId = this._authService.GetUserAccountIdFromToken();

            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"{apiUrl}/user/{userAccountId}");
            return JsonSerializer.Deserialize<IEnumerable<Apiary>>(result, options);
        }
    }
}
