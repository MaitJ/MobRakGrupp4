using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Grupp4
{
    public partial class WeatherPage : ContentPage
    {
        RestService _restService;
        private string url;
        private string lat;
        private string lng;


        public WeatherPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            List<Repository> repositories = await _restService.GetRepositoriesAsync(Constants.GitHubReposEndpoint);
            collectionView.ItemsSource = repositories;
        }
    }
}