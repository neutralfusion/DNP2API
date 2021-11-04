using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace DNP2.Data
{
    public class APIConnection
    {
        public APIConnection(){}
        
        public async Task<List<Family>> FetchFamiliesAsync(int? id, string? filter)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            using HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage responseMessage =
                await client.GetAsync($"https://localhost:5003/families?id={id}&filter={filter}");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception(@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            string result = await responseMessage.Content.ReadAsStringAsync();

            List<Family> families = JsonSerializer.Deserialize<List<Family>>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return families;
        }

        public async Task<List<Family>> FetchFamiliesAsync()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            using HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage responseMessage =
                await client.GetAsync($"https://localhost:5003/families");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception(@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            string result = await responseMessage.Content.ReadAsStringAsync();

            List<Family> families = JsonSerializer.Deserialize<List<Family>>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return families;
        }

        public async Task CreateFamily(Family family)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            using HttpClient client = new HttpClient(clientHandler);
            string familyAsJson = JsonSerializer.Serialize(family);
            StringContent content = new StringContent(familyAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response =
                await client.PostAsync("https://localhost:5003/Families", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception(@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
        }

        public async Task DeleteFamily(int id)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            using HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:5003/Families/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
        }
        public async Task UpdateFamily(Family family)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            using HttpClient client = new HttpClient(clientHandler);
            string familyAsJson = JsonSerializer.Serialize(family);
            StringContent content = new StringContent(familyAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response =
                await client.PatchAsync($"https://localhost:5003/Families/{family.Id}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task<User> ValidateUser(string username, string password)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            using HttpClient client = new HttpClient(clientHandler);
            HttpResponseMessage responseMessage =
                await client.GetAsync($"https://localhost:5003/user?username={username}&password={password}");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            string result = await responseMessage.Content.ReadAsStringAsync();

            User validatedUser = JsonSerializer.Deserialize<User>(result,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            return validatedUser;
        }
    }
}