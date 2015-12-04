using System;

namespace SAL.Flatbed
{
	/// <summary>Интерфейс базового элемента для всех объектов хоста</summary>
	public interface IHostItem
	{
		#region Properties
		/// <summary>Объект, который инкапсулируется интерфейсом</summary>
		Object Object { get; }
		#endregion Properties
	}
}