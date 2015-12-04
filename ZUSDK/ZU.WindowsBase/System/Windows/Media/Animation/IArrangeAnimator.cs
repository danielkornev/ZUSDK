using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Media.Animation
{
	// Source: http://www.codeproject.com/Articles/153554/Animated-WPF-Panels
	public interface IArrangeAnimator
	{
		Rect Arrange(double elapsedTime, string elementName, Point desiredPosition, Size desiredSize, Point currentPosition, Size currentSize);
	} // interface
} // namespace