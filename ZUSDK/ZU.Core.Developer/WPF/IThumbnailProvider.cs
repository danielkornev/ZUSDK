using System;
using System.Collections.Generic;
using ZU.Core;
namespace ZU.WPF
{
	/// <summary>
	/// This interface supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public interface IThumbnailProvider
	{
		void ClearThumbnail(ZU.Core.IEntity entity);
		void ClearThumbnail(ZU.Core.IEntity entity, bool notify);
		System.Windows.Media.ImageSource Empty { get; }
		//bool FileChangedAfterLastExtraction(ZU.Core.IEntity entity);
		System.Windows.Media.ImageSource GetThumbnail(ZU.Core.IEntity entity, bool getLarge, bool refreshCached = false);
		System.Windows.Media.Imaging.BitmapImage ToNormalThumbnailSize(System.Windows.Media.Imaging.BitmapSource bitmapSource);

		void NotifyEntityToUpdateThumbnail(IEntity entity);
		void DownloadAndSetThumbnail(IEntity entity, List<string> imageUrls);
	} // interface
} // class
