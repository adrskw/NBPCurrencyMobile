using System;
using System.Collections.Generic;
using System.Text;
using NBPCurrencyCore.Models;
using SQLite;

namespace NBPCurrencyMobile.Models
{
    public class TableExchangeRateEntity
    {
        [PrimaryKey]
        public string CurrencyCode { get; set; }

        [NotNull]
        public string CurrencyName { get; set; }

        [NotNull]
        public decimal Bid { get; set; }

        [NotNull]
        public decimal Ask { get; set; }

        public TableExchangeRateEntity()
        {
        }

        public TableExchangeRateEntity(TableExchangeRate tableExchangeRate)
        {
            CurrencyCode = tableExchangeRate.CurrencyCode;
            CurrencyName = tableExchangeRate.CurrencyName;
            Bid = tableExchangeRate.Bid;
            Ask = tableExchangeRate.Ask;
        }
    }
}