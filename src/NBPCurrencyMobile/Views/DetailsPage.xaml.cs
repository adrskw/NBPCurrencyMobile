using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBPCurrencyCore.Models;
using NBPCurrencyMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBPCurrencyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsViewModel DetailsViewModel { get; }

        public DetailsPage(TableExchangeRate exchangeRate)
        {
            InitializeComponent();
            Title = $"{exchangeRate.CurrencyName} ({exchangeRate.CurrencyCode})";

            DetailsViewModel = new DetailsViewModel(exchangeRate.CurrencyCode);
        }
    }
}