using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZU.ComponentModel
{
	/// <summary>
	/// This interface supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public interface IInstantiateProperty
	{
		void InstantiateProperty(string propertyName, System.Globalization.CultureInfo culture, SynchronizationContext callbackExecutionContext);
	} // interface
} // namespace