using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraPage : ContentPage
    {
        public CameraPage()
        {
            InitializeComponent();

            imgViewPanel.IsVisible = false;

        }

        private void CaptureImage(object sender, EventArgs e)
        {
           
            xctCameraView.Shutter();
        }
      
        private void MediaCaptured(object sender, MediaCapturedEventArgs e)
        {

            imgView.Source = e.Image;
            imgViewPanel.IsVisible = true;
        }

        private void CloseImageView(object sender, EventArgs e)
        {
            imgViewPanel.IsVisible = false;
        }
    }
}