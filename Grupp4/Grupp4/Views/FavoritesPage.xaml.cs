using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesPage : ContentPage
    {
        RestService _restService;
        public FavoritesPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            //collectionView.ItemsSource = await App.PlaceDatabase.GetPlacesAsync();
            var Places = await App.PlaceDatabase.GetPlacesAsync();


            foreach (var item in Places)
            {
                //Console.WriteLine(item.Name);
                //Console.WriteLine(item.Id);



                string requestUri = Constants.WeatherEndpoint;
                requestUri += $"?q={item.Name}";
                requestUri += "&units=metric";
                requestUri += $"&APPID={Constants.WeatherAPIKey}";
                WeatherData weatherData = await _restService.GetWeatherData(requestUri);
                BindingContext = weatherData;
                item.Temperature = weatherData.Main.Temperature;
                item.Wind = weatherData.Wind.Speed;
                item.Humidity = weatherData.Main.Humidity;
                item.Visibility = weatherData.Weather[0].Visibility;
                //Console.WriteLine(item.Temperature);


            }
            listView.ItemsSource = Places;
        }



        private void ListView_Refreshing(object sender, EventArgs e)
        {
            RefreshList();
            listView.EndRefresh();
        }
        /*private async double GetWeather(string Name)
        {

            string requestUri = Constants.WeatherEndpoint;
            requestUri += $"?q={Name}";
            requestUri += "&units=metric";
            requestUri += $"&APPID={Constants.WeatherAPIKey}";
            WeatherData weatherData = await _restService.GetWeatherData(requestUri);
            double temperature = weatherData.Main.Temperature;
                
            return temperature;

        }
        async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherData weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.WeatherEndpoint));
                BindingContext = weatherData;
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&units=metric";
            requestUri += $"&APPID={Constants.WeatherAPIKey}";
            return requestUri;
        }



        string GetNameOf(object topLevelXaml)
        {
            var fields = topLevelXaml.GetType().GetFields();
            foreach (var fi in fields)
            {
                var value = fi.GetValue(topLevelXaml);
                if (value!= "Name")
                    continue;
                return fi.Name;
            }
            return null;
        }*/


        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.PlaceDatabase.SavePlaceAsync(new Place
                {
                    Name = nameEntry.Text
                });


                nameEntry.Text = string.Empty;


                RefreshList();
            }

        }

       

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var btn = (Label)sender;
            var place = (Place)btn.BindingContext;
            await App.PlaceDatabase.DeletePlaceAsync(place);


            RefreshList();
        }



        async void RefreshList()
        {
            var Places = await App.PlaceDatabase.GetPlacesAsync();


            foreach (var item in Places)

            { 
                string requestUri = Constants.WeatherEndpoint;
                requestUri += $"?q={item.Name}";
                requestUri += "&units=metric";
                requestUri += $"&APPID={Constants.WeatherAPIKey}";
                WeatherData weatherData = await _restService.GetWeatherData(requestUri);
                BindingContext = weatherData;
                item.Temperature = weatherData.Main.Temperature;
                item.Wind = weatherData.Wind.Speed;
                item.Humidity = weatherData.Main.Humidity;
                item.Visibility = weatherData.Weather[0].Visibility;
                


            }
            listView.ItemsSource = Places;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView btn = (ListView)sender;
            await Navigation.PushAsync(new SearchPage());


        }

    }
}