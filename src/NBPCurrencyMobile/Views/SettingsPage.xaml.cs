using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBPCurrencyCore.Models;
using NBPCurrencyMobile.Models;
using NBPCurrencyMobile.ViewModels;
using NBPCurrencyMobile.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBPCurrencyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsViewModel SettingsViewModel { get; }

        public SettingsPage()
        {
            InitializeComponent();

            SettingsViewModel = new SettingsViewModel();
            BindingContext = SettingsViewModel;
        }

        private void StartDateSetting_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            SettingsViewModel.UpdateDefaultStartDateCommand.Execute(e.NewDate);
        }

        private void EndDateSetting_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            SettingsViewModel.UpdateDefaultEndDateCommand.Execute(e.NewDate);
        }

        private void DisplayedCurrenciesSettings_SwitchToggled(object sender, ToggledEventArgs e)
        {
            SettingsCurrencySwitchCellControl item = sender as SettingsCurrencySwitchCellControl;

            SettingsViewModel.UpdateDisplayedCurrencySettingCommand.Execute(item);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (SettingsViewModel.CurrencyDisplaySwitches.Count == 0)
            {
                SettingsViewModel.GetCurrentDisplayedCurrenciesSettings();
            }
        }
    }
}