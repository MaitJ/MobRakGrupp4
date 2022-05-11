using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4
{
    public class Forecast
    {
        public int Id { get; set; }
        public string DayStr { get; set; }
        public string Month { get; set; }
        public string DayMonth { get; set; }
        public double Temperature { get; set; }
        public string Icon { get; set; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherappPage : ContentPage
    {
        private readonly RestService _restService;
        private readonly WeatherService _weatherService;

        private WeatherData weatherData = new WeatherData();

        private double _currentTemperature = 0;
        private string _locationName = "";
        private string _currentDay = "";
        private string _weather = "";
        private double _windSpeed = 0;

        private long _humidity = 0;
        private long _visibility = 0;

        private double _nextDayTemp = 0;

        private ObservableRangeCollection<Forecast> _forecasts = new ObservableRangeCollection<Forecast>(); 

        public ObservableRangeCollection<Forecast> Forecasts
        {
            get => _forecasts;
            set => SetProperty(ref _forecasts, value);
        }

        public double NextDayTemp
        {
            get => _nextDayTemp;
            set => SetProperty(ref _nextDayTemp, value);
        }

        public string Weather
        {
            get => _weather;
            set => SetProperty(ref _weather, value);
        }

        public string CurrentDay
        {
            get => _currentDay;
            set => SetProperty(ref _currentDay, value);
        }

        public string LocationName
        {
            get => _locationName;
            set => SetProperty(ref _locationName, value);
        }

        public double CurrentTemperature
        {
            get => _currentTemperature;
            set => SetProperty(ref _currentTemperature, value);
        }

        public double WindSpeed
        {
            get => _windSpeed;
            set => SetProperty(ref _windSpeed, value);
        }

        public long Humidity
        {
            get => _humidity;
            set => SetProperty(ref _humidity, value);
        }

        public long Visibility
        {
            get => _visibility;
            set => SetProperty(ref _visibility, value);
        }


        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public WeatherappPage()
        {
            InitializeComponent();
            _restService = new RestService();
            _weatherService = new WeatherService(_restService);
            BindingContext = this;
            
        }
        private string GetCurrentTime()
        {
            DateTime now = DateTime.Now;
            return now.DayOfWeek.ToString();
        }

        /*
        private void createForecastDataTemplate()
        {
            var dataTemplate = new DataTemplate(() =>
            {
                var cell = new ViewCell();
                var grid = new Grid();

                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                for (int i = 0; i < 3; ++i)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                for (int i = 0; i < _forecasts.Count; ++i)
                {
                    Frame frame = new Frame();
                    grid.Children.Add(new Frame() {
                        
                    })
                }


            });
        }
        */

        private void refreshForecasts(WeatherDataForecast forecastData)
        {
            for (int i = 1; i < 8; ++i)
            {
                Forecast forecast = new Forecast();
                DateTimeOffset currentDt = DateTimeOffset.FromUnixTimeSeconds(forecastData.daily[i].Dt);
                DateTime date = currentDt.UtcDateTime;
                forecast.Id = i - 1;
                forecast.DayMonth = $"{date.Day}. {date.ToString("MMMM")}";
                forecast.DayStr = date.DayOfWeek.ToString();
                forecast.Month = date.Month.ToString();
                forecast.Temperature = forecastData.daily[i].Temperature.Day;
                forecast.Icon = forecastData.daily[i].Weather[0].Icon;
                //forecast.Icon = new Uri("http://openweathermap.org/img/wn/10d@2x.png");
                _forecasts.Add(forecast);
            }
            ForecastView.ItemsSource = Forecasts;

        }

        protected override async void OnAppearing()
        {
            WeatherDataForecast forecastData = await _weatherService.GetCurrentLocationForecast();
            refreshForecasts(forecastData);
            weatherData = await _weatherService.GetCurrentLocationData();

            LocationName = weatherData.Title;
            CurrentTemperature = weatherData.Main.Temperature;
            CurrentDay = GetCurrentTime();
            Weather = weatherData.Weather[0].Visibility;
            WindSpeed = weatherData.Wind.Speed;
            Humidity = weatherData.Main.Humidity;
            Visibility = weatherData.Visibility;

            base.OnAppearing();
        }

        public ImageSource ProductImage
        {
            get
            {
                var source = new Uri("http://openweathermap.org/img/wn/10d@2x.png");
                return source;
            }
        }

        public void OnHeartTapped(object sender, EventArgs args)
        {

            try
            {
                //Siia kood, et votaks andmed ja lisaks siis favourites listi
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void OnSearchTapped(object sender, EventArgs args)
        {

            try
            {
                //Siia kood, et toggleks search bari
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //async void ongetweatherbuttonclicked(object sender, eventargs e)
        //{
        //    if (!string.isnullorwhitespace(_cityentry.text))
        //    {
        //        weatherdata weatherdata = await _restservice.getweatherdata(generaterequesturi(constants.weatherendpoint));
        //        bindingcontext = weatherdata;
        //    }
        //}

        //string generaterequesturi(string endpoint)
        //{
        //    string requesturi = endpoint;
        //    requesturi += $"?q={_cityentry.text}";
        //    requesturi += "&units=metric";
        //    requesturi += $"&appid={constants.weatherapikey}";
        //    return requesturi;
        //}
    }
}