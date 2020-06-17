using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBPCurrencyMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBPCurrencyMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomeViewModel HomeViewModel { get; }

        public HomePage()
        {
            InitializeComponent();

            HomeViewModel = new HomeViewModel(Navigation);
            BindingContext = HomeViewModel;
        }

        private void CurrencyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            HomeViewModel.GoToDetailsCommand.Execute(e.Item);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            HomeViewModel.UpdateCurrentExchangeRates();
        }
    }
}