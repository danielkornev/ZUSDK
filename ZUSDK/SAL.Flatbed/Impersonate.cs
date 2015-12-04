using System;

namespace SAL.Flatbed
{
	/// <summary>��������� ������������� ������������</summary>
	public class Impersonate : IImpersonate
	{
		private readonly String _login;
		private readonly String _password;
		private readonly Object _tag;
		/// <summary>�������� ����� ������������</summary>
		public String Login { get { return this._login; } }
		/// <summary>�������� ������ ������������</summary>
		public String Password { get { return this._password; } }
		/// <summary>�������� �������������� ���������� � ������������ � � ��������� ���������� ��������� � �������������</summary>
		public Object Tag { get { return this._tag; } }
		/// <summary>�������� ���������� ������ ������������� ������������ � ��������� ���� ������� ������</summary>
		/// <param name="login">����� ������������</param>
		/// <param name="password">������ ������������</param>
		/// <param name="tag">�������������� ����������</param>
		internal Impersonate(String login, String password, Object tag)
		{
			this._login = login;
			this._password = password;
			this._tag = tag;
		}
	}
}