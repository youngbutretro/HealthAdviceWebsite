using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Weather()
        {
            getData(); 
            return View(thisWeather);
        }

        public static Root thisWeather = new Root();
        public static String location = "Barnsley"; 
        public IActionResult SetLocation()
        {
            return View(); 
        }

        public void getData()
        {
            var client = new RestClient("https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/" + location + "?unitGroup=metric&key=Y2CCUQPTHAT2D5KNMXJFA3C2H&contentType=json");
            var request = new RestRequest("");
            var Response = client.ExecuteGet(request);

            Root weather = JsonConvert.DeserializeObject<Root>(Response.Content);


            thisWeather = weather;
        }

        public IActionResult SetLocationSubmit(Location local)
        {
            location = local.location;
            getData(); 
            return View("Weather",thisWeather); 
        }


        public IActionResult News()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}