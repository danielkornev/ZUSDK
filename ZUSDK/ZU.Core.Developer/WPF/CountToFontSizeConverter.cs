using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZU.Core;
using ZU.Semantic.Keyphrases;

namespace ZU.WPF
{
	public sealed class CountToFontSizeConverter : IValueConverter
	{
		public IEntity Entity
		{ get; set; }

		

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			const int minFontSize = 6;
			const int maxFontSize = 22;
			const int increment = 3;

			var keyphrase = value as IKeyphrase;

			if (keyphrase == null)
			{
				return 12;
			}
			

			double rank100 = 0;

			if (this.Entity.Kind == Constants.Kinds.VisualCluster)
			{
				// we need to normalize keyphrase ranks
				if (keyphrase.CalculatedRank > 1)
					rank100 = 1 * 10;
				else
					rank100 = (double)keyphrase.CalculatedRank * 10;
			}
			else
			{
				rank100 = (double)keyphrase.Rank * 10; // as numbers coming are between 0 and 1,
				// we'll get smth. like 5.3 for 0.53
			}

			// now, we need to get whole part of 5.3
			double countD = Math.Round(rank100);

			int count = (int)countD;

			//int intPart = Convert.ToInt32(rank); //just convert to int, loose the dec.
			//int fractionalPart = (int)((position - intPart) * 1000); //rounding was not needed

			//int count = (int)value;

			var val = ((minFontSize + count + increment) < maxFontSize) ? (minFontSize + count + increment) : maxFontSize;

			return val;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		#endregion
	}
}
