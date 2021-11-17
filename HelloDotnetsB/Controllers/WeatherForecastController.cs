using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HelloDotnetsB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public readonly ConsulSetting _consulOption;
        private readonly IHttpClientFactory _httpClientFactory;
        public WeatherForecastController(IOptions<ConsulSetting> consulOption, IHttpClientFactory httpClientFactory)
        {
            _consulOption = consulOption.Value;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            using (var consulClient = new ConsulClient(a => a.Address = new Uri(_consulOption.Address)))
            {
                var services = consulClient.Catalog.Service("service-a").Result.Response;
                if (services != null && services.Any())
                {
                    // 模拟随机一台进行请求，这里只是测试，可以选择合适的负载均衡框架
                    var service = services.ElementAt(new Random().Next(services.Count()));

                    var client = _httpClientFactory.CreateClient();
                    var response = await client.GetAsync($"http://{service.ServiceAddress}:{service.ServicePort}/WeatherForecast/Seattle");
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            return "Not Found";
        }
    }
}
