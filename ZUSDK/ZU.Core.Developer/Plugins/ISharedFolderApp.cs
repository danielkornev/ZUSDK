﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Plugins
{
	public interface ISharedFolderApp : ILocalFolderApp
	{
		bool TryShareShellItem(string shellItemPath);
	} // interface
} // namespace
