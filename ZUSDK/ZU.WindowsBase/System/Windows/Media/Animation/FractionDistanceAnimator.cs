using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Windows.Media.Animation
{
	// source: http://www.codeproject.com/Articles/153554/Animated-WPF-Panels
	public class FractionDistanceAnimator : IArrangeAnimator
	{
		public double Fraction { get; set; }

		public FractionDistanceAnimator(double fraction)
		{
			Fraction = fraction;
		}

		public Rect Arrange(double elapsedTime, string elementName, Point desiredPosition, Size desiredSize, Point currentPosition, Size currentSize)
		{
			double deltaX = (desiredPosition.X - currentPosition.X) * Fraction;
			double deltaY = (desiredPosition.Y - currentPosition.Y) * Fraction;
			double deltaW = (desiredSize.Width - currentSize.Width) * Fraction;
			double deltaH = (desiredSize.Height - currentSize.Height) * Fraction;

			Debug.WriteLine(elementName + " (" + currentPosition.X + deltaX + "; " + currentPosition.Y + deltaY + "); target: " + desiredPosition);

			return new Rect(currentPosition.X + deltaX, currentPosition.Y + deltaY, currentSize.Width + deltaW, currentSize.Height + deltaH);
		}
	} // class
} // namespace