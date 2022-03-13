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
	public partial class BindingTest : ContentPage
	{
		public BindingTest ()
		{
			InitializeComponent ();
			label.BindingContext = slider;
			label.SetBinding(Label.RotationProperty, "Value");
		}
	}
}