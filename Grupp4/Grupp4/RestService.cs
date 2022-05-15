using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Grupp4
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();

            if (Device.RuntimePlatform == Device.UWP)
            {
                _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            }
        }

        public async Task<List<Repository>> GetRepositoriesAsync(string uri)
        {
            List<Repository> repositories = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    repositories = JsonConvert.DeserializeObject<List<Repository>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return repositories;
        }
        public async Task<WeatherData> GetWeatherDataByLoc(Location loc)
        {
            string requestUri = Constants.WeatherEndpoint;
            requestUri += $"?lat={loc.Latitude}&lon={loc.Longitude}";
            requestUri += "&units=metric";
            requestUri += $"&APPID={Constants.WeatherAPIKey}";

            WeatherData data = await GetWeatherData(requestUri);

            return data;

        }
        public async Task<WeatherDataForecast> GetWeatherForecastByLoc(Location loc)
        {
            string requestUri = Constants.WeatherForecastEndpoint;
            requestUri += $"?lat={loc.Latitude}&lon={loc.Longitude}";
            requestUri += "&units=metric";
            requestUri += "&exclude=current,minutely,hourly,alerts";
            requestUri += $"&APPID={Constants.WeatherAPIKey}";

            WeatherDataForecast data = await CallWeatherApi<WeatherDataForecast>(requestUri);

            return data;
        }

        public async Task<T> CallWeatherApi<T>(string query)
        {
            T apiData = default(T);
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    apiData = JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return apiData;

        }
        public async Task<WeatherData> GetWeatherData(string query)
        {
            WeatherData weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return weatherData;
        }

        public async Task<string> GetSearchData(string query)
        {
            string searchData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    searchData = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return searchData;
        }

    }
}
