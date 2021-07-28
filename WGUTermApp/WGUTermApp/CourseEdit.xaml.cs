using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGUTermApp.Models;

namespace WGUTermApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseEdit : ContentPage
    {
        private List<Course> courses = App.Tables.GetAllCourses();
        private Course course;
        private List<Person> people = App.Tables.GetAllPeople();
        private Person person;
        public CourseEdit()
        {
            InitializeComponent();
            if (!App.isNew)
            {

                //List<Assessment> assessments = App.Tables.GetAllAssessments();
                for (int i = 0; i < courses.Count; i++)
                {
                    if (courses[i].CourseId == App.currentClass)
                    {
                        course = courses[i];
                        person = people[course.Instructor - 1];
                    }
                }
                ID.Text = course.CourseId.ToString();
                Name.Text = course.Name;
                Start.Date = course.Start;
                End.Date = course.End;
                statusPicker.SelectedItem = course.Status;
                Instructor.Text = person.Name;
                Phone.Text = person.Phone;
                Email.Text = person.Email;
                Term.Text = course.Term.ToString();
            }
            else
            {
                Term.Text = App.currentTerm.ToString();
            }
        }



        //return to previous page without saving
        private async void CancelButton(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        //save, then return to previous page
        private async void SaveButton(object sender, EventArgs e)
        {
            if (Start.Date < End.Date)
            {
                people = App.Tables.GetAllPeople();
                courses = App.Tables.GetAllCourses();
                int persId = 0;
                if (!App.isNew)
                {
                    persId = person.PersonId;
                }
                course = new Course
                {
                    Name = Name.Text,
                    Start = Start.Date,
                    End = End.Date,
                    Status = statusPicker.SelectedItem.ToString(),
                    Term = int.Parse(Term.Text)
                };
                person = new Person
                {
                    Name = Instructor.Text,
                    Email = Email.Text,
                    Phone = Phone.Text
                };
                if (App.isNew)//to-do: !!optional!! should this check for new class with existing instructor? don't know, work on it after you submit
                {
                    if (!person.IsNullOrEmpty())
                    {
                        App.Tables.AddNewRecord(person);
                        people = App.Tables.GetAllPeople();
                        person = people[people.Count - 1];
                        course.Instructor = person.PersonId;
                        if (!course.IsNullOrEmpty())
                        {
                            App.Tables.AddNewRecord(course);
                            await Navigation.PopModalAsync();
                            await Navigation.PopAsync();
                        }
                        else { Note.Text = "Please fill out all fields."; }
                    }
                    else { Note.Text = "Please fill out all fields."; }
                }
                else
                {
                    if (!person.IsNullOrEmpty())
                    {
                        person.PersonId = persId;
                        course.CourseId = int.Parse(ID.Text);
                        course.Instructor = persId;
                        App.Tables.UpdateRecord(person);
                        if (!course.IsNullOrEmpty())
                        {
                            App.Tables.UpdateRecord(course);
                            await Navigation.PopModalAsync();
                            await Navigation.PopAsync();
                        }
                        else { Note.Text = "Please fill out all fields."; }
                    }
                    else { Note.Text = "Please fill out all fields."; }
                }
            }
            else
            {
                Note.Text = "Please enter valid dates.";
            }
        }
    }
}