using System;
using System.IO;
using TravelPlannerAPP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TravelPlannerAPP
{
    public partial class App : Application
    {
        public static Database DB { get; set; }
        public static int CurrentUserId { get; set; }
        public static TripDatabase tripDatabase { get; set; }
        public App()
        {
            InitializeComponent();

            string userDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");

            DB = new Database(userDbPath);

            string tripDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tripdb.sqlite");
            tripDatabase = new TripDatabase(tripDbPath);

            
            MainPage = new NavigationPage(new MainPage());
            
                
        }

        protected override void OnStart()
        {
           
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
