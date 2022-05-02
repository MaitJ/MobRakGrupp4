using Grupp4.Helpers;
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

            Theme.SetTheme();

            MainPage = new AppShell();
            _restService = new RestService();
            _weatherService = new WeatherService(_restService);
        }

        protected override void OnStart()
        {
            OnResume();
            _weatherService.GetCurrentLocation();

        }

        protected override void OnSleep()
        {
            Theme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            _weatherService.GetCurrentLocation();
            Helpers.Theme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            Theme.SetTheme();
        }
    }
}
