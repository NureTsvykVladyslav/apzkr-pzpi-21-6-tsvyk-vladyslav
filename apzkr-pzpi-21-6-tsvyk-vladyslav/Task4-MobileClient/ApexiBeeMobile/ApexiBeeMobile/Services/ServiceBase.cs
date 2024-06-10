using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ApexiBeeMobile.Services
{
    public class ServiceBase
    {
        // Deserialization settings for case insensitivity
        protected JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        protected HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            // Default client settings can be changed
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
    }
}
