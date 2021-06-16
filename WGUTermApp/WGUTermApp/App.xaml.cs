using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WGUTermApp
{
    public partial class App : Application
    {
        static readonly string databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "CourseDatabase.db3");
        public static TableMethods Tables { get; private set; }
        public static int currentClass;
        public static bool isNew;
        public static int currentTerm;
        public App()
        {
            Tables = new TableMethods(databasePath);

            if (Properties.ContainsKey("name"))
            {
                Name = (string)Properties["name"];
            }
            if (Properties.ContainsKey("startDate"))
            {
                StartDate = (string)Properties["startDate"];
            }
            if (Properties.ContainsKey("endDate"))
            {
                EndDate = (string)Properties["endDate"];
            }
            if (Properties.ContainsKey("status"))
            {
                Status = (string)Properties["status"];
            }
            if (Properties.ContainsKey("instructor"))
            {
                Instructor = (string)Properties["instructor"];
            }
            if (Properties.ContainsKey("phone"))
            {
                Phone = (string)Properties["phone"];
            }
            if (Properties.ContainsKey("email"))
            {
                Email = (string)Properties["email"];
            }
            if (Properties.ContainsKey("performanceAssessment"))
            {
                PerformanceAssessment = (string)Properties["performanceAssessment"];
            }
            if (Properties.ContainsKey("objectiveAssessment"))
            {
                ObjectiveAssessment = (string)Properties["objectiveAssessment"];
            }
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Gainsboro,
                BarTextColor = Color.Black
            };
        }

        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string Instructor { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PerformanceAssessment { get; set; }
        public string ObjectiveAssessment { get; set; }
        protected override void OnStart()
        {
            // Handle when your app starts
        }
// Handle when your app sleeps
        protected override void OnSleep()
        {
            Properties["name"] = Name;
            Properties["startDate"] = StartDate;
            Properties["endDate"] = EndDate;
            Properties["status"] = Status;
            Properties["instructor"] = Instructor;
            Properties["phone"] = Phone;
            Properties["email"] = Email;
            Properties["performanceAssessment"] = PerformanceAssessment;
            Properties["objectiveAssessment"] = ObjectiveAssessment;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
