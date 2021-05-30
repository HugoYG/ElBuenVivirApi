using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ElBuenVivir;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using ElBuenVivir.Entities;

namespace ElBuenVivirTesting
{
    public class IntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient _client { get; }

        public IntegrationTest(WebApplicationFactory<Startup> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_Retrieve_Secretarias()
        {
            var response = await _client.GetAsync("/api/secretarias"); 
            response.StatusCode.Should().Be(HttpStatusCode.OK); 
            var forecast = JsonConvert.DeserializeObject<Secretaria[]>(await response.Content.ReadAsStringAsync()); 
            //Retrieve just 2 secretarias.
            //Change the value to get an error
            forecast.Should().HaveCount(3);
        }
    }
}
