using EtelfutarAPI.Models;
using MySqlX.XDevAPI;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using VizsgaremekAPI.Controllers;
using VizsgaremekAPI.DTOs;
using Xunit;

namespace EtelfutarAPI.Controllers.Tests
{
    public class ChainControllerTests
    {
        [Fact()]
        public async void GetChainAsyncTest()
        {
            //Arrange
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            string url = "/Chain/GetChainAsync";

            //Act
            var result = await client.GetAsync(url);

            //Assert
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
        public async void PostChainAsyncTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            string url = "/Chain/PostChainAsync";

            Chain ujChain = new Chain
            {
                Id = 0,
                Nev = ""
            };
            var result = await client.PostAsync($"api/Login/GetSalt/TakacsL", new StringContent("asdfgh", Encoding.UTF8, "text/plain"));
            string salt = await result.Content.ReadAsStringAsync();
            string tmpHash = CreateSHA256("asdfgh" + salt);
            LoginDTO loginDTO = new LoginDTO()
            {
                LoginName = "TakacsL",
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
            string requestjson = JsonSerializer.Serialize(ujChain, JsonSerializerOptions.Default);
            var requestbody = new StringContent(requestjson, Encoding.UTF8, "application/json");
            var requestResult = await client.PostAsync(url, requestbody);

            Xunit.Assert.Equal("OK", requestResult.StatusCode.ToString());
        }

        [Fact()]
        public async void PutChainAsyncTest()
        {

        }
    }
}