namespace RegiRide.Converters
{
    using System;
    using System.Windows.Data;

    using RegiRide.Resources;

    public class BoolToSwitchConverter : IValueConverter
    {
        private readonly string FalseValue = AppResources.Off;
        private readonly string TrueValue = AppResources.On;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return this.FalseValue;
            }

            return "On".Equals(value) ? this.TrueValue : this.FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null && value.Equals(this.TrueValue);
        }
    }
}
