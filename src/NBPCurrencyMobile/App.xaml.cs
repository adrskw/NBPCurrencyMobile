using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NBPCurrencyMobile.Views;
using System.Globalization;

namespace NBPCurrencyMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pl-PL");

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