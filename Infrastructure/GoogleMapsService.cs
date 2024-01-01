using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Models;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private const string API_URL = "https://maps.googleapis.com/maps/api/geocode/json";

        public async Task<GoogleMapsGeocodingResponse> GetAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                return null;

            var encodedAddress = Uri.EscapeDataString(address.Replace(' ', '+'));

            return await SendRequest($"address={encodedAddress}");
        }

        public async Task<GoogleMapsGeocodingResponse> GetAddressByPostalCode(string postalCode)
        {
            return string.IsNullOrEmpty(postalCode) ? null : await SendRequest($"components=postal_code:{postalCode}");
        }

        private async Task<GoogleMapsGeocodingResponse> SendRequest(string parameters)
        {
            var apiKey = Environment.GetEnvironmentVariable("GOOGLE_MAPS_API_KEY");

            var apiUrl = $"{API_URL}?{parameters}&key={apiKey}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GoogleMapsGeocodingResponse>(content);

                return result;
            }
        }

    }

    public interface IGoogleMapsService
    {
        Task<GoogleMapsGeocodingResponse> GetAddress(string address);
        Task<GoogleMapsGeocodingResponse> GetAddressByPostalCode(string postalCode);
    }
}
