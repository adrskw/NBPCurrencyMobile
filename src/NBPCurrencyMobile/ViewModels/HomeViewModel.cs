using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NBPCurrencyMobile.Views;
using Xamarin.Forms;

namespace NBPCurrencyMobile.ViewModels
{
    public class HomeViewModel
    {
        public ICommand GoToDetailsCommand => goToDetailsCommand ?? (goToDetailsCommand = new Command(GoToDetails));
        private ICommand goToDetailsCommand;
        private readonly INavigation navigation;

        public HomeViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

        private void GoToDetails()
        {
            navigation.PushAsync(new DetailsPage());
        }
    }
}