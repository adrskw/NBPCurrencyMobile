using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBPCurrencyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            // Example listview data
            CurrencyListView.ItemsSource = new List<object>
            {
                new { CurrencyName = "dolar amerykański", CurrencyCode = "USD", CurrencyAverageExchangeRate = 4.22 },
                new { CurrencyName = "dolar australijski", CurrencyCode = "AUD", CurrencyAverageExchangeRate = 2.67 },
                new { CurrencyName = "dolar kanadyjski", CurrencyCode = "CAD", CurrencyAverageExchangeRate = 2.99 }
            };
        }
    }
}