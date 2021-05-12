using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGUTermApp.Models;

namespace WGUTermApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CourseEdit : ContentPage
	{
		
		public CourseEdit ()
		{			
			InitializeComponent();
			List<Person> people = App.Tables.GetAllPeople();
			List<Course> courses = App.Tables.GetAllCourses();
			Name.Text = courses.ElementAt(0).Name;
			Start.Date = courses.ElementAt(0).Start;
			End.Date = courses.ElementAt(0).End;
			statusPicker.SelectedItem = courses.ElementAt(0).Status;
			Instructor.Text = people.ElementAt(0).Name;
			Performance.Text = courses.ElementAt(0).Performance;
			Objective.Text = courses.ElementAt(0).Objective;
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