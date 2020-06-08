using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NBPCurrencyCore;
using NBPCurrencyCore.Models;
using NBPCurrencyMobile.Views;
using Xamarin.Forms;

namespace NBPCurrencyMobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public List<TableExchangeRate> ExchangeRates { get; set; }
        public ICommand GoToDetailsCommand => goToDetailsCommand ?? (goToDetailsCommand = new Command<TableExchangeRate>(GoToDetails));
        private ICommand goToDetailsCommand;
        private readonly INavigation navigation;

        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            UpdateCurrentExchangeRates();
        }

        private async void UpdateCurrentExchangeRates()
        {
            ExchangeRatesTable currentTable = await App.NbpClient.GetCurrentTable();
            ExchangeRates = currentTable.ExchangeRates;
            RaisePropertyChanged(nameof(ExchangeRates));
        }

        private void GoToDetails(TableExchangeRate exchangeRate)
        {
            navigation.PushAsync(new DetailsPage(exchangeRate));
        }
    }
}