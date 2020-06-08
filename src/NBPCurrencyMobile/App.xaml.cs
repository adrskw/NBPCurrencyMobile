using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NBPCurrencyMobile.Views;
using System.Globalization;
using NBPCurrencyCore;

namespace NBPCurrencyMobile
{
    public partial class App : Application
    {
        public static NbpClient NbpClient { get; set; }

        public App()
        {
            InitializeComponent();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pl-PL");
            NbpClient = new NbpClient();

            MainPage = new TabsPage();
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