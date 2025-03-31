using Xunit;
using EtelfutarAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtelfutarAPI.Controllers.Tests
{
    public class FelhasznalokControllerTests
    {
        [Fact()]
        public async void GetFelhasznaloAsyncTest()
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
            string url = "/Felhasznalok/GetFelhasznalokAsync";

            var result = await client.GetAsync(url);

            Xunit.Assert.Equal("OK", result.StatusCode.ToString());
        }
    }
}