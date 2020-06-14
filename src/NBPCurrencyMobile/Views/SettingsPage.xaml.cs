using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBPCurrencyCore.Models;
using NBPCurrencyMobile.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBPCurrencyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void StartDateSetting_OnDateSelected(object sender, DateChangedEventArgs e)
        {
        }

        private void EndDateSetting_OnDateSelected(object sender, DateChangedEventArgs e)
        {
        }
    }
}