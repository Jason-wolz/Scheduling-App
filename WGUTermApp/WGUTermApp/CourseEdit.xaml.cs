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

        public CourseEdit()
        {
            InitializeComponent();//add term field
            if (!App.isNew)
            {
                List<Course> courses = App.Tables.GetAllCourses();
                List<Person> people = App.Tables.GetAllPeople();
                //List<Assessment> assessments = App.Tables.GetAllAssessments();
                for (int i = 0; i < courses.Count; i++)
                {
                    if (courses[i].CourseId == App.currentClass)
                    {
                        var course = courses.ElementAt(i);
                        var person = people.ElementAt(course.Instructor - 1);
                        //var perf = assessments.ElementAt(course.Performance - 1);
                        //var obj = assessments.ElementAt(course.Objective - 1);
                        Name.Text = course.Name;
                        Start.Date = course.Start;
                        End.Date = course.End;
                        statusPicker.SelectedItem = course.Status;
                        Instructor.Text = person.Name;
                        Phone.Text = person.Phone;
                        Email.Text = person.Email;
                        //Performance.Text = perf.Name;
                        //Objective.Text = obj.Name;
                        Term.Text = course.Term.ToString();
                    }
                }
            }
            else
            {
                Term.Text = App.currentTerm.ToString();
                Term.IsEnabled = true;
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

            Course course = new Course
            {
                Name = Name.Text.ToString(),
                Start = Start.Date,
                End = End.Date,
                Status = statusPicker.SelectedItem.ToString(),
                Instructor = instructorId,
                Term = int.Parse(Term.Text.ToString())
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
        }
    }
}