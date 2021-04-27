using System;
using SQLite;
using Xamarin.Essentials;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUTermApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoursePage : ContentPage
	{
        readonly List<String> statuses = new List<string>()
        {
            "In Progress", "Completed", "Dropped", "Planned", "Incomplete"
        };
        public CoursePage ()
		{
			InitializeComponent ();
            foreach (string item in statuses)
            {
                statusPicker.Items.Add(item);
            }
		}

        async private void CancelButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void EditButton(object sender, EventArgs e)
        {
            //try
            //{
            await Navigation.PushModalAsync(new CourseEdit());
            //}
            //catch (NullReferenceException)
            //{

            //}
        }
    }
}