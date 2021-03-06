﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private bool isRefreshing = false;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                SetProperty(ref isRefreshing, value);
            }
        }

        public ICommand RefreshCommand
                            => refreshCommand ?? (refreshCommand = new Command(
                                async () =>
                                {
                                    IsRefreshing = true;

                                    await UpdateCurrentExchangeRates(isUpdateForced: true);

                                    IsRefreshing = false;
                                }));

        private ICommand refreshCommand;

        public ICommand GoToDetailsCommand => goToDetailsCommand ?? (goToDetailsCommand = new Command<TableExchangeRate>(GoToDetails));
        private ICommand goToDetailsCommand;
        private readonly INavigation navigation;

        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            UpdateCurrentExchangeRates();
        }

        public async Task UpdateCurrentExchangeRates(bool isUpdateForced = false)
        {
            List<TableExchangeRate> exchangeRatesData;

            if (IsExchangeRateDatabaseDataUpToDate() && !isUpdateForced)
            {
                exchangeRatesData = App.Database.GetTableExchangeRates();
            }
            else
            {
                ExchangeRatesTable currentTable = await App.NbpClient.GetCurrentTable();
                exchangeRatesData = currentTable.ExchangeRates;

                DateTime dataValidUntil = TimeZoneInfo.ConvertTimeToUtc(
                                                        currentTable.EffectiveDate.Date.AddDays(1) + App.NbpClient.TableCUpdateTime,
                                                        App.NbpClient.ApiTimeZone);
                App.Database.UpdateSetting(new SettingEntity("DatabaseDataValidUntil", dataValidUntil.ToString("s")));
                App.Database.SaveTableExchangeRates(exchangeRatesData);
            }

            List<SettingDisplayedCurrencyEntity> displayedCurrencySettings = App.Database.GetDisplayedCurrenciesSettings();
            ExchangeRates = exchangeRatesData.Where(
                rate => displayedCurrencySettings.Any(
                    setting => rate.CurrencyCode == setting.CurrencyCode && setting.IsDisplayed)).ToList();
        }

        private void GoToDetails(TableExchangeRate exchangeRate)
        {
            navigation.PushAsync(new DetailsPage(exchangeRate));
        }

        private bool IsExchangeRateDatabaseDataUpToDate()
        {
            SettingEntity setting = App.Database.GetSetting("DatabaseDataValidUntil");
            DateTime databaseDataValidUntil = Convert.ToDateTime(setting.Value);

            if (App.Database.GetTableExchangeRates().Count == 0)
            {
                return false;
            }

            return databaseDataValidUntil >= DateTime.UtcNow;
        }
    }
}