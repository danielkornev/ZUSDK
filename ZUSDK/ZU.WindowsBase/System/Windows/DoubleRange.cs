using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace System.Windows
{
	public class DoubleRange : ConvertorBaseThis<DoubleRange>
	{
		/// <summary>
		/// Value high range
		/// </summary>
        public double ValHi
        {
        	set;
        	private get;
        }

		/// <summary>
		/// Value low range
		/// </summary>
        public double ValLo
        {
        	set;
        	private get;
        }

        private double _mul = 1.0;
        private double _add = 0.0;
        private bool _invert = false;

		/// <summary>
		/// Multiplier
		/// </summary>
        public double Mul
        {
        	set
        	{
        		_mul = value;
        	}
        	private get
        	{
        		return _mul;
        	}
        }

		/// <summary>
		/// Additional
		/// </summary>
        public double Add
        {
        	set
        	{
        		_add = value;
        	}
        	private get
        	{
        		return _add;
        	}
        }

		/// <summary>
		/// Additional
		/// </summary>
        public bool Invert
        {
        	set
        	{
        		_invert = value;
        	}
        	private get
        	{
        		return _invert;
        	}
        }

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double val = (double)value;
			double result = 1.0;
			System.Diagnostics.Debug.Assert((ValHi != ValLo), "Range is empty.");
			//
			if (val<ValHi)
			{
				if (val<ValLo)
				{
					result = 0.0;
				}
				else
				{
					result = val/(ValHi-ValLo);
				}
			}
			// Inversion
			if (Invert)
			{
				result = 1 - result;
			}
			// Result with Mul and Add
			return result * Mul + Add;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}//class
}//namespace
