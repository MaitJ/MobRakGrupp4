using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamAR.Core.Models.Distance;
using XamAR.Core.Models.Geolocation;
using XamAR.Core.Models.Position;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ARPage : ContentPage
    {
        public ARPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var world = XamAR.World.Instance;
            var sphere = world.CreateModel("sphere");

            var location = new Location(44.823052, 20.447704);
            IPositionSource positionSource = new FixedLocation(location);
            var sphereObject = world.AddModel(location, sphere);
            IDistanceOverride distanceOverride = new FixedDistance(2);
            sphereObject.DistanceOverride = distanceOverride;
        }
    }
}