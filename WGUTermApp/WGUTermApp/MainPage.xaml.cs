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
            //DateTime now = DateTime.Now;
            //App.Tables.AddNewRecord(new Person("JimBob", "(333)-333-3333", "something@wgu.edu"));
            //App.Tables.AddNewRecord(new Course("Bacon 102", now, now.AddMonths(1), "In Progress", 1, "Test 1", "Test 2", 1));
            //List<Person> people = App.Tables.GetAllPeople();
            //Label1.Text = people.ElementAt(0).Name;
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoursePage());
        }
    }
}
