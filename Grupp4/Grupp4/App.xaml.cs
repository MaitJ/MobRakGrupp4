using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4
{
    public partial class App : Application
    {
        private static PlaceDatabase _placeDatabase;

        public static PlaceDatabase PlaceDatabase
        {
            get
            {
                if (_placeDatabase == null)
                {
                    _placeDatabase = new 
                        PlaceDatabase(Path.Combine(Environment.GetFolderPath(Environment
                        .SpecialFolder.LocalApplicationData), "places.db3"));
                }
                return _placeDatabase;

            }
        }
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
