using EtelfutarAPI.Models;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using VizsgaremekAPI.Controllers;
using VizsgaremekAPI.DTOs;
using Xunit;

namespace EtelfutarAPI.Controllers.Tests
{
    public class VarosokControllerTests
    {
        [Fact()]
        public async void GetVarosokAsyncTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            string url = "/Varosok/GetVarosokAsync";

            var result = await client.GetAsync(url);

            Xunit.Assert.Equal("OK", result.StatusCode.ToString());
        }

        public static string CreateSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        [Fact()]
        public async void PostVarosAsyncTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            string url = "/Varosok/PostVarosAsync";

            Varosok ujVaros = new Varosok
            {
                Id = 0,
                Nev = "",
                IndexKep = ""
            };
            var result = await client.PostAsync($"api/Login/GetSalt/timike", new StringContent("asdfgh", Encoding.UTF8, "text/plain"));
            string salt = await result.Content.ReadAsStringAsync();
            string tmpHash = CreateSHA256("asdfgh" + salt);
            LoginDTO loginDTO = new LoginDTO()
            {
                LoginName = "timike",
                TmpHash = tmpHash,
            };
            string json = JsonSerializer.Serialize(loginDTO, JsonSerializerOptions.Default);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var postResult = await client.PostAsync("api/Login", body);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true,
            };
            string valaszJson = await postResult.Content.ReadAsStringAsync();
            LoggedUser loggedUser = JsonSerializer.Deserialize<LoggedUser>(valaszJson, options);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            string requestjson = JsonSerializer.Serialize(ujVaros, JsonSerializerOptions.Default);
            var requestbody = new StringContent(requestjson, Encoding.UTF8, "application/json");
            var requestResult = await client.PostAsync(url, requestbody);

            Xunit.Assert.Equal("OK", requestResult.StatusCode.ToString());
        }

        [Fact()]
        public async void PutVarosAsyncTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            string url = "/Varosok/GetVarosokAsync";

            Varosok modositottVaros = new Varosok
            {
                Id = 0,
                Nev = "",
                IndexKep = ""
            };
            var result = await client.PostAsync($"api/Login/GetSalt/timike", new StringContent("asdfgh", Encoding.UTF8, "text/plain"));
            string salt = await result.Content.ReadAsStringAsync();
            string tmpHash = CreateSHA256("asdfgh" + salt);
            LoginDTO loginDTO = new LoginDTO()
            {
                LoginName = "timike",
                TmpHash = tmpHash,
            };
            string json = JsonSerializer.Serialize(loginDTO, JsonSerializerOptions.Default);
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var postResult = await client.PostAsync("api/Login", body);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true,
            };
            string valaszJson = await postResult.Content.ReadAsStringAsync();
            LoggedUser loggedUser = JsonSerializer.Deserialize<LoggedUser>(valaszJson, options);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);
            string requestjson = JsonSerializer.Serialize(modositottVaros, JsonSerializerOptions.Default);
            var requestbody = new StringContent(requestjson, Encoding.UTF8, "application/json");
            var requestResult = await client.PutAsync(url, requestbody);

            Xunit.Assert.Equal("OK", requestResult.StatusCode.ToString());
        }
    }
}