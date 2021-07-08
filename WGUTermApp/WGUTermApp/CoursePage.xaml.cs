using System;
using SQLite;
using WGUTermApp.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;
using Xamarin.Essentials;

namespace WGUTermApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        int startNoteID;
        int endNoteID;
        bool toggle = true;
        public CoursePage()
        {
            InitializeComponent();
            List<Course> courses = App.Tables.GetAllCourses();//need to add system for adding/deleting assessments
            List<Person> people = App.Tables.GetAllPeople();
            List<Assessment> assessments = App.Tables.GetAllAssessments();
            for (int i = 0; i < courses.Count; i++)
            {
                if (courses[i].CourseId == App.currentClass)
                {
                    Course course = courses.ElementAt(i);
                    Person person = people.ElementAt(course.Instructor - 1);
                    Assessment perf = assessments.ElementAt(course.Performance - 1);
                    Assessment obj = assessments.ElementAt(course.Objective - 1);
                    Title = course.Name;
                    statusPicker.SelectedItem = course.Status;
                    Start.Date = course.Start;
                    End.Date = course.End;
                    Instructor.Text = person.Name + ", " + person.Phone + " \n" + person.Email;
                    Objective.Text = obj.Name;
                    Performance.Text = perf.Name;
                }
            }
            MakeNewNote();            
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

        private void Note_Clicked(object sender, EventArgs e)
        {
            //make notifications, check for class currently going
            Button button = (Button)sender;
            if (button.BackgroundColor == Color.White)
            {
                string message;
                button.BackgroundColor = Color.Navy;
                button.TextColor = Color.White;
                button.Text = "yes";
                if (DateTime.Now < Start.Date)
                {
                    message = Title + " will begin tomorrow at 1pm.";
                    startNoteID = App.currentClass + 10;
                    //CrossLocalNotifications.Current.Show("Class begins tomorrow", message, startNoteID, Start.Date.AddDays(-1));
                }
                message = Title + " will be ending one week from today. Make sure your assessments are done!";
                endNoteID = App.currentClass + 100;
                CrossLocalNotifications.Current.Show("Class ends next week", message, endNoteID, End.Date.AddDays(-7));
            }
            else
            {
                button.BackgroundColor = Color.White;
                button.TextColor = Color.Black;
                button.Text = "no";
                startNoteID = App.currentClass + 10;
                endNoteID = App.currentClass + 100;
                CrossLocalNotifications.Current.Cancel(startNoteID);
                CrossLocalNotifications.Current.Cancel(endNoteID);
            }
            
        }

        private void MakeNewNote()
        {
            Label dateLabel = new Label
            {
                Text = "Date for reminder: ",
                VerticalOptions = LayoutOptions.Center
            };                        
            Label noteLabel = new Label
            {
                Text = "Reminder: ",
                VerticalOptions = LayoutOptions.Center
            };
            Editor noteText = new Editor
            {
                AutoSize = EditorAutoSizeOption.TextChanges,
                WidthRequest = 170
            };
            StackLayout textLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                        {
                            noteLabel,
                            noteText
                        }
            };
            Button newNoteButton = new Button
            {
                Text = "Remind Me",
                HorizontalOptions = LayoutOptions.Center,
            };
            newNoteButton.Clicked += New_Note_Clicked;
            StackLayout mainLayout = new StackLayout
            {
                Children =
                        {                            
                            textLayout,
                            newNoteButton
                        }
            };
            Frame noteFrame = new Frame
            {
                CornerRadius = 3,
                WidthRequest = 250,
                HorizontalOptions = LayoutOptions.Center,
                Content = mainLayout,
            };
            if (toggle)
            {
                noteFrame.BackgroundColor = Color.Goldenrod;
            }
            else
            {
                noteFrame.BackgroundColor = Color.Gold;
            }
            toggle = !toggle;
            noteLayout.Children.Add(noteFrame);
        }

        private async void New_Note_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            StackLayout mainStack = (StackLayout)b.Parent;
            StackLayout noteStack = (StackLayout)mainStack.Children[0];
            Editor note = (Editor)noteStack.Children[1];
            if (!string.IsNullOrEmpty(note.Text))
            {
                await Share.RequestAsync(new ShareTextRequest { Text = note.Text });
                b.IsEnabled = false;
                b.IsVisible = false;
                MakeNewNote();
            }
            else
            {
                note.Placeholder = "Please enter the reminder text.";
            }            
        }
    }
}