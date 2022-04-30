using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherappPage : ContentPage
    {
        RestService _restService;
        public WeatherappPage()
        {
            InitializeComponent();
            _restService = new RestService();
            
        }

        public ImageSource ProductImage
        {
            get
            {
                var source = new Uri("http://openweathermap.org/img/wn/10d@2x.png");
                return source;
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