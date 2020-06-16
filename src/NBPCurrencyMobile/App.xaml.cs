using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NBPCurrencyMobile.Views;
using System.Globalization;
using NBPCurrencyCore;
using NBPCurrencyMobile.Models;

namespace NBPCurrencyMobile
{
    public partial class App : Application
    {
        public static NbpClient NbpClient { get; set; }
        public static DatabaseHelper Database { get; private set; }

        public App()
        {
            InitializeComponent();
            InitializeApp();
        }

        private void InitializeApp()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pl-PL");
            NbpClient = new NbpClient();
            InitializeDatabase();

            MainPage = new TabsPage();
        }

        private void InitializeDatabase()
        {
            if (Database == null)
            {
                Database = new DatabaseHelper();
                Database.Initialize();
            }
        }

        protected override void OnStart()
        {
            InitializeDatabase();
        }

        protected override void OnSleep()
        {
            Database.Dispose();
            Database = null;
        }

        protected override void OnResume()
        {
            InitializeDatabase();
        }
    }
}