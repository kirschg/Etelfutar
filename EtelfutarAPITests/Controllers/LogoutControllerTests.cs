using Xunit;
using VizsgaremekAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlX.XDevAPI;
using System.Text.Json;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using VizsgaremekAPI.DTOs;
using System.Net.Http.Headers;

namespace EtelfutarAPI.Controllers.Tests
{
    public class LogoutControllerTests
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
        public async void LogoutTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
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
            string uId = loggedUser.Token;
            
            string url = $"/api/Logout/{uId}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loggedUser.Token);


            string requestjson = JsonSerializer.Serialize(uId, JsonSerializerOptions.Default);
            var requestbody = new StringContent(requestjson, Encoding.UTF8, "application/json");
            var requestResult = await client.PostAsync(url, requestbody);

            Xunit.Assert.Equal("OK", requestResult.StatusCode.ToString());
        }
    }
}