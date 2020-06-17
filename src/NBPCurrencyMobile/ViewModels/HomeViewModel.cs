using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using NBPCurrencyCore;
using NBPCurrencyCore.Models;
using NBPCurrencyMobile.Models;
using NBPCurrencyMobile.Views;
using Xamarin.Forms;

namespace NBPCurrencyMobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private List<TableExchangeRate> exchangeRates;

        public List<TableExchangeRate> ExchangeRates
        {
            get => exchangeRates;
            set
            {
                SetProperty(ref exchangeRates, value);
            }
        }

        public ICommand GoToDetailsCommand => goToDetailsCommand ?? (goToDetailsCommand = new Command<TableExchangeRate>(GoToDetails));
        private ICommand goToDetailsCommand;
        private readonly INavigation navigation;

        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            UpdateCurrentExchangeRates();
        }

        public async void UpdateCurrentExchangeRates()
        {
            ExchangeRatesTable currentTable = await App.NbpClient.GetCurrentTable();
            List<SettingDisplayedCurrencyEntity> displayedCurrencySettings = App.Database.GetDisplayedCurrenciesSettings();
            ExchangeRates = currentTable.ExchangeRates.Where(rate => displayedCurrencySettings.Any(setting => rate.CurrencyCode == setting.CurrencyCode && setting.IsDisplayed)).ToList();
        }

        private void GoToDetails(TableExchangeRate exchangeRate)
        {
            navigation.PushAsync(new DetailsPage(exchangeRate));
        }
    }
}