public bool OnConnection(ConnectMode mode)
{
	if (this.Host == null) throw new NotImplementedException("Host is not available");

	this.ZetHost = this.Host as IZetHost;

	if (this.ZetHost == null) throw new PlatformNotSupportedException("This build of Zet Universe platform is not supported. Host doesn't implement ZU.Plugins.IZetHost interface");

	IAppSettings settings = this.ZetHost.AppManager.GetAppSettings(this).Result;

	// Continue plugin initialization using a separate Initialize() method
	this.Initialize(settings, this.ZetHost.SIM, this.ZetHost.AppManager);

	return true;
}