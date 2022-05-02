using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4
{
    public partial class App : Application
    {
        private readonly WeatherService _weatherService;
        private readonly RestService _restService;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            _restService = new RestService();
            _weatherService = new WeatherService(_restService);
        }

        protected override void OnStart()
        {
            _weatherService.GetCurrentLocation();

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            _weatherService.GetCurrentLocation();
        }
    }
}
