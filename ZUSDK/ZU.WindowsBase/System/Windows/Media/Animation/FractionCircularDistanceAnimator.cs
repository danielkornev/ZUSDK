using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Media.Animation
{
	class FractionCircularDistanceAnimator : IArrangeAnimator
	{
		public double Fraction { get; set; }
		public double Radius { get; set; }
		public Point CenterPosition { get; set; }

		public FractionCircularDistanceAnimator(double fraction)
		{
			Fraction = fraction;
		}

		public Rect Arrange(double elapsedTime, string elementName, Point desiredPosition, Size desiredSize, Point currentPosition, Size currentSize)
		{
			double deltaW = (desiredSize.Width - currentSize.Width) * Fraction;
			double deltaH = (desiredSize.Height - currentSize.Height) * Fraction;

			// 0. let's get current angle (relative to Center position)
			double currentAngle = Math.Atan2(currentPosition.Y - CenterPosition.Y, currentPosition.X - CenterPosition.X);

			var desiredAngle = Math.Atan2(desiredPosition.Y - CenterPosition.Y, desiredPosition.X - CenterPosition.X);

			// 1. let's calculate the arc's length
			var l = (desiredAngle - currentAngle) * Radius;

			// 2. fraction of arc
			var deltal = l * Fraction;

			// 3. angle
			var deltaAngle = deltal / Radius;

			var frAngle = currentAngle + deltaAngle;

			var newPosition = new Point(
								Radius * Math.Cos(frAngle) + CenterPosition.X,
								Radius * Math.Sin(frAngle) + CenterPosition.Y);

			var dx = Math.Abs(newPosition.X - desiredPosition.X);
			var dy = Math.Abs(newPosition.Y - desiredPosition.Y);

			if (dx < 2 && dy < 2)
				newPosition = desiredPosition;

			return new Rect(newPosition.X, newPosition.Y, desiredSize.Width, desiredSize.Height);
		}
	} // class
} // namespace