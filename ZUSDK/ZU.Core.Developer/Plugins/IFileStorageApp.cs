using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZU.Configuration.Settings;
using ZU;
using ZU.Core;

namespace ZU.Plugins
{
	public interface IFileStorageApp : IZetApp
	{
		void ImportShellItem(string localShellItemNamePath, IAppAccount account, IModel modelContext, Rect position);
	} // interface
} // namespace
