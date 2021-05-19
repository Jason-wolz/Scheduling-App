using System;
using SQLite;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUTermApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoursePage : ContentPage
	{        
        public CoursePage ()
		{
			InitializeComponent ();
            var courses = App.Tables.GetAllCourses();
            var course = courses.ElementAt(App.currentClass);
            var people = App.Tables.GetAllPeople();
            var person = people.ElementAt(course.Instructor - 1);
            this.Title = course.Name;
            statusPicker.SelectedItem = course.Status;
            Start.Date = course.Start;
            End.Date = course.End;
            Instructor.Text = person.Name + ", " + person.Phone + ", " + person.Email;
            Objective.Text = course.Objective;
            Performance.Text = course.Performance;
		}

        async private void CancelButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async private void EditButton(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CourseEdit());            
        }
    }
}