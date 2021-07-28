using System;
using System.Collections.Generic;
using WGUTermApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WGUTermApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentPage : ContentPage
    {
        private readonly Course course;
        public AssessmentPage(Assessment a)
        {
            InitializeComponent();
            if (App.isNew)
            {
                List<Assessment> assessments = App.Tables.GetAllAssessments();
                ID.Text = assessments[assessments.Count - 1].AssessmentId.ToString();
                Type.Text = a.Type;
                Title = a.Type + " Assessment";
                Delete.IsEnabled = false;
            }
            else
            {
                ID.Text = a.AssessmentId.ToString();
                Title = a.Name;
                Name.Text = a.Name;
                Type.Text = a.Type;
                Desc.Text = a.Description;
                End.Date = a.EstDueDate;
            }
            List<Course> courses = App.Tables.GetAllCourses();
            for (int i = 0; i < courses.Count; i++)
            {
                if (courses[i].CourseId == App.currentClass)
                {
                    course = courses[i];
                    break;
                }
            }

        }

        //return to previous page without saving
        private async void Cancel_Button(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        //save changes, then return to previous page
        private async void Save_Button(object sender, EventArgs e)
        {
            Assessment assessment = new Assessment
            {
                AssessmentId = int.Parse(ID.Text),
                Name = Name.Text,
                Type = Type.Text,
                Description = Desc.Text,
                EstDueDate = End.Date
            };
            if (App.isNew)
            {
                App.Tables.AddNewRecord(assessment);
            }
            else
            {
                App.Tables.UpdateRecord(assessment);
            }
            if (Type.Text == "Performance")
            {
                course.Performance = int.Parse(ID.Text);
            }
            else if (Type.Text == "Objective")
            {
                course.Objective = int.Parse(ID.Text);
            }
            App.Tables.UpdateRecord(course);
            await Navigation.PopModalAsync();
        }

        private async void Delete_Button(object sender, EventArgs e)
        {
            App.Tables.DeleteRecord("Assessment", int.Parse(ID.Text));
            if (Type.Text == "Performance")
            {
                course.Performance = -1;
            }
            else if (Type.Text == "Objective")
            {
                course.Objective = -1;
            }
            App.Tables.UpdateRecord(course);
            await Navigation.PopModalAsync();
        }
    }
}