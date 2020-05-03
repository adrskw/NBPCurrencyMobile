using System;
using System.Collections.Generic;
using System.Text;
using NBPCurrencyCore.Models;

namespace NBPCurrencyMobile.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        public string CurrencyCode { get; }

        public DetailsViewModel(string currencyCode)
        {
            CurrencyCode = currencyCode;
        }
    }
}