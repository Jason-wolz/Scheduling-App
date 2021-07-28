using System;
using WGUTermApp.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;
using Xamarin.Essentials;

namespace WGUTermApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        private int startNoteID;
        private int endNoteID;
        private bool toggle = true;
        private Assessment perf;
        private Assessment obj;
        private Course course = new Course();
        private List<Course> courses;
        public CoursePage()
        {
            InitializeComponent();
            courses = App.Tables.GetAllCourses();
            List<Person> people = App.Tables.GetAllPeople();
            List<Assessment> assessments = App.Tables.GetAllAssessments();
            Person person = new Person();
            for (int i = 0; i < courses.Count; i++)
            {
                if (courses[i].CourseId == App.currentClass)
                {
                    course = courses[i];
                    break;
                }
            }
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].PersonId == course.Instructor)
                {
                    person = people[i];
                }
            }
            int perfIndex = 0;
            int objIndex = 0;
            for (int i = 0; i < assessments.Count; i++)
            {
                if (assessments[i].AssessmentId == course.Performance)
                {
                    perfIndex = i;
                }
                if (assessments[i].AssessmentId == course.Objective)
                {
                    objIndex = i;
                }
            }
            if (course.Performance > 0)
            {
                perf = assessments[perfIndex];
                Performance.Text = perf.Name;
            }
            if (course.Objective > 0)
            {

                obj = assessments[objIndex];
                Objective.Text = obj.Name;
            }
            Title = course.Name;
            statusPicker.SelectedItem = course.Status;
            Start.Date = course.Start;
            End.Date = course.End;
            Instructor.Text = person.Name + ", " + person.Phone + " \n" + person.Email;
            MakeNewNote();
        }

        protected override void OnAppearing()
        {
            List<Assessment> assessments = App.Tables.GetAllAssessments();
            courses = App.Tables.GetAllCourses();
            for (int i = 0; i < courses.Count; i++)
            {
                if(courses[i].CourseId == App.currentClass)
                {
                    course = courses[i];
                    break;
                }
            }
            int perfIndex = -1;
            int objIndex = -1;
            for (int i = 0; i < assessments.Count; i++)
            {
                if (assessments[i].AssessmentId == course.Performance)
                {
                    perfIndex = i;
                }
                if (assessments[i].AssessmentId == course.Objective)
                {
                    objIndex = i;
                }
            }
            if (course.Performance > 0)
            {
                perf = assessments[perfIndex];
                Performance.Text = perf.Name;
            }
            else
            {
                Performance.Text = "Performance Assessment";
            }
            if (course.Objective > 0)
            {

                obj = assessments[objIndex];
                Objective.Text = obj.Name;
            }
            else
            {
                Objective.Text = "Objective Assessment";
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

        //make notifications for start and end of class, check for class currently going
        //to-do: !!optional!! reloading page resets yes/no button to default
        private void Note_Clicked(object sender, EventArgs e)
        {
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
                    CrossLocalNotifications.Current.Show("Class begins tomorrow", message, startNoteID, Start.Date.AddDays(-1));
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
            noteFrame.BackgroundColor = toggle ? Color.Goldenrod : Color.Gold;
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

        private async void Assessment_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.StyleId == "Objective")
            {
                if (obj != null)
                {
                    App.isNew = false;
                    await Navigation.PushModalAsync(new AssessmentPage(obj));
                }
                else
                {
                    App.isNew = true;
                    Assessment temp = new Assessment { Type = b.StyleId };
                    await Navigation.PushModalAsync(new AssessmentPage(temp));
                }
            }
            else if (b.StyleId == "Performance")
            {
                if (perf != null)
                {
                    App.isNew = false;
                    await Navigation.PushModalAsync(new AssessmentPage(perf));
                }
                else
                {
                    App.isNew = true;
                    Assessment temp = new Assessment { Type = b.StyleId };
                    await Navigation.PushModalAsync(new AssessmentPage(temp));
                }
            }
        }
    }
}