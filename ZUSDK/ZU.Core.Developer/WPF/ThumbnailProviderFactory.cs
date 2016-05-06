using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.WPF
{
	public static class ThumbnailProviderFactory
	{
		public static IThumbnailProvider Instance
		{ get; internal set; }

		internal static void Initialize(IThumbnailProvider cache)
		{
			Instance = cache;
		}
	} // class
} // namespace
