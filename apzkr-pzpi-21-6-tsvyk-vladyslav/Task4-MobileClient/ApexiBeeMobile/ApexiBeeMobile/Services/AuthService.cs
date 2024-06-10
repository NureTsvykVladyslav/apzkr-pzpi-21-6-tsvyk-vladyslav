using ApexiBeeMobile.DTO;
using ApexiBeeMobile.Interfaces;
using ApexiBeeMobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.IdentityModel.Tokens.Jwt;

namespace ApexiBeeMobile.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        readonly string apiUrl = Preferences.Get("ApiUrl", "") + "/api/Auth";

        public async Task<bool> SignIn(string username, string password)
        {
            AuthModel model = new AuthModel()
            {
                Username = username,
                Password = password,
            };

            HttpClient client = GetClient();
            string serializedModel = JsonSerializer.Serialize(model);
            var response = await client.PostAsync($"{apiUrl}/login",
                new StringContent(
                    serializedModel,
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                AuthResponse authResponseContentModel = JsonSerializer.Deserialize<AuthResponse>(responseContent, options);
                Preferences.Set("token", authResponseContentModel.Token);
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
                return false;
            else
                // Exception throws if http response code incorrect
                throw new Exception();
        }

        public Guid GetUserAccountIdFromToken()
        {
            var handler = new JwtSecurityTokenHandler();
            string jsonTokenString = Preferences.Get("token", "");
            if (jsonTokenString == "")
                return Guid.Empty;
            var jsonToken = handler.ReadToken(jsonTokenString) as JwtSecurityToken;
            foreach (var claim in jsonToken.Claims)
            {
                string type = claim.Type;
                string value = claim.Value;
                if (type == "accountId")
                    return Guid.Parse(value);
            }
            return Guid.Empty;
        }

        public async Task<User> GetUserById(Guid id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync($"{Preferences.Get("ApiUrl", "")}/api/User/info/{id}");
            return JsonSerializer.Deserialize<User>(result, options);
        }

        public async Task<bool> UpdateUserProfile(UpdateUserModel model)
        {
            HttpClient client = GetClient();
            string serializedUserData = JsonSerializer.Serialize(model);
            var content = new StringContent(serializedUserData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{Preferences.Get("ApiUrl", "")}/api/User", content);
            if (response.StatusCode != HttpStatusCode.OK)
                return false;

            return true;
        }
    }
}
