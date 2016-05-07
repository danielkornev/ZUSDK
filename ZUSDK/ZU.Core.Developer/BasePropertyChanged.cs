using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

//--using System.Windows;
//--using System.Windows.Input;

namespace ZU
{
	//	+	INotifyPropertyChanged implementation
	//	+	Thread safe PropertyChanged calls
	//	+	Call CommandManager.InvalidateRequerySuggested for properties with names starts with "Is"|"Can"
	//	+	SetProperty<T> helper
	/// <summary>
	/// This class supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>

	public abstract class BasePropertyChanged : INotifyPropertyChanged
	{
	#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		public virtual void Changed(string propertyName)
		{
			if (PropertyChanged != null)
			{
				var action =
					new Action
					(
						delegate
						{
							Platform.InputInvalidate(propertyName);
							PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
						}
					);
				Platform.InvokeInUI(action);
			}
		}
	#endregion INotifyPropertyChanged

	#region Helpers
		public void SetProperty<T>(T value, ref T _field, string _property)
		{
			if ((_field==null) || (!_field.Equals(value)))
			{
				_field = value;
				Changed(_property);
			}
		}//SetProperty<T>()
	#endregion Helpers
	}//class
}//namespace
