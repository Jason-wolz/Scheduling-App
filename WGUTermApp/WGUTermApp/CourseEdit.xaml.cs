using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGUTermApp.Models;
using Plugin.LocalNotifications;

namespace WGUTermApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CourseEdit : ContentPage
	{
		
		public CourseEdit ()
		{			
			InitializeComponent();//add term field
			if (!App.isNew)
			{
				List<Course> courses = App.Tables.GetAllCourses();
				var course = courses.ElementAt(App.currentClass - 1);
				List<Person> people = App.Tables.GetAllPeople();
				var person = people.ElementAt(course.Instructor - 1);
				Name.Text = course.Name;
				Start.Date = course.Start;
				End.Date = course.End;
				statusPicker.SelectedItem = course.Status;
				Instructor.Text = person.Name;
				Phone.Text = person.Phone;
				Email.Text = person.Email;
				Performance.Text = course.Performance;
				Objective.Text = course.Objective;
				Term.Text = course.Term.ToString();
			}
            else
            {
				Term.Text = App.currentTerm.ToString();
            }
        }



		//return to previous page without saving
        async private void CancelButton(object sender, EventArgs e)
        {
			await Navigation.PopModalAsync();
        }

		//save, then return to previous page
		//currently can't edit existing records, just makes a new one
		async private void SaveButton(object sender, EventArgs e)
        {
			List<Person> people = App.Tables.GetAllPeople();
			List<Course> courses = App.Tables.GetAllCourses();
			var person = new Person
			{
				Name = Instructor.Text.ToString(),
				Email = Email.Text.ToString(),
				Phone = Phone.Text.ToString()
            };
			//check records for entry matching new one
			bool isFound = false;
			int highestId = 0;
			int matchingId = 0;
			int instructorId;
			foreach (Person p in people)
            {
				highestId++;
				if (person.IsEqual(p))
                {
					matchingId = p.PersonId;
					isFound = true;
					break;
				}
            }
			if (!isFound)
            {
				App.Tables.AddNewRecord(person);
				instructorId = highestId;
			}
			else { instructorId = matchingId; }

			var course = new Course
			{
				Name = Name.Text.ToString(),
				Start = Start.Date,
				End = End.Date,
				Status = statusPicker.SelectedItem.ToString(),
				Instructor = instructorId,
				Performance = Performance.Text.ToString(),
				Objective = Objective.Text.ToString(),
				Term = Int32.Parse(Term.Text.ToString())
			};
			//check records for entry matching new one
			isFound = false;
			foreach (Course c in courses)
			{
				if (course.IsEqual(c))
                {
					isFound = true;
					break;
                }
			}
			if (!isFound)
            {
				App.Tables.AddNewRecord(course);
            }
			await Navigation.PopModalAsync();
			await Navigation.PopAsync();
			CrossLocalNotifications.Current.Show("Reminder", "Remember to do the thing");
		}
    }
}