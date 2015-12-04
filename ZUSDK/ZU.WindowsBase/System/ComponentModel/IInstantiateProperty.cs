using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace System.ComponentModel
{
	public interface IInstantiateProperty
	{
		void InstantiateProperty(string propertyName, System.Globalization.CultureInfo culture, SynchronizationContext callbackExecutionContext);
	} // interface
} // namespace