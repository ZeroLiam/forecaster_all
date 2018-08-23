using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using forecaster_backend.Models;

// NOTE! I am noob in backend, I apologize if you see an amateur practice
// ANOTHER NOTE! Most of this was taken from the documentation from 
//               Microsoft. The other part was me trying to catch errors (hopefully).
namespace forecaster_backend.Controllers
{
    // This is the router for the api/weather url
    [Route("api/weather")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly ForecastContext _context;

        public ForecastController(ForecastContext context)
        {
            _context = context;
            //Don't return anything yet... I don't know what to do with this context.

              // _context.ForecastItems.ToList();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            // This is the default route. for api/weather. It should give a 200 response code with this message.
            //The front end has the url beyond api/weather, so I doubt you can see it in the app.
            //However, feel free to test it  and break it :)
            return Ok(new{
                Msg = "Please complete the url as ['/api/weather/forecast?city=german_city_or_zipcode'] "
            });
        }


        [HttpGet("{forecast}",Name = "GetForecast")]
        public async Task<IActionResult> GetForecast(string forecast, [FromQuery]string city)
        {
            // Here is where we request the city. URL should be api/weather/forecast?city=something
             using (var client = new HttpClient())
            {
                try
                {
                    //The base address from OpenWeather
                    string baseurl = "http://api.openweathermap.org";
                    //API key for OpenWeather
                    string apikey = "fcadd28326c90c3262054e0e6ca599cd";
                    client.BaseAddress = new Uri(baseurl);
                    var response = await client.GetAsync($"/data/2.5/forecast?q={city},de&appid={apikey}&units=metric");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    //Let's see the response in the console...
                    Console.WriteLine(stringResult);

                    // Get the JSON and map it to OpenWeather model.
                    // Fun fact! I found an online tool that generates the classes I need
                    // from the JSON on the response. This was very useful! http://json2csharp.com/
                    var getNewForecast = JsonConvert.DeserializeObject<OpenWeather>(stringResult);

                    //Now, only retrieve the city name if found, and the forecast list
                    // from the OpenWeather API. We'll deal with the data in the frontend.
                    //(It's easier for me to deal with it in the frontend)
                    return Ok(new {
                        City = getNewForecast.City.name,
                        Forecast = getNewForecast.List.AsEnumerable()
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    // If you find an error in the request, return the error
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                   
                }
                catch (NullReferenceException nullRequestException)
                {
                    // If your object is null because it's being mapped wrong, also throw the error. Don't let the user wonder what's going on.
                    return BadRequest($"Error: {nullRequestException.Message}");
                }
            }
        }
    }
}