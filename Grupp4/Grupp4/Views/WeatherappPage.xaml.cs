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
            _restService = new RestService();
            _weatherService = new WeatherService(_restService);
            InitializeComponent();
        }
        private string GetCurrentTime()
        {
            DateTime now = DateTime.Now;
            return now.DayOfWeek.ToString();
        }

        private void refreshForecasts(WeatherDataForecast forecastData)
        {
            if (forecastData.daily == null)
            {
                return;
            }

            _forecasts.Clear();

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
                _forecasts.Add(forecast);
                ForecastView.ItemsSource = Forecasts;
            }

        }

        protected async Task FetchWeather()
        {
            WeatherDataForecast forecastData = await _weatherService.GetCurrentLocationForecast();
            refreshForecasts(forecastData);
            weatherData = await _weatherService.GetCurrentLocationData();

            if (weatherData.Title != null)
            {
                LocationName = weatherData.Title;
                CurrentTemperature = weatherData.Main.Temperature;
                CurrentDay = GetCurrentTime();
                Weather = weatherData.Weather[0].Visibility;
                WindSpeed = weatherData.Wind.Speed;
                Humidity = weatherData.Main.Humidity;
                Visibility = weatherData.Visibility;
            }
            BindingContext = this;
        }


        protected async Task FetchWeatherByLonLat(double lon, double lat)
        {
            WeatherDataForecast forecastData = await _weatherService.GetLocationForecast(lon,lat);
            refreshForecasts(forecastData);
            weatherData = await _weatherService.GetLocationData(lon, lat);

            if (weatherData.Title != null)
            {
                LocationName = weatherData.Title;
                CurrentTemperature = weatherData.Main.Temperature;
                CurrentDay = GetCurrentTime();
                Weather = weatherData.Weather[0].Visibility;
                WindSpeed = weatherData.Wind.Speed;
                Humidity = weatherData.Main.Humidity;
                Visibility = weatherData.Visibility;
            }
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
           
            if (BindingContext != null)
            {
                string test = BindingContext.ToString();
             
                if (test== "Grupp4.Place") 
                {
              
                    var Place = (Place)BindingContext;

                    if (Place != null)
                    {

                        string requestUri = Constants.WeatherEndpoint;
                        requestUri += $"?q={Place.Name}";
                        requestUri += "&units=metric";
                        requestUri += $"&APPID={Constants.WeatherAPIKey}";
                        WeatherData weatherData = await _restService.GetWeatherData(requestUri);

                        double longitude = weatherData.Coord.Lon;
                        double latitude = weatherData.Coord.Lat;

                        if (longitude != 0 && latitude != 0)
                        {
                            await FetchWeatherByLonLat(longitude, latitude);
                        }
                        else
                        {
                            await FetchWeather();
                        }
                    }

                }
                else
                {
                    await FetchWeather();
                }

            }
            else
            {
                await FetchWeather();
            }

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
        
    }
}