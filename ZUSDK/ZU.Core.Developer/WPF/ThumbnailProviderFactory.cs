using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.WPF
{
	/// <summary>
	/// This class supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
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
