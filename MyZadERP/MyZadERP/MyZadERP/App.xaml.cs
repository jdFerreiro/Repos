using MyZadERP.Interfaces;
using MyZadERP.Models.Facade;
using MyZadERP.Views.LoginScreen;
using MyZadERP.Views;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace MyZadERP
{
    public partial class App : Application
    {
        //public static string MyZadBackEndURL = "http://h2533715.stratoserver.net:8080"; //AMBIENTE: PRO
        //public static string MyZadBackEndURL = "http://h2533715.stratoserver.net:8090"; //AMBIENTE: PRE
        public static string MyZadBackEndURL = "https://10.0.2.2:5001"; //AMBIENTE: LOCAL
        //public static string MyZadBackEndURL = "https://192.168.1.125:5000"; //AMBIENTE: LOCAL
        public static string DataBasePath;
        public static double ScreenWidth;
        public static double ScreenHeight;
        public static ViewModels.DTO.UserDTO UserInfo;
        public static SQLiteConnection Connection;
        static MyZapDataBase database;


        public App(string dataBasePath, double screenHeight, double screenWidth)
        {
            DataBasePath = dataBasePath;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            InitializeComponent();
            var userManager = new AccountManager();
            App.UserInfo = userManager.GetUserInfo();
            Application.Current.MainPage = App.UserInfo == null ? (Page)new LoginScreen() : new MainPage();
        }

        public static MyZapDataBase Database =>
            database ?? (database = new MyZapDataBase(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyZadERP.db3")));

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
