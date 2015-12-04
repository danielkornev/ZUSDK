using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace System.Windows.Controls
{
	// source: http://wpf.2000things.com/2014/02/03/1000-displaying-the-contents-of-a-listbox-in-a-circle/
	// source: http://wpf.2000things.com/2014/02/03/1000-displaying-the-contents-of-a-listbox-in-a-circle/
	public class CircularPanel : Panel
	{
		private DispatcherTimer animationTimer;
		private DateTime lastArrange = DateTime.MinValue;
		private bool _drawOnce;

		public IArrangeAnimator Animator { get; set; }

		public CircularPanel()
			: this(new FractionCircularDistanceAnimator(0.25), TimeSpan.FromSeconds(0.05))
		{

		}

		public bool DrawOnce
		{
			get
			{
				return _drawOnce;
			}
			set
			{
				_drawOnce = value;
			}
		}

		public CircularPanel(IArrangeAnimator animator, TimeSpan animationInterval)
		{
			animationTimer = AnimationBase.CreateAnimationTimer(this, animationInterval);
			Animator = animator;
		}


		protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
		{
			foreach (UIElement child in Children)
				child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

			return base.MeasureOverride(availableSize);
		}

		// Arrange stuff in a circle
		protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
		{
			if (Children.Count > 0)
			{
				// Center & radius of panel
				Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);
				double radius = Math.Min(finalSize.Width, finalSize.Height) / 2.0;
				radius *= 0.65;   // To avoid hitting edges

				if (this.Animator.GetType() == typeof(FractionCircularDistanceAnimator))
				{
					var fcda = this.Animator as FractionCircularDistanceAnimator;
					fcda.CenterPosition = center;
					fcda.Radius = radius;
				}

				double angleInRadians = 0.0;

				var children = Children.OfType<ListBoxItem>().ToList();
				var selected = (from c in children
								where c.IsSelected == true
								select c).ToList();

				#region Equal Distribution
				if (selected.Count == 0)
				{
					// # radians between children
					double angleIncrRadians = 2.0 * Math.PI / Children.Count;

					foreach (UIElement child in Children)
					{
						Point childPosition = new Point(
							radius * Math.Cos(angleInRadians) + center.X,
							radius * Math.Sin(angleInRadians) + center.Y);

						child.Arrange(new Rect(childPosition, child.DesiredSize));

						angleInRadians += angleIncrRadians;
					}
				}
				#endregion
				#region Focused Distribution
				else
				{
					// let
					var specialAnglIncrRadians = 2; // 2 radians
					var angleIncrRadians = (2.0 * Math.PI - specialAnglIncrRadians * 2) / (Children.Count - 1);

					List<double> angles = new List<double>();

					// at first, we position the selected one
					var sel = selected.FirstOrDefault();

					Point currentPosition = (Point)sel.GetValue(AnimationBase.CurrentPositionProperty);
					double currentAngle = Math.Atan2(currentPosition.Y - center.Y, currentPosition.X - center.X);

					double selAnglRad = 2 * Math.PI - 1.15 * Math.PI;

					Point selPosition = new Point(
								radius * Math.Cos(-selAnglRad) + center.X,
								radius * Math.Sin(-selAnglRad) + center.Y); // always right 0,0 position

					sel.Arrange(new Rect(selPosition, sel.DesiredSize));

					angles.Add(-selAnglRad);

					// done

					#region order of things
					// now we need to find the one that is before it, right?
					Dictionary<ListBoxItem, int> ordered = new Dictionary<ListBoxItem, int>();
					Dictionary<int, ListBoxItem> orderedBack = new Dictionary<int, ListBoxItem>();

					int n = 0;

					// how could we find it? // for that we need to find its index
					foreach (var child in children)
					{
						ordered.Add(child, n);
						orderedBack.Add(n, child);
						n++;
					}
					#endregion

					// we've got the order, fine.
					var selInd = ordered[sel]; // this is the index

					var angleBefRad = selAnglRad + 2.9; // specialAnglIncrRadians;

					// then all other items are AFTER selected (0, 1,2,.. selInd-1)
					for (int j = selInd - 1; j >= 0; j--)
					{
						// we go to the right
						var child = Children[j];

						Point childPosition;

						childPosition = new Point(
								radius * Math.Cos(-angleBefRad) + center.X,
								radius * Math.Sin(-angleBefRad) + center.Y);

						child.Arrange(new Rect(childPosition, child.DesiredSize));

						angles.Add(-angleBefRad);

						angleBefRad += angleIncrRadians;
					}

					var left = new List<UIElement>();


					for (int i = selInd + 1; i <= Children.Count - 1; i++)
					{
						left.Add(Children[i]);
					}

					left.Reverse();

					foreach (var child in left)
					{
						Point childPosition;

						childPosition = new Point(
								radius * Math.Cos(-angleBefRad) + center.X,
								radius * Math.Sin(-angleBefRad) + center.Y);

						child.Arrange(new Rect(childPosition, child.DesiredSize));

						angles.Add(-angleBefRad);

						angleBefRad += angleIncrRadians;
					}
				}
				#endregion
			}


			// source: http://www.codeproject.com/Articles/466118/WPF-Layout-to-Layout-Transitions
			AnimationBase.Arrange(Math.Max(0, (DateTime.Now - lastArrange).TotalSeconds), this, Children, Animator);

			lastArrange = DateTime.Now;


			return finalSize;
		}
	} // class
}// namespace