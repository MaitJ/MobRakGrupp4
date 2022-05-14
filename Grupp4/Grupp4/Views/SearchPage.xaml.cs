using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        RestService _restService;
        Timer searchTimer;

        public SearchPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();
            

            if (BindingContext != null)
            {
                var Place = (Place)BindingContext;
                Console.WriteLine(Place.Name);


                if (Place != null)
                {



                    string requestUri = Constants.WeatherEndpoint;
                    requestUri += $"?q={Place.Name}";
                    requestUri += "&units=metric";
                    requestUri += $"&APPID={Constants.WeatherAPIKey}";
                    WeatherData weatherData = await _restService.GetWeatherData(requestUri);
                    BindingContext = weatherData;






                }
            }
        }
        async void OnTextChanged(object sender, EventArgs e)
        {
            if (searchTimer != null)
                searchTimer.Stop();
            else 
            {
                searchTimer = new Timer(300);
                searchTimer.Elapsed += async delegate 
                {
                    if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
                    {
                        SearchData searchData = await _restService.GetSearchData(GenerateSearchRequestUri(Constants.CitiesEndPoint));
                    }
                        
                };
            }
            searchTimer.Start();
        }
        async void OnGetWeatherSearch(object sender, EventArgs e)
        {
            if (searchTimer != null)
            {
                searchTimer.Close();
                searchTimer = null;
            }

            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherData weatherData = await _restService.GetWeatherData(GenerateWeatherRequestUri(Constants.WeatherEndpoint));

                BindingContext = weatherData;
            }
        }

        string GenerateWeatherRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&units=metric";
            requestUri += $"&APPID={Constants.WeatherAPIKey}";
            return requestUri;
        }

        string GenerateSearchRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?limit=5";
            requestUri += $"&namePrefix={_cityEntry.Text}";
            requestUri += $"&rapidapi-key={Constants.CitiesAPIKey}";
            return requestUri;
        }

    }


}