using System;
using ZU.Core;
namespace ZU.WPF
{
	public interface IEntityThumbnailCache
	{
		void ClearThumbnail(ZU.Core.IEntity entity);
		void ClearThumbnail(ZU.Core.IEntity entity, bool notify);
		System.Windows.Media.ImageSource Empty { get; }
		bool FileChangedAfterLastExtraction(ZU.Core.IEntity entity);
		System.Windows.Media.ImageSource GetThumbnail(ZU.Core.IEntity entity, bool getLarge, bool refreshCached = false);
		void SetThumbnailAsync(ZU.Core.IEntity entity, System.Windows.Media.ImageSource imgSrc, bool saveToLargeImagesCache, string uri, bool cacheUri);
		System.Windows.Media.Imaging.BitmapImage ToNormalThumbnailSize(System.Windows.Media.Imaging.BitmapSource bitmapSource);

		void NotifyEntityToUpdateThumbnail(IEntity entity);
		void Save();

		void RebuildIndex();
	} // interface
} // class
