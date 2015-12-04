using System;

namespace SAL.Flatbed
{
	/// <summary>Интерфейс передачи данных в элемент управления</summary>
	/// <remarks>Пока используется только при использовании интерфейса <see cref="I:IWindow"/></remarks>
	public interface IPluginData
	{
		/// <summary>Метод привязывания даных к элементу приложения</summary>
		/// <param name="data">Данные передаваемые в элемент приложения</param>
		/// <returns>Успешность привязывания данных</returns>
		Boolean DataBind(params Object[] data);
	}
}