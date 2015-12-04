using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.WPF
{
	public static class EntityThumbnailCacheFactory
	{
		public static IEntityThumbnailCache Instance
		{ get; internal set; }

		internal static void Initialize(IEntityThumbnailCache cache)
		{
			Instance = cache;
		}
	} // class
} // namespace
