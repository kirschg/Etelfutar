using EtelfutarAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Text.Json;
using Xunit;
using static System.Net.WebRequestMethods;

namespace EtelfutarAPI.Controllers.Tests
{
    public class RegistryControllerTests
    {
        [Fact()]
        public void PostregistryTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };

            string url = $"/api/Registry";

            Felhasznalok ujRegisztracio = new Felhasznalok
            {
                Email = "timkoa@kkszki.hu",
                FelhasznaloNev = "timike",
                TeljesNev = "Timkó Ákos",
                Hash = "2592d23d53bc62c8f324ccfee9b1092078bbf48b6826a64871408ebfd1af667a",
                VarosId = 2,
                Lakcim = "Otthon Utca 49",
                Salt = "VLTIAPZvfcFzkY6fc0Vc8OGk0WMjmRvYZ0QVozRHq7IBotj8hMn4w5GqqyPh"
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true,
            };

            string requestjson = JsonSerializer.Serialize(ujRegisztracio, JsonSerializerOptions.Default);
            var requestbody = new StringContent(requestjson, Encoding.UTF8, "application/json");
            var requestResult = client.PostAsync(url, requestbody);

            Xunit.Assert.Equal("OK", requestResult.Status.ToString());
        }
        
        [Fact()]
        public void GetregistryTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };

            string felhasznaloNev = "timike";
            string email = "timkoa@kkszki.hu";

            string url = $"api/Registry?felhasznaloNev={felhasznaloNev}&email={email}";
            
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNameCaseInsensitive = true,
            };

            string requestjson = JsonSerializer.Serialize(JsonSerializerOptions.Default);
            var requestbody = new StringContent(requestjson, Encoding.UTF8, "application/json");
            var requestResult = client.PostAsync(url, requestbody);

            Xunit.Assert.Equal("OK", requestResult.Status.ToString());
        }
    }
}