using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace HelloDotnets
{
    public class WeatherClient
    {

        private HttpClient httpClient { get; set; }

        private WeatherSetting setting { get; set; }

        public WeatherClient(HttpClient client, IOptions<WeatherSetting> options)
        {
            httpClient = client;
            setting = options.Value;
        }

        public async Task<Forcast> GetCurrentWeatherAsync(string city)
        {
            
            return await httpClient.GetFromJsonAsync<Forcast>($"https://{setting.WeatherHost}?q={city}&appid={setting.ApiKey}");
        }
        public record Weather(string description);

        public record Main(decimal temp);

        public record Forcast(List<Weather> weather, Main main, long dt);

    }
}