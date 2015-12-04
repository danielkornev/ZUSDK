using System;

namespace SAL.Flatbed
{
	/// <summary>Интерфейс олицетворения пользователя</summary>
	public class Impersonate : IImpersonate
	{
		private readonly String _login;
		private readonly String _password;
		private readonly Object _tag;
		/// <summary>Получить логин пользователя</summary>
		public String Login { get { return this._login; } }
		/// <summary>Получить пароль пользователя</summary>
		public String Password { get { return this._password; } }
		/// <summary>Получить дополнительную информацию о пользователе и о выбранных настройках связанных с безопасностью</summary>
		public Object Tag { get { return this._tag; } }
		/// <summary>Создание экземпляра класса олицетворения пользователя с указанием всех базовых данных</summary>
		/// <param name="login">Логин пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		/// <param name="tag">Дополнительная информация</param>
		internal Impersonate(String login, String password, Object tag)
		{
			this._login = login;
			this._password = password;
			this._tag = tag;
		}
	}
}