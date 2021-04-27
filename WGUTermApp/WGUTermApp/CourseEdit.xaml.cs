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
			List<String> statuses = new List<string>()
			{
				"In Progress", "Completed", "Dropped", "Planned", "Incomplete"
			};
			InitializeComponent();
			foreach (string item in statuses)
			{
				statusPicker.Items.Add(item);
			}
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