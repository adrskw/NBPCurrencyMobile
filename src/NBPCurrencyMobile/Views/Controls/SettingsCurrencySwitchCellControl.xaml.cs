using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBPCurrencyMobile.Views.Controls
{
    public partial class SettingsCurrencySwitchCellControl : ViewCell
    {
        public delegate void SwitchToggledHandler(object sender, ToggledEventArgs e);

        public event SwitchToggledHandler OnSwitchToggled;

        public static readonly BindableProperty CurrencyCodeProperty = BindableProperty.Create(
            propertyName: nameof(CurrencyCode),
            returnType: typeof(string),
            declaringType: typeof(SettingsCurrencySwitchCellControl),
            defaultValue: null);

        public string CurrencyCode
        {
            get { return (string)GetValue(CurrencyCodeProperty); }
            set { SetValue(CurrencyCodeProperty, value); }
        }

        public static readonly BindableProperty CurrencyNameProperty = BindableProperty.Create(
            propertyName: nameof(CurrencyName),
            returnType: typeof(string),
            declaringType: typeof(SettingsCurrencySwitchCellControl),
            defaultValue: null);

        public string CurrencyName
        {
            get { return (string)GetValue(CurrencyNameProperty); }
            set { SetValue(CurrencyNameProperty, value); }
        }

        public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(
            propertyName: nameof(IsToggled),
            returnType: typeof(bool),
            declaringType: typeof(SettingsCurrencySwitchCellControl),
            defaultValue: true);

        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }

        public SettingsCurrencySwitchCellControl()
        {
            InitializeComponent();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            OnSwitchToggled?.Invoke(this, e);
        }
    }
}