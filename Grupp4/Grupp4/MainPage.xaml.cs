using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grupp4
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        int nameCount = 1;
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            var newNameLabel = new Label();
            newNameLabel.Text = $"{nameCount}. {FirstNameField.Text}";
            newNameLabel.Margin = new Thickness(30, 1, 30, 1);

            NameList.Children.Add(newNameLabel);
            ++nameCount;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
