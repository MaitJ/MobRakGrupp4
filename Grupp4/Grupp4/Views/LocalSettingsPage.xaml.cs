using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Grupp4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalSettingsPage : ContentPage
    {
        public LocalSettingsPage()
        {
            InitializeComponent();
            Label1.Text = Preferences.Get("text", string.Empty);
            Checkbox1.IsChecked = Preferences.Get("checkbox", false);
            Switch1.IsToggled = Preferences.Get("switch", false);
        }

        void ButtonClicked(System.Object sender, System.EventArgs e)
        {
            Preferences.Set("text", Label1.Text);
            Preferences.Set("checkbox", Checkbox1.IsChecked);
            Preferences.Set("switch", Switch1.IsToggled);
        }

        void ButtonClicked2(System.Object sender, System.EventArgs e)
        {
            Preferences.Clear();
        }
    }
}