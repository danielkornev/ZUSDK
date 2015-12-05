using System;
using ZU.Plugins;

namespace ZU.Configuration.Settings
{
	public interface IAppAccount
	{
		string AccountId { get; set; }
		string AccountTitle { get; set; }
		IZetApp App { get; set; }
		string CloudAppUserRecordId { get; set; }
		string Email { get; set; }
		string Secret { get; set; }
		string Token { get; set; }
		string UserId { get; set; }
		string UserPassword { get; set; }
		string UserTitle { get; set; }
		string Value1 { get; set; }
		string Value2 { get; set; }
		string Value3 { get; set; }
		string Value4 { get; set; }
	} // class
} // namespace
