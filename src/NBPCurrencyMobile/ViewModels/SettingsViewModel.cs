﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NBPCurrencyMobile.Models;
using NBPCurrencyMobile.Views.Controls;
using Xamarin.Forms;

namespace NBPCurrencyMobile.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private List<SettingsCurrencySwitchCellControl> currencyDisplaySwitches;

        public List<SettingsCurrencySwitchCellControl> CurrencyDisplaySwitches
        {
            get => currencyDisplaySwitches;
            set => SetProperty(ref currencyDisplaySwitches, value);
        }

        private DateTime defaultStartDate;

        public DateTime DefaultStartDate
        {
            get => defaultStartDate;
            set => SetProperty(ref defaultStartDate, value);
        }

        private DateTime defaultEndDate;

        public DateTime DefaultEndDate
        {
            get => defaultEndDate;
            set
            {
                if (MinimumDefaultStartDate > value) // fix crash with date picker maximum date being less than minimum date
                {
                    CalculateMinimumDefaultStartDate(value);
                }

                SetProperty(ref defaultEndDate, value);

                if (MinimumDefaultStartDate <= value)
                {
                    CalculateMinimumDefaultStartDate(value);
                }
            }
        }

        private DateTime minimumDefaultStartDate;

        public DateTime MinimumDefaultStartDate
        {
            get => minimumDefaultStartDate;
            set => SetProperty(ref minimumDefaultStartDate, value);
        }

        public DateTime MinimumDefaultDate { get; } = new DateTime(2002, 1, 2);

        public DateTime MaximumDefaultDate { get; } = DateTime.Now;

        public ICommand UpdateDefaultStartDateCommand => updateDefaultStartDateCommand ?? (updateDefaultStartDateCommand = new Command<DateTime>(UpdateDefaultStartDate));
        private ICommand updateDefaultStartDateCommand;

        public ICommand UpdateDefaultEndDateCommand => updateDefaultEndDateCommand ?? (updateDefaultEndDateCommand = new Command<DateTime>(UpdateDefaultEndDate));
        private ICommand updateDefaultEndDateCommand;

        public ICommand UpdateDisplayedCurrencySettingCommand => updateDisplayedCurrencySettingCommand ?? (updateDisplayedCurrencySettingCommand = new Command<SettingsCurrencySwitchCellControl>(UpdateDisplayedCurrencySetting));
        private ICommand updateDisplayedCurrencySettingCommand;

        public SettingsViewModel()
        {
            GetCurrentDefaultDates();
            GetCurrentDisplayedCurrenciesSettings();
            CalculateMinimumDefaultStartDate(DefaultEndDate);
        }

        public void GetCurrentDefaultDates()
        {
            SettingEntity defaultStartDate = App.Database.GetSetting("DefaultStartDate");
            SettingEntity defaultEndDate = App.Database.GetSetting("DefaultEndDate");

            DefaultStartDate = Convert.ToDateTime(defaultStartDate.Value);
            DefaultEndDate = Convert.ToDateTime(defaultEndDate.Value);
        }

        public void GetCurrentDisplayedCurrenciesSettings()
        {
            List<SettingDisplayedCurrencyEntity> currencyDisplaySettings = App.Database.GetDisplayedCurrenciesSettings();
            CurrencyDisplaySwitches = currencyDisplaySettings.Select(
                setting => new SettingsCurrencySwitchCellControl()
                {
                    CurrencyName = setting.CurrencyName,
                    CurrencyCode = setting.CurrencyCode,
                    IsToggled = setting.IsDisplayed
                }).ToList();
        }

        private void UpdateDefaultStartDate(DateTime date)
        {
            DefaultStartDate = date;
            SettingEntity setting = new SettingEntity("DefaultStartDate", date.ToString("s"));
            App.Database.UpdateSetting(setting);
        }

        private void UpdateDefaultEndDate(DateTime date)
        {
            DefaultEndDate = date;
            SettingEntity setting = new SettingEntity("DefaultEndDate", date.ToString("s"));
            App.Database.UpdateSetting(setting);
        }

        private void UpdateDisplayedCurrencySetting(SettingsCurrencySwitchCellControl item)
        {
            SettingDisplayedCurrencyEntity setting = new SettingDisplayedCurrencyEntity(item.CurrencyCode, item.CurrencyName, item.IsToggled);
            App.Database.UpdateDisplayedCurrencySetting(setting);
        }

        private void CalculateMinimumDefaultStartDate(DateTime endDate)
        {
            MinimumDefaultStartDate = endDate.AddDays(-App.NbpClient.MaximumNumberOfDaysForDownloadingData);
        }
    }
}