using System;
using SQLite;
using WGUTermApp.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUTermApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public CoursePage()
        {
            InitializeComponent();
            var courses = App.Tables.GetAllCourses();
            Course course;
            var people = App.Tables.GetAllPeople();
            Person person;
            for (int i = 0; i < courses.Count; i++)
            {
                if (courses[i].CourseId == App.currentClass)
                {
                    course = courses.ElementAt(i);
                    person = people.ElementAt(course.Instructor - 1);
                    this.Title = course.Name;
                    statusPicker.SelectedItem = course.Status;
                    Start.Date = course.Start;
                    End.Date = course.End;
                    Instructor.Text = person.Name + ", " + person.Phone + ", " + person.Email;
                    Objective.Text = course.Objective;
                    Performance.Text = course.Performance;
                }
            }
            
            
        }

        private async void CancelButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void EditButton(object sender, EventArgs e)
        {
            App.isNew = false;
            await Navigation.PushModalAsync(new CourseEdit());            
        }
    }
}