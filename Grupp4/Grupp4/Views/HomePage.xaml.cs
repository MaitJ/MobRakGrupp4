using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grupp4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        void logButtonClicked(object sender, EventArgs e)
        {
            logList.Children.Add(new Label
            {
                Text = "Logged time: " + DateTime.Now.ToString("T")
            });
        }
    }
}