public List<IAppAccount> Accounts
{
	get
	{
		return this._settings.Accounts;
	}
	set
	{
		this._settings.Accounts = value;
	}
}