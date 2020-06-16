using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBPCurrencyMobile.Views.Controls
{
    public partial class SettingsDatePickerCellControl : ViewCell
    {
        public delegate void DateSelectedHandler(object sender, DateChangedEventArgs e);

        public event DateSelectedHandler OnDateSelected;

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(SettingsDatePickerCellControl),
            defaultValue: null);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty DisplayedDateProperty = BindableProperty.Create(
            propertyName: nameof(DisplayedDate),
            returnType: typeof(DateTime),
            declaringType: typeof(SettingsDatePickerCellControl),
            defaultValue: DateTime.Now);

        public DateTime DisplayedDate
        {
            get { return (DateTime)GetValue(DisplayedDateProperty); }
            set { SetValue(DisplayedDateProperty, value); }
        }

        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(
            propertyName: nameof(MinimumDate),
            returnType: typeof(DateTime),
            declaringType: typeof(SettingsDatePickerCellControl),
            defaultValue: DateTime.MinValue);

        public DateTime MinimumDate
        {
            get { return (DateTime)GetValue(MinimumDateProperty); }
            set { SetValue(MinimumDateProperty, value); }
        }

        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(
            propertyName: nameof(MaximumDate),
            returnType: typeof(DateTime),
            declaringType: typeof(SettingsDatePickerCellControl),
            defaultValue: DateTime.MaxValue);

        public DateTime MaximumDate
        {
            get { return (DateTime)GetValue(MaximumDateProperty); }
            set { SetValue(MaximumDateProperty, value); }
        }

        public SettingsDatePickerCellControl()
        {
            InitializeComponent();
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            OnDateSelected?.Invoke(this, e);
        }
    }
}