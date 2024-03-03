using System;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using proaseguros_test.Models;

namespace MyMvcApp.Controllers
{
  public class SearchController : Controller
  {
    private readonly IHttpClientFactory _clientFactory;

    public SearchController(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }

    public async Task<IActionResult> Index()
    {
      var client = _clientFactory.CreateClient();

      // Set up the request
      var request = new HttpRequestMessage
      {
        Method = HttpMethod.Get,
        RequestUri = new Uri("https://api.foursquare.com/v3/places/search"),
      };

      // Add headers
      request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      // Add authorization header
      request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "fsq3yXvqvcRYB330n3OFwg7dIS1aVasFHFFtu+KNA9IdV/o=");


      // Send the request and handle the response
      using (var response = await client.SendAsync(request))
      {
        if (response.IsSuccessStatusCode)
        {
          var jsonString = await response.Content.ReadAsStringAsync();
          var places = JsonSerializer.Deserialize<SearchResultModel>(jsonString);
          return View(places);
        }
        else
        {
          return View("Error");
        }
      }
    }
  }
}
