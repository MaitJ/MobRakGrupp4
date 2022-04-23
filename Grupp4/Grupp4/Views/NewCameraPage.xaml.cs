using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms;
using NativeMedia;

namespace Grupp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCameraPage : ContentPage
    {
        public NewCameraPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            var stream = await photo.OpenReadAsync();

            Photoresult.Source = ImageSource.FromStream(() => stream);
            await MediaGallery.SaveAsync(MediaFileType.Image, await photo.OpenReadAsync(), "photo.png");
        }

        private async void Button_Clicked2(object sender, EventArgs e)
        {
            var photos = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "vali pilt"
            });
            var stream = await photos.OpenReadAsync();

            Photoresult.Source = ImageSource.FromStream(() => stream);
        }
    }
}