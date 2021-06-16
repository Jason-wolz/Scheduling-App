using System;
using WGUTermApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WGUTermApp
{
    public partial class MainPage : ContentPage
    {
        bool isDeleting = false;
        List<Course> courses;
        List<Person> people;
        public MainPage()
        {
            InitializeComponent();
            //delete all records from database
            courses = App.Tables.GetAllCourses();
            while (courses.Count > 0)
            {
                App.Tables.DeleteRecord("Course", courses[0].CourseId);
                courses = App.Tables.GetAllCourses();
            }
            //people = App.Tables.GetAllPeople();
            //while (people.Count > 0)
            //{
            //    App.Tables.DeleteRecord("Person", people[0].PersonId);
            //    people = App.Tables.GetAllPeople();
            //}

            //sample data
            DateTime now = DateTime.Now.Date;
            App.Tables.AddNewRecord(new Person("BobJim", "(333)-333-3333", "something@wgu.edu"));
            App.Tables.AddNewRecord(new Course("Bacon 101", now, now.AddMonths(1), "In Progress", 1, "Test 1", "Test 2", 1));
            App.Tables.AddNewRecord(new Course("Bacon 102", now, now.AddMonths(1), "In Progress", 1, "Test 1", "Test 2", 2));
            //people = App.Tables.GetAllPeople();


            //add all terms and courses in database to main page
        }
        protected override void OnAppearing()
        {
            BuildUI();
        }
        public void BuildUI()
        {
            DeleteUI();
            courses = App.Tables.GetAllCourses();
            int maxTerm = 0;
            foreach (Course c in courses)
            {
                if (maxTerm < c.Term)
                {
                    maxTerm = c.Term;
                }
            }

            int styleID = 1;
            for (int term = 1; term <= maxTerm; term++)
            {
                //add term
                Button button = new Button
                {
                    Text = "\u205D",
                    WidthRequest = 30,
                    HeightRequest = 35,
                    BackgroundColor = Color.Goldenrod,
                };
                button.Clicked += Button_Clicked1;
                StackLayout stackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new Label
                        {
                            Text = "Term " + term,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            VerticalOptions = LayoutOptions.Center
                        },
                        button
                    }
                };
                var otherLayout = new StackLayout
                {
                    Children = { stackLayout }
                };
                Frame frame = new Frame
                {
                    Content = otherLayout,
                    BackgroundColor = Color.Goldenrod
                };
                StackLayout termLayout = new StackLayout
                {
                    StyleId = term.ToString(),
                    Children = { frame }
                };
                Layout.Children.Add(termLayout);
                if (courses.Count > 0)
                {
                    bool toggle = false;
                    //add classes
                    foreach (Course c in courses)
                    {
                        if (c.Term == term)
                        {
                            toggle = !toggle;
                            Button classButton = new Button
                            {
                                Text = c.Name,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                StyleId = c.CourseId.ToString()
                            };
                            classButton.Clicked += Button_Clicked;
                            if (toggle)
                            {
                                classButton.BackgroundColor = Color.CornflowerBlue;
                            }
                            else
                            {
                                classButton.BackgroundColor = Color.DodgerBlue;
                            }
                            Button check = new Button
                            {
                                BackgroundColor = Color.White,
                                BorderWidth = 4,
                                BorderColor = Color.White,
                                WidthRequest = 35
                            };
                            check.Clicked += Check_Clicked;
                            StackLayout classLayout = new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {
                                    check,
                                    classButton
                                }
                            };
                            termLayout.Children.Add(classLayout);
                            styleID++;
                        }
                    }
                }
            }
        }

        private void DeleteUI()
        {
            while (Layout.Children.Count > 0)
            {
                Layout.Children.RemoveAt(0);
            }
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            b.BackgroundColor = Color.Black;
        }

        private void Button_Clicked1(object sender, EventArgs e)//change border color of check boxes
        {
            var b = (Button)sender;
            var layout = (StackLayout)b.Parent.Parent;
            
            var add = new Button
            {                
                Text = "Add a new course"
            };
            add.Clicked += AddCoursePage;
            var delete = new Button
            {
                Text = "Delete courses from term"
            };
            delete.Clicked += Delete_Clicked;
            if (layout.Children.Count() < 3)
            {
                layout.Children.Add(add);
                layout.Children.Add(delete);
            }
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            isDeleting = !isDeleting;
            var b = (Button)sender;
            var layout = (StackLayout)b.Parent.Parent.Parent;                            
            if (isDeleting)
            {
                b.Text = "Delete selected courses?";
                for (int i = 1; i < layout.Children.Count; i++)
                {
                    var stack = (StackLayout)layout.Children[i];
                    var button = (Button)stack.Children[0];
                    button.BorderColor = Color.Black;
                }
            }            
            else
            {
                for (int i = 1; i < layout.Children.Count; i++)
                {
                    var stack = (StackLayout)layout.Children[i];
                    var button = (Button)stack.Children[0];
                    if (button.BackgroundColor == Color.Black)
                    {
                        var deleted = (Button)stack.Children[1];
                        App.Tables.DeleteRecord("Course", int.Parse(deleted.StyleId));
                        BuildUI();
                    }
                }
            }
        }

        async private void AddCoursePage(object sender, EventArgs e)
        {
            App.isNew = true;
            var b = (Button)sender;
            var s = (StackLayout)b.Parent.Parent.Parent;
            App.currentTerm = int.Parse(s.StyleId);
            await Navigation.PushModalAsync(new CourseEdit());
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            App.currentClass = int.Parse(b.StyleId);
            await Navigation.PushAsync(new CoursePage());
        }
    }
}
