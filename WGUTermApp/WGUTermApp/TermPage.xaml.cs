using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUTermApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
    {
        public TermPage()
        {
            InitializeComponent();
        }

        private async void CancelButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}