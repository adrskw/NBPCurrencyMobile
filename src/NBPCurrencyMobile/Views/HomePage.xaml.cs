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
        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel(Navigation);
        }

        private void CurrencyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            HomeViewModel homeViewModel = BindingContext as HomeViewModel;
            homeViewModel.GoToDetailsCommand.Execute(e.Item);
            CurrencyListView.SelectedItem = null;
        }
    }
}