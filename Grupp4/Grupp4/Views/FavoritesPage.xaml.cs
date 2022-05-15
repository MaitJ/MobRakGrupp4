using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesPage : ContentPage
    {
        RestService _restService;
        Timer searchTimer;
        Task searchTask;
        public FavoritesPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            var Places = await App.PlaceDatabase.GetPlacesAsync();

            if (Places != null)
            {
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
        }



        private void ListView_Refreshing(object sender, EventArgs e)
        {
            RefreshList();
            listView.EndRefresh();
        }
        
        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                var Places = await App.PlaceDatabase.GetPlacesAsync();
                var myItem = Places.Find(Place => Place.Name == nameEntry.Text);
                if (myItem == null)
                {
                    await App.PlaceDatabase.SavePlaceAsync(new Place
                    {
                        Name = nameEntry.Text
                    });


                    nameEntry.Text = string.Empty;


                    RefreshList();
                }
                else
                {
                    DisplayAlert("Notice", String.Format("{0} already in Favorites!", nameEntry.Text), "OK");
                }
            }
        }

       

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var btn = (MenuItem)sender;
            var place = (Place)btn.BindingContext;
            await App.PlaceDatabase.DeletePlaceAsync(place);


            RefreshList();
        }



        async void RefreshList()
        {
            var Places = await App.PlaceDatabase.GetPlacesAsync();

            if (Places != null)
            {
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
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var btn = (ListView)sender;
            var place = (Place)btn.SelectedItem;

            await Navigation.PushAsync(new WeatherappPage
            {
                BindingContext = place,
            });
        }

        async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var btn = (SwipeItem)sender;
            var place = (Place)btn.BindingContext;
            await App.PlaceDatabase.DeletePlaceAsync(place);

            DisplayAlert("Notice", String.Format("{0} is deleted!", place.Name), "OK");

            RefreshList();
        }

        async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchTask == null || searchTask.IsCompleted) 
            {
                searchTask = Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    
                    ObservableCollection<string> searchList = new ObservableCollection<string>();
                    await GetSearchAPICallback(searchList);

                    for (int i = 0; i < searchList.Count; i++)
                    {
                        Console.WriteLine(searchList[i]);
                    };

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        searchResults.IsVisible = true;
                        searchResults.BeginRefresh();
                        BindingContext = searchList;

                        try
                        {
                            var dataEmpty = searchList.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
                            Console.WriteLine(e.NewTextValue);
                            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                                searchResults.IsVisible = false;
                            else if (dataEmpty.Max().Length == 0)
                                searchResults.IsVisible = false;
                            else if (e.NewTextValue == searchList[0])
                                searchResults.IsVisible = false; 
                            else
                                searchResults.ItemsSource = searchList.Where(i => i.ToLower().Contains(e.NewTextValue.ToLower()));
                        }
                        catch (Exception ex)
                        {
                            searchResults.IsVisible = false;

                        }
                        searchResults.EndRefresh();
                    });
                });
            }
            
        }


        private async Task GetSearchAPICallback(ObservableCollection<string> searchList)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                string searchData = await _restService.GetSearchData(GenerateSearchRequestUri(Constants.CitiesEndPoint));
                if (!string.IsNullOrWhiteSpace(searchData))
                {
                    var searchItem = JsonConvert.DeserializeObject<SearchData>(searchData);

                    for (int i = 0; i < searchItem.data.Count; i++)
                    {
                        searchList.Add(searchItem.data[i].name);
                    };
                }
            }
        }

        private void OnSearchTapped(Object sender, ItemTappedEventArgs e)
        {

            String listsd = e.Item as string;
            nameEntry.Text = listsd;
            searchResults.IsVisible = false;

            ((ListView)sender).SelectedItem = null;
        }

        string GenerateSearchRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?limit=5";
            requestUri += $"&namePrefix={nameEntry.Text}";
            requestUri += $"&minPopulation=5000";
            requestUri += $"&rapidapi-key={Constants.CitiesAPIKey}";
            return requestUri;
        }
    }
}