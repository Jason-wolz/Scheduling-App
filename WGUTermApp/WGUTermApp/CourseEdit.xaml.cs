using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TextChangeTrigger;

namespace WGUTermApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CourseEdit : ContentPage
	{
		
		public CourseEdit ()
		{			
			InitializeComponent();            
        }



		//return to previous page without saving
        async private void CancelButton(object sender, EventArgs e)
        {
			await Navigation.PopModalAsync();
        }

		//save, then return to previous page
		async private void SaveButton(object sender, EventArgs e)
        {
			await Navigation.PopModalAsync();
        }
    }
}