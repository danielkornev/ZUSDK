using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace System.Windows
{
    /// <summary>
    /// A generic converter using lamdas.
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    /// <remarks>
    /// - Taken from http://khason.net/dev/nifty-time-savers-for-wpf-development/
    /// </remarks>
    public class ValueConverter<TIn, TOut> : IValueConverter
    {

        public ValueConverter(Func<TIn, TOut> forwardConversion)
        {
            ForwardConversion = forwardConversion;
        }

        public ValueConverter(Func<TIn, TOut> forwardConversion, Func<TOut, TIn> reverseConversion)
        {
            ForwardConversion = forwardConversion;
            ReverseConversion = reverseConversion;
        }

        public Func<TIn, TOut> ForwardConversion { get; set; }

        public Func<TOut, TIn> ReverseConversion { get; set; }

        #region IValueConverter Members

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var in1 = Object.ReferenceEquals(value, DependencyProperty.UnsetValue) ? default(TIn) : (TIn)value;
                return ForwardConversion(in1);
            }
            catch
            {
                return null;
            }
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var out1 = Object.ReferenceEquals(value, DependencyProperty.UnsetValue) ? default(TOut) : (TOut)value;
                return ReverseConversion(out1);
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
