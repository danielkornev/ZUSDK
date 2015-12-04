using System;

namespace SAL.Flatbed
{
	/// <summary>Интерфейс </summary>
	public interface IImpersonate
	{
		/// <summary>Получить логин пользователя</summary>
		String Login { get; }
		/// <summary>Получить пароль пользователя</summary>
		String Password { get; }
		/// <summary>Получить дополнительную информацию о пользователе и о выбранных настройках связанных с безопасностью</summary>
		Object Tag { get; }
	}
}