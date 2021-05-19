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
        public MainPage()
        {
            InitializeComponent();
            //sample data
            //DateTime now = DateTime.Now;
            //App.Tables.AddNewRecord(new Person("BobJim", "(333)-333-3333", "something@wgu.edu"));
            //App.Tables.AddNewRecord(new Course("Bacon 102", now, now.AddMonths(1), "In Progress", 1, "Test 1", "Test 2", 2));
            //var people = App.Tables.GetAllPeople();
            //Label1.Text = people.ElementAt(0).Name;
            //add all terms and courses in database
            var courses = App.Tables.GetAllCourses();
            int maxTerm = 0;
            foreach (Course c in courses)
            {
                if (maxTerm < c.Term)
                {
                    maxTerm = c.Term;
                }
            }

            int styleID = 0;
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
                Frame frame = new Frame
                {
                    Content = stackLayout,
                    BackgroundColor = Color.Goldenrod
                };
                Layout.Children.Add(frame);
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
                                StyleId = styleID.ToString()//assumes that classes are in order by term, not sure how to fix. maybe sort first?
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
                            Layout.Children.Add(classButton);
                            styleID++;
                        }
                    }
                }
            }
        }

        private void Button_Clicked1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            App.currentClass = Int32.Parse(b.StyleId);
            await Navigation.PushAsync(new CoursePage());
        }
    }
}
