using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZU.Configuration.Settings;

namespace ZU.Core.Apps
{
	public interface IFileStorageApp : IApp
	{
		void ImportShellItem(string localShellItemNamePath, IAppAccount account, IModel modelContext, Rect position);
	} // interface
} // namespace
