using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;
    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    [Route("example")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherForecastService.Get();
    }

    [HttpGet("{take}/currentDay")]
    public IActionResult GetWeatherForecasts([FromQuery] int max, [FromRoute] int take)
    {
        var result = _weatherForecastService.Get().First();

        //Response.StatusCode = StatusCodes.Status200OK;

        //return StatusCode(200, result); - same thing as before
        return Ok(result);
    }

    [HttpPost("generate")]
    public IActionResult Generate([FromQuery]int count, [FromBody] TemperatureRequest request)
    {
        if(count < 0 || request.Max < request.Min)
        {
            return BadRequest("Count has to be positive number, and max must be greater that the min value");
        }

        var result = _weatherForecastService.Get(count, request.Min, request.Max);
        return Ok(result);
    }

    [HttpPost]
    public string Hello([FromBody] string name)
    {
        return $"Hello {name}";
    }
}

public class TemperatureRequest
{
    public int Min {  get; set; }
    public int Max {  get; set; }
}
