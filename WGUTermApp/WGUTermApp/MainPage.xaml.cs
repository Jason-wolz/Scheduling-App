using System;
using WGUTermApp.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace WGUTermApp
{
    public partial class MainPage : ContentPage
    {
        private bool isDeleting;
        private bool termDeleting;
        private List<Course> courses;
        private List<Term> terms;
        public MainPage()
        {
            InitializeComponent();
            //sample data
            DateTime now = DateTime.Now.Date;
            DateTime then = now.AddMonths(1);
            DateTime afterThat = now.AddMonths(6);
            App.Tables.AddNewRecord(new Term("Term 1", now, afterThat));
            App.Tables.AddNewRecord(new Assessment("Test 1", "Performance", "You will make a thing", then));
            App.Tables.AddNewRecord(new Assessment("Test 2", "Objective", "You will do a thing", then));
            App.Tables.AddNewRecord(new Person("Jason Wolz", "(307)-752-9762", "jwolz1@my.wgu.edu"));
            App.Tables.AddNewRecord(new Course("Bacon 101", now, then, "In Progress", 1, 1, 2, 1));


            //add all terms and courses in database to main page when 
        }
        protected override void OnAppearing()
        {
            BuildUI();
            isDeleting = false;
        }

        //Build the ui, check for no terms to display
        public void BuildUI()
        {
            DeleteUI();
            courses = App.Tables.GetAllCourses();
            terms = App.Tables.GetAllTerms();
            if (terms.Count > 0)
            {
                int maxTerm = terms[terms.Count - 1].TermId;
                for (int i = 0; i < terms.Count; i++)
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
                                Text = terms[i].Name + ": " + terms[i].Start.ToString("yyyy-MM-dd") + " - " + terms[i].End.ToString("yyyy-MM-dd"),
                                HorizontalOptions = LayoutOptions.StartAndExpand,
                                VerticalOptions = LayoutOptions.Center
                            },
                            button
                        }
                    };
                    StackLayout otherLayout = new StackLayout
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
                        StyleId = terms[i].TermId.ToString(),
                        Children = { frame }
                    };
                    Layout.Children.Add(termLayout);
                    if (courses.Count > 0)
                    {
                        bool toggle = false;
                        //add classes
                        foreach (Course c in courses)
                        {
                            if (c.Term == terms[i].TermId)
                            {
                                toggle = !toggle;
                                Button classButton = new Button
                                {
                                    Text = c.Name,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    StyleId = c.CourseId.ToString()
                                };
                                classButton.Clicked += Button_Clicked;
                                classButton.BackgroundColor = toggle ? Color.CornflowerBlue : Color.DodgerBlue;
                                Button check = new Button
                                {
                                    BackgroundColor = Color.White,
                                    CornerRadius = 4,
                                    BorderWidth = 4,
                                    BorderColor = Color.White,
                                    WidthRequest = 35,
                                    IsEnabled = false
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
                            }
                        }
                    }
                }
            }
            
            //button for adding new term
            Button button1 = new Button
            {
                Text = "Add a new term?",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            button1.Clicked += New_Term_Clicked;
            StackLayout nameLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label { Text = "Name: " },
                    new Editor { WidthRequest = 160 }
                }
            };
            StackLayout startLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label { Text = "Start time: " },
                    new DatePicker
                    {
                        Format = "yyyy-MM-dd",
                        Date = DateTime.Now
                    }
                }
            };
            StackLayout endLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label { Text = "End time: " },
                    new DatePicker
                    {
                        Format = "yyyy-MM-dd",
                        Date = DateTime.Now.AddMonths(6)
                    }
                }
            };
            StackLayout buttonLayout = new StackLayout
            {
                Children =
                {
                    nameLayout,
                    startLayout,
                    endLayout,
                    button1
                }
            };
            StackLayout frameLayout = new StackLayout
            {
                Children = { buttonLayout }
            };
            Frame newTermFrame = new Frame
            {
                Content = frameLayout,
                BackgroundColor = Color.Goldenrod
            };
            StackLayout newTermLayout = new StackLayout
            {
                Children = { newTermFrame }
            };
            Layout.Children.Add(newTermLayout);
        }


        //clear interface for updating
        private void DeleteUI()
        {
            while (Layout.Children.Count > 0)
            {
                Layout.Children.RemoveAt(0);
            }
        }

        private void New_Term_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            StackLayout stack = (StackLayout)b.Parent;
            StackLayout startLayout = (StackLayout)stack.Children[1];
            DatePicker start = (DatePicker)startLayout.Children[1];
            StackLayout endLayout = (StackLayout)stack.Children[2];
            DatePicker end = (DatePicker)endLayout.Children[1];
            if (start.Date < end.Date)
            {
                StackLayout nameLayout = (StackLayout)stack.Children[0];
                Editor name = (Editor)nameLayout.Children[1];
                Term term = new Term
                {
                    Name = name.Text,
                    Start = start.Date,
                    End = end.Date
                };
                App.Tables.AddNewRecord(term);
            }
            BuildUI();
        }

        private void Check_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            b.BackgroundColor = b.BackgroundColor == Color.White ? Color.Navy : Color.White;
        }

        //add the add and delete course buttons, as well as the term view/delete buttons
        private void Button_Clicked1(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            StackLayout layout = (StackLayout)b.Parent.Parent;
            if (layout.Children.Count() < 4)
            {
                Button add = new Button
                {
                    Text = "Add a new course",
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                add.Clicked += Add_Course_Clicked;
                Button delete = new Button
                {
                    Text = "Delete courses from term",
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                delete.Clicked += Delete_Clicked;
                Button termView = new Button
                {
                    Text = "View term information",
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                termView.Clicked += View_Term_Clicked;
                Button termDelete = new Button
                {
                    Text = "Delete this term",
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                termDelete.Clicked += Delete_Term_Clicked;
                StackLayout row1 = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        delete,
                        add
                    }
                };
                StackLayout row2 = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        termView,
                        termDelete
                    }
                };
                termDeleting = false;
                layout.Children.Add(row1);
                layout.Children.Add(row2);
            }
        }

        private void Delete_Term_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            termDeleting = !termDeleting;
            if (termDeleting)
            {
                b.Text = "Are you sure you want to delete this term?";
            }
            else
            {
                StackLayout s = (StackLayout)b.Parent.Parent.Parent.Parent;
                courses = App.Tables.GetAllCourses();
                for (int i = 0; i < courses.Count; i++)
                {
                    if (courses[i].Term == int.Parse(s.StyleId))
                    {
                        App.Tables.DeleteRecord("Course", courses[i].CourseId);
                    }
                }
                App.Tables.DeleteRecord("Term", int.Parse(s.StyleId));
                BuildUI();
            }
        }

        //prep for ui for deleting or delete selected courses
        private void Delete_Clicked(object sender, EventArgs e)
        {
            isDeleting = !isDeleting;
            Button b = (Button)sender;
            StackLayout layout = (StackLayout)b.Parent.Parent.Parent.Parent;
            if (isDeleting)
            {
                b.Text = "Delete selected courses?";
                for (int i = 1; i < layout.Children.Count; i++)
                {
                    StackLayout stack = (StackLayout)layout.Children[i];
                    Button button = (Button)stack.Children[0];
                    button.BorderColor = Color.Black;
                    button.IsEnabled = true;
                }
            }
            else
            {
                for (int i = 1; i < layout.Children.Count; i++)
                {
                    StackLayout stack = (StackLayout)layout.Children[i];
                    Button button = (Button)stack.Children[0];
                    if (button.BackgroundColor == Color.Navy)
                    {
                        Button deleted = (Button)stack.Children[1];
                        App.Tables.DeleteRecord("Course", int.Parse(deleted.StyleId));
                        BuildUI();
                    }
                    button.BorderColor = Color.White;
                }
                b.Text = "Delete courses from term";
            }
        }

        private async void Add_Course_Clicked(object sender, EventArgs e)
        {
            App.isNew = true;
            Button b = (Button)sender;
            StackLayout s = (StackLayout)b.Parent.Parent.Parent.Parent;
            App.currentTerm = int.Parse(s.StyleId);
            await Navigation.PushModalAsync(new CourseEdit());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            App.currentClass = int.Parse(b.StyleId);
            await Navigation.PushAsync(new CoursePage());
        }

        private async void View_Term_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            StackLayout s = (StackLayout)b.Parent.Parent;
            //App.currentTerm = s.
            await Navigation.PushModalAsync(new TermPage());
        }
    }
}
