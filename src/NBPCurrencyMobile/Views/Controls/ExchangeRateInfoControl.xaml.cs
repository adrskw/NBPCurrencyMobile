using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBPCurrencyMobile.Views.Controls
{
    public partial class ExchangeRateInfoControl : StackLayout
    {
        private double infoGridBaseHeight = -1;
        private bool isAnimationInProgress = false;
        private bool isExpanded = true;

        public ExchangeRateInfoControl()
        {
            InitializeComponent();
            UpdateDropdownIcon();
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: nameof(Title),
            returnType: typeof(string),
            declaringType: typeof(ExchangeRateInfoControl),
            defaultValue: null);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty AverageExchangeRateProperty = BindableProperty.Create(
            propertyName: nameof(AverageExchangeRate),
            returnType: typeof(decimal),
            declaringType: typeof(ExchangeRateInfoControl),
            defaultValue: null);

        public decimal AverageExchangeRate
        {
            get { return (decimal)GetValue(AverageExchangeRateProperty); }
            set { SetValue(AverageExchangeRateProperty, value); }
        }

        public static readonly BindableProperty MinimumExchangeRateProperty = BindableProperty.Create(
            propertyName: nameof(MinimumExchangeRate),
            returnType: typeof(decimal),
            declaringType: typeof(ExchangeRateInfoControl),
            defaultValue: null);

        public decimal MinimumExchangeRate
        {
            get { return (decimal)GetValue(MinimumExchangeRateProperty); }
            set { SetValue(MinimumExchangeRateProperty, value); }
        }

        public static readonly BindableProperty MaximumExchangeRateProperty = BindableProperty.Create(
            propertyName: nameof(MaximumExchangeRate),
            returnType: typeof(decimal),
            declaringType: typeof(ExchangeRateInfoControl),
            defaultValue: null);

        public decimal MaximumExchangeRate
        {
            get { return (decimal)GetValue(MaximumExchangeRateProperty); }
            set { SetValue(MaximumExchangeRateProperty, value); }
        }

        public static readonly BindableProperty StandardDeviationProperty = BindableProperty.Create(
            propertyName: nameof(StandardDeviation),
            returnType: typeof(decimal),
            declaringType: typeof(ExchangeRateInfoControl),
            defaultValue: null);

        public decimal StandardDeviation
        {
            get { return (decimal)GetValue(StandardDeviationProperty); }
            set { SetValue(StandardDeviationProperty, value); }
        }

        public static readonly BindableProperty GreatestExchangeRateDifferenceProperty = BindableProperty.Create(
            propertyName: nameof(GreatestExchangeRateDifference),
            returnType: typeof(string),
            declaringType: typeof(ExchangeRateInfoControl),
            defaultValue: null);

        public string GreatestExchangeRateDifference
        {
            get { return (string)GetValue(GreatestExchangeRateDifferenceProperty); }
            set { SetValue(GreatestExchangeRateDifferenceProperty, value); }
        }

        private async void InfoButton_Clicked(object sender, EventArgs e)
        {
            if (!isAnimationInProgress)
            {
                isAnimationInProgress = true;
                double startHeight;
                double endHeight;

                if (infoGridBaseHeight == -1)
                {
                    infoGridBaseHeight = InfoGrid.Height;
                }

                if (isExpanded)
                {
                    startHeight = infoGridBaseHeight;
                    endHeight = 0;
                }
                else
                {
                    startHeight = 0;
                    endHeight = infoGridBaseHeight;
                }

                isExpanded = !isExpanded;
                UpdateDropdownIcon();

                var animate = new Animation(height => InfoGrid.HeightRequest = height, startHeight, endHeight, Easing.CubicIn);
                animate.Commit(InfoGrid, "InfoGridCollapse", 16, 500);

                await Task.Delay(500);
                isAnimationInProgress = false;
            }
        }

        private void UpdateDropdownIcon()
        {
            string icon;

            if (isExpanded)
            {
                icon = "dropdownArrowUpIcon.png";
            }
            else
            {
                icon = "dropdownArrowDownIcon.png";
            }

            InfoButton.ImageSource = ImageSource.FromFile(icon);
        }
    }
}