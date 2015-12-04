using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace System.Windows.Media.Animation
{
	// source: http://www.codeproject.com/Articles/153554/Animated-WPF-Panels
	public static class AnimationBase
	{
		public static DependencyProperty CurrentPositionProperty = DependencyProperty.RegisterAttached("CurrentPosition", typeof(Point), typeof(AnimationBase), new PropertyMetadata(new Point()));
		public static DependencyProperty CurrentSizeProperty = DependencyProperty.RegisterAttached("CurrentSize", typeof(Size), typeof(AnimationBase), new PropertyMetadata(new Size()));

		public static DependencyProperty OverrideArrangeProperty = DependencyProperty.RegisterAttached("OverrideArrange", typeof(bool), typeof(AnimationBase), new PropertyMetadata(false));
		public static DependencyProperty OverridePositionProperty = DependencyProperty.RegisterAttached("OverridePosition", typeof(Point), typeof(AnimationBase), new PropertyMetadata(new Point()));
		public static DependencyProperty OverrideSizeProperty = DependencyProperty.RegisterAttached("OverrideSize", typeof(Size), typeof(AnimationBase), new PropertyMetadata(new Size()));


		public static DispatcherTimer CreateAnimationTimer(UIElement owner, TimeSpan animationInterval)
		{
			DispatcherTimer animationTimer = new DispatcherTimer(DispatcherPriority.Render, owner.Dispatcher);
			animationTimer.Interval = animationInterval;
			animationTimer.Tick += (s, e) => { owner.InvalidateArrange(); };

			owner.IsVisibleChanged += (s, e) =>
			{
				if (((bool)e.NewValue))
					animationTimer.Start();
				else
					animationTimer.Stop();
			};

			return animationTimer;
		}

		public static void Arrange(double elapsedTime, UIElement owner, UIElementCollection elements, IArrangeAnimator animator)
		{
			foreach (UIElement element in elements)
			{
				var fxelement = element as FrameworkElement;
				if (fxelement == null)
					return;

				Point desiredPosition = (bool)element.GetValue(AnimationBase.OverrideArrangeProperty) ? (Point)element.GetValue(AnimationBase.OverridePositionProperty) : element.TranslatePoint(new Point(), owner);
				Size desiredSize = (bool)element.GetValue(AnimationBase.OverrideArrangeProperty) ? (Size)element.GetValue(AnimationBase.OverrideSizeProperty) : element.RenderSize;

				Point currentPosition = (Point)element.GetValue(AnimationBase.CurrentPositionProperty);
				Size currentSize = (Size)element.GetValue(AnimationBase.CurrentSizeProperty);

				Rect rect = animator.Arrange(elapsedTime, fxelement.Name, desiredPosition, desiredSize, currentPosition, currentSize);

				element.SetValue(AnimationBase.CurrentPositionProperty, rect.TopLeft);
				element.SetValue(AnimationBase.CurrentSizeProperty, rect.Size);


				element.Arrange(rect);
			}
		}

	} // class
} // namespace

