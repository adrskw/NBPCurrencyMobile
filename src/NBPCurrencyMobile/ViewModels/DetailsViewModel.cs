using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NBPCurrencyCore.Models;
using Xamarin.Forms;

namespace NBPCurrencyMobile.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        public string CurrencyCode { get; }

        private CurrencyInfo currencyInfo;

        public CurrencyInfo CurrencyInfo
        {
            get => currencyInfo;
            set
            {
                SetProperty(ref currencyInfo, value);

                UpdateProperties();
            }
        }

        private string biggestDifferenceOfBidPrices;

        public string BiggestDifferenceOfBidPrices
        {
            get => biggestDifferenceOfBidPrices;
            set => SetProperty(ref biggestDifferenceOfBidPrices, value);
        }

        private string biggestDifferenceOfAskPrices;

        public string BiggestDifferenceOfAskPrices
        {
            get => biggestDifferenceOfAskPrices;
            set => SetProperty(ref biggestDifferenceOfAskPrices, value);
        }

        public ICommand DownloadLatestDataCommand => downloadLatestDataCommand ?? (downloadLatestDataCommand = new Command(
                                                                async () => await DownloadLatestData(numberOfRecentData: 1)));

        private ICommand downloadLatestDataCommand;

        public ICommand DownloadDataFromTheLastDaysCommand
                            => downloadDataFromTheLastDaysCommand ?? (downloadDataFromTheLastDaysCommand = new Command<string>(
                                            async (parameter) =>
                                            {
                                                int numberOfDays = int.Parse(parameter);
                                                await DownloadDataForGivenPeriod(startDate: DateTime.Now.AddDays(-numberOfDays),
                                                                                    endDate: DateTime.Now);
                                            }));

        private ICommand downloadDataFromTheLastDaysCommand;

        public DetailsViewModel(string currencyCode)
        {
            CurrencyCode = currencyCode;
            DownloadLatestData();
        }

        private void UpdateProperties()
        {
            BiggestDifferenceOfBidPrices = GetStringOfDifferencesOfPrices(CurrencyInfo.BidSummary.BiggestDifference);
            BiggestDifferenceOfAskPrices = GetStringOfDifferencesOfPrices(CurrencyInfo.AskSummary.BiggestDifference);
        }

        private async Task DownloadLatestData(int numberOfRecentData = 1)
        {
            CurrencyInfo = await App.NbpClient.GetSeriesOfLatestExchangeRates(CurrencyCode, numberOfRecentData);
        }

        private async Task DownloadDataForGivenPeriod(DateTime startDate,
                                                        DateTime endDate)
        {
            CurrencyInfo = await App.NbpClient.GetSeriesOfExchangeRatesFromGivenPeriod(CurrencyCode, startDate, endDate);
        }

        private string GetStringOfDifferencesOfPrices(IEnumerable<ExchangeRate> rates)
        {
            string differenceOfPrices = "";

            foreach (var difference in rates)
            {
                differenceOfPrices += $"{difference.Date: dd/MM/yyyy}   {difference.Value: 0.0000;-0.0000}\n";
            }

            return differenceOfPrices.TrimEnd(Environment.NewLine.ToCharArray());
        }
    }
}