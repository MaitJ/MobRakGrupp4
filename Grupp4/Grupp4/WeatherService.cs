using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Grupp4
{
    class WeatherService
    {
        private readonly RestService _restService;
        CancellationTokenSource cts;
        public WeatherService(RestService restService)
        {
            this._restService = restService;
            cts = new CancellationTokenSource();
        }
        private async Task<Location> GetLocationAsync()
        {
            try
            {
                Location location = await Geolocation.GetLastKnownLocationAsync();
                
                if (location == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    location = await Geolocation.GetLocationAsync(request, cts.Token);
                }

                return location;
            } catch (Exception ex)
            {
                Debug.WriteLine("Couldn't receive last known location, exception: ", ex.Message);
                return new Location(37.783333, -122.416667);

            }

        }
        public async Task<WeatherData> GetCurrentLocationData()
        {
            Location location = await GetLocationAsync();
            WeatherData data = await _restService.GetWeatherDataByLoc(location);
            return data;
        }

        public async Task<WeatherDataForecast> GetCurrentLocationForecast()
        {
            Location location = await GetLocationAsync();
            WeatherDataForecast data = await _restService.GetWeatherForecastByLoc(location);
            return data;
        }
        
    }
}
