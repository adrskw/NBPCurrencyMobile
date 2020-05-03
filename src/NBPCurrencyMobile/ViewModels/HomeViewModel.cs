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
        public ICommand GoToDetailsCommand => goToDetailsCommand ?? (goToDetailsCommand = new Command(GoToDetails));
        private ICommand goToDetailsCommand;
        private readonly INavigation navigation;
        private readonly NbpClient nbpClient;

        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            nbpClient = new NbpClient();
            UpdateCurrentExchangeRates();
        }

        private async void UpdateCurrentExchangeRates()
        {
            ExchangeRatesTable currentTable = await nbpClient.GetCurrentTable();
            ExchangeRates = currentTable.ExchangeRates;
            RaisePropertyChanged(nameof(ExchangeRates));
        }

        private void GoToDetails()
        {
            navigation.PushAsync(new DetailsPage());
        }
    }
}