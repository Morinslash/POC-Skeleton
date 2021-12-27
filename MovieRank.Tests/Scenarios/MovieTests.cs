using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MovieRank.Libs.Models;
using MovieRank.Tests.Setup;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace MovieRank.Tests.Scenarios
{
    [Collection("api")]
    public class MovieTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;
        public MovieTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddMovieRankDataReturnsOkStatus()
        {
            const int userID = 1;
            var movieDb = new MovieDb
            {
                UserID = userID,
                MovieName = "test-MovieName",
                Description = "test-Description",
                Actors = new List<string>
                {
                    "testUser1",
                    "testUser2",
                },
                RankedDateTime = "5/10/2018 6:17:17 PM",
                Ranking = 4
            };

            var json = JsonConvert.SerializeObject(movieDb);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        
            var response = await _client.PostAsync($"movies/{userID}", stringContent);
            var message = await response.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(message);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}