using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Grupp4
{
    class WeatherService
    {
        private readonly RestService _restService;
        Location lastKnownLoc;
        public WeatherService(RestService restService)
        {
            this._restService = restService;
        }
        public async Task<WeatherData> GetCurrentLocationData()
        {
            try
            {
                lastKnownLoc = await Geolocation.GetLastKnownLocationAsync();

                if (lastKnownLoc == null)
                    return new WeatherData();

                WeatherData data = await _restService.GetWeatherDataByLoc(lastKnownLoc);
                return data;


            } catch (Exception ex)
            {
                //Set some default location
                Debug.WriteLine("Couldn't receive last known location, exception: ", ex.Message);
                lastKnownLoc = new Location(37.783333, -122.416667);
            }

            return new WeatherData();
        }

        public async Task<WeatherDataForecast> GetCurrentLocationForecast()
        {
            try
            {
                lastKnownLoc = await Geolocation.GetLastKnownLocationAsync();

                if (lastKnownLoc == null)
                    return new WeatherDataForecast();

                WeatherDataForecast data = await _restService.GetWeatherForecastByLoc(lastKnownLoc);
                return data;


            } catch (Exception ex)
            {
                //Set some default location
                Debug.WriteLine("Couldn't receive last known location, exception: ", ex.Message);
                lastKnownLoc = new Location(37.783333, -122.416667);
            }

            return new WeatherDataForecast();
        }
        
    }
}
