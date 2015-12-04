using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.DirectoryServices;
using System.Security.Cryptography;

namespace System
{
	public static class SystemInformation
	{
		public static string GetOperatingSystemVersion()
		{
			// Get OperatingSystem information from the system namespace.
			System.OperatingSystem osInfo = System.Environment.OSVersion;

			// Determine the platform.
			switch (osInfo.Platform)
			{
				// Platform is Windows 95, Windows 98, 
				// Windows 98 Second Edition, or Windows Me.
				case System.PlatformID.Win32Windows:

					switch (osInfo.Version.Minor)
					{
						case 0:
							return "Windows 95";

						case 10:
							if (osInfo.Version.Revision.ToString() == "2222A")
								return "Windows 98 Second Edition";
							else
								return "Windows 98";

						case 90:
							return "Windows Me";

					}
					break;

				// Platform is Windows NT 3.51, Windows NT 4.0, Windows 2000,
				// or Windows XP.
				case System.PlatformID.Win32NT:

					switch (osInfo.Version.Major)
					{
						case 3:
							return "Windows NT 3.51";

						case 4:
							return "Windows NT 4.0";

						case 5:
							if (osInfo.Version.Minor == 0)
								return "Windows 2000";
							else
								return "Windows XP";

						case 6:
							if (osInfo.Version.Minor == 0)
								return "Windows Vista";
							else if (osInfo.Version.Minor == 1)
								return "Windows 7";
							else if (osInfo.Version.Minor == 2)
								return "Windows 8";
							else if (osInfo.Version.Minor == 3)
								return "Windows 8.1";
							else
								return "Windows 10 (Pre-Release)";

						case 10:
							return "Windows 10";
					} break;
			}
			return "Unknown Operating System";
		}

		public static string GetMachineModel()
		{
			// create management class object
			ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
			//collection to store all management objects
			ManagementObjectCollection moc = mc.GetInstances();
			if (moc.Count != 0)
			{
				foreach (ManagementObject mo in mc.GetInstances())
				{
					return mo["Model"].ToString();
				}
			}

			return string.Empty;
		}

		/// <summary>
		/// From http://blogs.msdn.com/b/securitytools/archive/2009/07/29/wmi-programming-using-c-net.aspx
		/// </summary>
		/// <returns></returns>
		public static string GetMachineManufacturer()
		{
			// create management class object
			ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
			//collection to store all management objects
			ManagementObjectCollection moc = mc.GetInstances();
			if (moc.Count != 0)
			{
				foreach (ManagementObject mo in mc.GetInstances())
				{
					return mo["Manufacturer"].ToString();
				}
			}

			return string.Empty;
		}

		public enum ChassisTypes
		{
			Other = 1,
			Unknown,
			Desktop,
			LowProfileDesktop,
			PizzaBox,
			MiniTower,
			Tower,
			Portable,
			Laptop,
			Notebook,
			Handheld,
			DockingStation,
			AllInOne,
			SubNotebook,
			SpaceSaving,
			LunchBox,
			MainSystemChassis,
			ExpansionChassis,
			SubChassis,
			BusExpansionChassis,
			PeripheralChassis,
			StorageChassis,
			RackMountChassis,
			SealedCasePC
		}

		/// <summary>
		/// From http://stackoverflow.com/questions/1013354/how-to-check-the-machine-type-laptop-or-desktop
		/// </summary>
		/// <returns></returns>
		public static ChassisTypes GetCurrentChassisType()
		{
			ManagementClass systemEnclosures = new ManagementClass("Win32_SystemEnclosure");
			foreach (ManagementObject obj in systemEnclosures.GetInstances())
			{
				foreach (int i in (UInt16[])(obj["ChassisTypes"]))
				{
					if (i > 0 && i < 25)
					{
						return (ChassisTypes)i;
					}
				}
			}
			return ChassisTypes.Unknown;
		}

		public static NetworkInterfaceType GetCurrentNetworkType()
		{
			if (!System.Net.NetworkStatus.IsAvailable)
				return NetworkInterfaceType.Unknown; // Unavailable

			try
			{
				UdpClient u = new UdpClient("www.microsoft.com", 1);
				IPAddress localAddr = ((IPEndPoint)u.Client.LocalEndPoint).Address;

				NetworkInterface current = null;

				List<IPAddress> addresses = new List<IPAddress>();

				foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
				{
					IPInterfaceProperties ipProps = nic.GetIPProperties();
					// check if localAddr is in ipProps.UnicastAddresses
					//if (ipProps.ToString())

					foreach (var ip in ipProps.UnicastAddresses)
					{
						addresses.Add(ip.Address);

						if (ip.Address.ToString() == localAddr.ToString())
						{
							current = nic;
							break;
						}
					}

					if (current != null)
						break;
				}

				if (current == null)
					return NetworkInterfaceType.Unknown; // strange

				else
					return current.NetworkInterfaceType;
			}
			catch (Exception ex)
			{
				return NetworkInterfaceType.Unknown;
			}
		}

		public static string GetComputerUDID()
		{
			//var sid = new SecurityIdentifier((byte[])new DirectoryEntry(string.Format("WinNT://{0},Computer", Environment.MachineName)).Children.Cast<DirectoryEntry>().First().InvokeGet("objectSID"), 0).AccountDomainSid;

			//var id = sid.ToString();

			return OpenUDID.value;
		}

		/// <summary>
		/// From https://github.com/jasonlamkk/OpenUDID.Net/blob/master/OpenUDID.Net/OpenUDIDCSharp/OpenUDID.cs
		/// </summary>
		private static class OpenUDID
		{
			public enum OpenUDIDErrors
			{
				None = 0,
				OptedOut = 1,
				Compromised = 2
			}
			private static String _cachedValue;
			private static OpenUDIDErrors _lastError;
			private static String _getOpenUDID()
			{
				_lastError = OpenUDIDErrors.None;
				if (_cachedValue == null)
				{
					//MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
					MD5 _md5 = MD5.Create();
					ManagementObjectSearcher _searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
					int i = 0;
					foreach (ManagementObject mo in _searcher.Get())
					{
						Console.WriteLine("CPU:{0} Info:\t{1}", i++, mo["ProcessorId"].ToString());
						byte[] bs = System.Text.Encoding.UTF8.GetBytes(mo["ProcessorId"].ToString());
						bs = _md5.ComputeHash(bs);
						StringBuilder s = new StringBuilder();
						foreach (byte b in bs)
						{
							s.Append(b.ToString("x2").ToLower());
						}
						_cachedValue = s.ToString();
					}

				}
				return _cachedValue;
			}

			public static String value
			{
				get
				{
					return _getOpenUDID();
				}
			}
			public static String valueWithError(out OpenUDIDErrors error)
			{
				String v = value;
				error = _lastError;
				return v;
			}
			public static String CorpIdentifier;
			public static String CorpValue
			{
				get
				{
					MD5 _md5 = MD5.Create();
					byte[] _buf = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}.{1}", CorpIdentifier, value));

					_buf = _md5.ComputeHash(_buf);

					StringBuilder s = new StringBuilder();
					foreach (byte b in _buf)
					{
						s.Append(b.ToString("x2").ToLower());
					}
					return s.ToString();
				}
			}
			public static String GetCorpUDID(String corpIdentifier)
			{
				CorpIdentifier = corpIdentifier;
				return CorpValue;
			}
		} // class
	} // class
} // namespace
