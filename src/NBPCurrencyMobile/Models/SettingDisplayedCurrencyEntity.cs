using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace NBPCurrencyMobile.Models
{
    public class SettingDisplayedCurrencyEntity
    {
        [PrimaryKey]
        public string CurrencyCode { get; set; }

        [NotNull]
        public string CurrencyName { get; set; }

        [NotNull]
        public bool IsDisplayed { get; set; }

        public SettingDisplayedCurrencyEntity()
        {
        }

        public SettingDisplayedCurrencyEntity(string currencyCode, string currencyName, bool isDisplayed = true)
        {
            CurrencyCode = currencyCode;
            CurrencyName = currencyName;
            IsDisplayed = isDisplayed;
        }
    }
}