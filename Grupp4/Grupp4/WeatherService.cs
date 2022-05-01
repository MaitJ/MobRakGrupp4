using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
        public async void GetCurrentLocation()
        {
            try
            {
                lastKnownLoc = await Geolocation.GetLastKnownLocationAsync();
                if (lastKnownLoc == null)
                    return;

                WeatherData data = await _restService.GetWeatherDataByLoc(lastKnownLoc);

                Debug.WriteLine($"Current temp: {data.Main.Temperature}, City name: {data.Title}");


            } catch (Exception ex)
            {
                //Set some default location
                Debug.WriteLine("Couldn't receive last known location, exception: ", ex.Message);
                lastKnownLoc = new Location(37.783333, -122.416667);
            }
        }

        
    }
}
