using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using proaseguros_test.Models;
using System.Text.Json;

namespace proaseguros_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string location, string query, bool openNow)
        {
            var uri = new UriBuilder("https://api.foursquare.com/v3/places/search");

            var parameters = new Dictionary<string, string>
            {
                { "ll", location },
                { "open_now", openNow.ToString() },
                { "query", query }
            };

            Console.WriteLine($"location: {location}, open now: {openNow}, query: {query}");

            IDictionary<string, string?> parametersNullable = parameters;

            var apiKey = "";

            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", apiKey);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "es");

            var response = await client.GetAsync(QueryHelpers.AddQueryString(uri.Uri.ToString(), parametersNullable));
            if (response.IsSuccessStatusCode){
                var responseBody = await response.Content.ReadAsStringAsync();
                var searchResult = JsonSerializer.Deserialize<SearchResultModel>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                string[] parts = location.Split(',');
                searchResult.Context = new ContextModel
                {
                    Latitude = parts[0],
                    Longitude = parts[1]
                };
                var searchResultJson = JsonSerializer.Serialize(searchResult);
                ViewBag.SearchResultJson = searchResultJson;//
                //return RedirectToAction("Index", "result");
                return View("SearchResult", searchResult);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
