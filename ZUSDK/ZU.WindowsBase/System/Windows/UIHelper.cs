using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace System.Windows
{
	public static class UIHelper
	{
		/// <summary>
		/// Finds a parent of a given item on the visual tree.
		/// </summary>
		/// <typeparam name="T">The type of the queried item.</typeparam>
		/// <param name="child">A direct or indirect child of the queried item.</param>
		/// <returns>The first parent item that matches the submitted type parameter. 
		/// If not matching item can be found, a null reference is being returned.</returns>
		public static T FindVisualParent<T>(DependencyObject child)
		where T : DependencyObject
		{
			if (child == null) return null;

			// get parent item
			DependencyObject parentObject = VisualTreeHelper.GetParent(child);
			// we’ve reached the end of the tree
			if (parentObject == null)
				return null;
			// check if the parent matches the type we’re looking for
			T parent = parentObject as T;
			if (parent != null)
			{
				return parent;
			}
			else
			{
				// use recursion to proceed with next level
				return FindVisualParent<T>(parentObject);
			}
		}
	}
}
