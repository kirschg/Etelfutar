using Xunit;
using EtelfutarAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtelfutarAPI.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using VizsgaremekAPI.Controllers;
using VizsgaremekAPI.DTOs;
using System.Security.Cryptography;

namespace EtelfutarAPI.Controllers.Tests
{
    public class RendelesControllerTests
    {
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
        public async void GetRendelesAsyncTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            string url = "/Rendeles/GetRendelesekAsync";

            var postresult = await client.PostAsync($"api/Login/GetSalt/timike", new StringContent("asdfgh", Encoding.UTF8, "text/plain"));
            string salt = await postresult.Content.ReadAsStringAsync();
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
            var result = await client.GetAsync(url);

            Xunit.Assert.Equal("OK", result.StatusCode.ToString());
        }


        [Fact()]
        public async void PostRendelesAsyncTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            string url = "/Rendeles/PostRendelesAsync";

            Rendeles ujRendeles = new Rendeles
            {
                Id = 0,
                FelhasznaloId = 9
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
            string requestjson = JsonSerializer.Serialize(ujRendeles, JsonSerializerOptions.Default);
            var requestbody = new StringContent(requestjson, Encoding.UTF8, "application/json");
            var requestResult = await client.PostAsync(url, requestbody);

            Xunit.Assert.Equal("OK", requestResult.StatusCode.ToString());
        }
    }
}