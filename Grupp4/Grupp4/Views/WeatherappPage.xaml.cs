using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherappPage : ContentPage
    {
        RestService _restService;
        CancellationTokenSource cts;
        public WeatherappPage()
        {
            InitializeComponent();
            _restService = new RestService();
            OnGetWeatherButtonClicked();
        }
        async void OnGetWeatherButtonClicked()
        {
            
                WeatherData weatherData = await _restService.GetWeatherData(await GetCurrentLocation(Constants.WeatherEndpoint));
                BindingContext = weatherData;
            
        }
        async Task<string> GetCurrentLocation(string endpoint)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                string requestUri = endpoint;

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
                    requestUri += $"?lat={location.Latitude}";
                    requestUri += $"&lon={location.Longitude}";
                    requestUri += "&units=metric";
                    requestUri += $"&APPID={Constants.WeatherAPIKey}";
                }
                return requestUri;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                return null;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                return null;
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                return null;
            }
            catch (Exception ex)
            {
                // Unable to get location
                return null;
            }
        }

        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }
    }
}