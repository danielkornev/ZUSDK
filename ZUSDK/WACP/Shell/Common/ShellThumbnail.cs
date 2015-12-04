//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Shell.Resources;
using MS.WindowsAPICodePack.Internal;
using System.Windows.Media;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Threading;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Represents a thumbnail or an icon for a ShellObject.
    /// </summary>
    public class ShellThumbnail
    {
        #region Private members

        /// <summary>
        /// Native shellItem
        /// </summary>
        private IShellItem shellItemNative;
		private ShellObject shellItemManaged;

        /// <summary>
        /// Internal member to keep track of the current size
        /// </summary>
        private System.Windows.Size currentSize = new System.Windows.Size(256, 256);

        #endregion

        #region Constructors

        /// <summary>
        /// Internal constructor that takes in a parent ShellObject.
        /// </summary>
        /// <param name="shellObject"></param>
        internal ShellThumbnail(ShellObject shellObject)
        {
            if (shellObject == null || shellObject.NativeShellItem == null)
            {
                throw new ArgumentNullException("shellObject");
            }

			shellItemManaged = shellObject;
            shellItemNative = shellObject.NativeShellItem;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the default size of the thumbnail or icon. The default is 32x32 pixels for icons and 
        /// 256x256 pixels for thumbnails.
        /// </summary>
        /// <remarks>If the size specified is larger than the maximum size of 1024x1024 for thumbnails and 256x256 for icons,
        /// an <see cref="System.ArgumentOutOfRangeException"/> is thrown.
        /// </remarks>
        public System.Windows.Size CurrentSize
        {
            get { return currentSize; }
            set
            {
                // Check for 0; negative number check not required as System.Windows.Size only allows positive numbers.
                if (value.Height == 0 || value.Width == 0)
                {
                    throw new System.ArgumentOutOfRangeException("value", LocalizedMessages.ShellThumbnailSizeCannotBe0);
                }

                System.Windows.Size size = (FormatOption == ShellThumbnailFormatOption.IconOnly) ?
                    DefaultIconSize.Maximum : DefaultThumbnailSize.Maximum;

                if (value.Height > size.Height || value.Width > size.Width)
                {
                    throw new System.ArgumentOutOfRangeException("value",
                        string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        LocalizedMessages.ShellThumbnailCurrentSizeRange, size.ToString()));
                }

                currentSize = value;
            }
        }

        /// <summary>
        /// Gets the thumbnail or icon image in <see cref="System.Drawing.Bitmap"/> format.
        /// Null is returned if the ShellObject does not have a thumbnail or icon image.
        /// </summary>
        public Bitmap Bitmap { get { return GetBitmap(CurrentSize); } }

        /// <summary>
        /// Gets the thumbnail or icon image in <see cref="System.Windows.Media.Imaging.BitmapSource"/> format. 
        /// Null is returned if the ShellObject does not have a thumbnail or icon image.
        /// </summary>
        public BitmapSource BitmapSource { get { return GetBitmapSource(CurrentSize); } }

        /// <summary>
        /// Gets the thumbnail or icon image in <see cref="System.Drawing.Icon"/> format. 
        /// Null is returned if the ShellObject does not have a thumbnail or icon image.
        /// </summary>
        public Icon Icon { get { return Icon.FromHandle(Bitmap.GetHicon()); } }

        /// <summary>
        /// Gets the thumbnail or icon in small size and <see cref="System.Drawing.Bitmap"/> format.
        /// </summary>
        public Bitmap SmallBitmap
        {
            get
            {
                return GetBitmap(DefaultIconSize.Small, DefaultThumbnailSize.Small);
            }
        }

        /// <summary>
        /// Gets the thumbnail or icon in small size and <see cref="System.Windows.Media.Imaging.BitmapSource"/> format.
        /// </summary>
        public BitmapSource SmallBitmapSource
        {
            get
            {
                return GetBitmapSource(DefaultIconSize.Small, DefaultThumbnailSize.Small);
            }
        }

        /// <summary>
        /// Gets the thumbnail or icon in small size and <see cref="System.Drawing.Icon"/> format.
        /// </summary>
        public Icon SmallIcon { get { return Icon.FromHandle(SmallBitmap.GetHicon()); } }

        /// <summary>
        /// Gets the thumbnail or icon in Medium size and <see cref="System.Drawing.Bitmap"/> format.
        /// </summary>
        public Bitmap MediumBitmap
        {
            get
            {
                return GetBitmap(DefaultIconSize.Medium, DefaultThumbnailSize.Medium);
            }
        }

        /// <summary>
        /// Gets the thumbnail or icon in medium (96x96) size and <see cref="System.Windows.Media.Imaging.BitmapSource"/> format.
        /// </summary>
        public BitmapSource MediumBitmapSource
        {
            get
            {
                return GetBitmapSource(DefaultIconSize.Medium, DefaultThumbnailSize.Medium);
            }
        }

        /// <summary>
        /// Gets the thumbnail or icon in Medium size and <see cref="System.Drawing.Icon"/> format.
        /// </summary>
        public Icon MediumIcon { get { return Icon.FromHandle(MediumBitmap.GetHicon()); } }

        /// <summary>
        /// Gets the thumbnail or icon in large size and <see cref="System.Drawing.Bitmap"/> format.
        /// </summary>
        public Bitmap LargeBitmap
        {
            get
            {
                return GetBitmap(DefaultIconSize.Large, DefaultThumbnailSize.Large);
            }
        }

        /// <summary>
        /// Gets the thumbnail or icon in large (256x256) size and <see cref="System.Windows.Media.Imaging.BitmapSource"/> format.
        /// </summary>
        public BitmapSource LargeBitmapSource
        {
            get
            {
                return GetBitmapSource(DefaultIconSize.Large, DefaultThumbnailSize.Large);
            }
        }

        /// <summary>
        /// Gets the thumbnail or icon in Large size and <see cref="System.Drawing.Icon"/> format.
        /// </summary>
        public Icon LargeIcon { get { return Icon.FromHandle(LargeBitmap.GetHicon()); } }

        /// <summary>
        /// Gets the thumbnail or icon in extra large size and <see cref="System.Drawing.Bitmap"/> format.
        /// </summary>
        public Bitmap ExtraLargeBitmap
        {
            get
            {
                return GetBitmap(DefaultIconSize.ExtraLarge, DefaultThumbnailSize.ExtraLarge);
            }
        }

        /// <summary>
        /// Gets the thumbnail or icon in Extra Large (1024x1024) size and <see cref="System.Windows.Media.Imaging.BitmapSource"/> format.
        /// </summary>
        public BitmapSource ExtraLargeBitmapSource
        {
            get
            {
                return GetBitmapSource(DefaultIconSize.ExtraLarge, DefaultThumbnailSize.ExtraLarge);
            }
        }

        /// <summary>
        /// Gets the thumbnail or icon in Extra Large size and <see cref="System.Drawing.Icon"/> format.
        /// </summary>
        public Icon ExtraLargeIcon { get { return Icon.FromHandle(ExtraLargeBitmap.GetHicon()); } }

        /// <summary>
        /// Gets or sets a value that determines if the current retrieval option is cache or extract, cache only, or from memory only.
        /// The default is cache or extract.
        /// </summary>
        public ShellThumbnailRetrievalOption RetrievalOption { get; set; }

        private ShellThumbnailFormatOption formatOption = ShellThumbnailFormatOption.Default;
        /// <summary>
        /// Gets or sets a value that determines if the current format option is thumbnail or icon, thumbnail only, or icon only.
        /// The default is thumbnail or icon.
        /// </summary>
        public ShellThumbnailFormatOption FormatOption
        {
            get { return formatOption; }
            set
            {
                formatOption = value;

                // Do a similar check as we did in CurrentSize property setter,
                // If our mode is IconOnly, then our max is defined by DefaultIconSize.Maximum. We should make sure 
                // our CurrentSize is within this max range
                if (FormatOption == ShellThumbnailFormatOption.IconOnly
                    && (CurrentSize.Height > DefaultIconSize.Maximum.Height || CurrentSize.Width > DefaultIconSize.Maximum.Width))
                {
                    CurrentSize = DefaultIconSize.Maximum;
                }
            }

        }

        /// <summary>
        /// Gets or sets a value that determines if the user can manually stretch the returned image.
        /// The default value is false.
        /// </summary>
        /// <remarks>
        /// For example, if the caller passes in 80x80 a 96x96 thumbnail could be returned. 
        /// This could be used as a performance optimization if the caller will need to stretch 
        /// the image themselves anyway. Note that the Shell implementation performs a GDI stretch blit. 
        /// If the caller wants a higher quality image stretch, they should pass this flag and do it themselves.
        /// </remarks>
        public bool AllowBiggerSize { get; set; }

        #endregion

        #region Private Methods

        private ShellNativeMethods.SIIGBF CalculateFlags()
        {
            ShellNativeMethods.SIIGBF flags = 0x0000;

            if (AllowBiggerSize)
            {
                flags |= ShellNativeMethods.SIIGBF.BiggerSizeOk;
            }

            if (RetrievalOption == ShellThumbnailRetrievalOption.CacheOnly)
            {
                flags |= ShellNativeMethods.SIIGBF.InCacheOnly;
            }
            else if (RetrievalOption == ShellThumbnailRetrievalOption.MemoryOnly)
            {
                flags |= ShellNativeMethods.SIIGBF.MemoryOnly;
            }

            if (FormatOption == ShellThumbnailFormatOption.IconOnly)
            {
                flags |= ShellNativeMethods.SIIGBF.IconOnly;
            }
            else if (FormatOption == ShellThumbnailFormatOption.ThumbnailOnly)
            {
                flags |= ShellNativeMethods.SIIGBF.ThumbnailOnly;
            }

            return flags;
        }

		// might not be neccessary
		//[HandleProcessCorruptedStateExceptions]
		private IntPtr GetHBitmap(System.Windows.Size size)
		{
			IntPtr hbitmap = IntPtr.Zero;
			bool obtained = false;

			// Create a size structure to pass to the native method
			CoreNativeMethods.Size nativeSIZE = new CoreNativeMethods.Size();
			nativeSIZE.Width = Convert.ToInt32(size.Width);
			nativeSIZE.Height = Convert.ToInt32(size.Height);

			//if (Dispatcher.CurrentDispatcher.)

			System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(
				(Action)(() =>
				{



					//try
					//{

					// Use IShellItemImageFactory to get an icon
					// Options passed in: Resize to fit
					HResult hr = ((IShellItemImageFactory)shellItemNative).GetImage(nativeSIZE, CalculateFlags(), out hbitmap);

					if (hr == HResult.Ok) { obtained = true; }
					else if ((uint)hr == 0x8004B200 && FormatOption == ShellThumbnailFormatOption.ThumbnailOnly)
					{
						// Thumbnail was requested, but this ShellItem doesn't have a thumbnail.
						throw new InvalidOperationException(LocalizedMessages.ShellThumbnailDoesNotHaveThumbnail, Marshal.GetExceptionForHR((int)hr));
					}
					else if ((uint)hr == 0x80040154) // REGDB_E_CLASSNOTREG
					{
						throw new NotSupportedException(LocalizedMessages.ShellThumbnailNoHandler, Marshal.GetExceptionForHR((int)hr));
					}

					if (hr != HResult.Ok)
						throw new ShellException(hr);
					//}
					//catch (System.InvalidOperationException ex)
					//{
					//	var itemParsingName = shellItemManaged.ParsingName;



					//	// we should log it
					//	// 
					//	//string error = ex.Message + " happened with: " + itemName;
					//}
				}));

			return hbitmap;
		}

        private Bitmap GetBitmap(System.Windows.Size iconOnlySize, System.Windows.Size thumbnailSize)
        {
            return GetBitmap(FormatOption == ShellThumbnailFormatOption.IconOnly ? iconOnlySize : thumbnailSize);
        }

        private Bitmap GetBitmap(System.Windows.Size size)
        {
            IntPtr hBitmap = GetHBitmap(size);

            // return a System.Drawing.Bitmap from the hBitmap
            Bitmap returnValue = Bitmap.FromHbitmap(hBitmap);

            // delete HBitmap to avoid memory leaks
            ShellNativeMethods.DeleteObject(hBitmap);

            return returnValue;
        }

        private BitmapSource GetBitmapSource(System.Windows.Size iconOnlySize, System.Windows.Size thumbnailSize)
        {
            return GetBitmapSource(FormatOption == ShellThumbnailFormatOption.IconOnly ? iconOnlySize : thumbnailSize);
        }

        private BitmapSource GetBitmapSource(System.Windows.Size size)
        {
			//try
			//{
				IntPtr hBitmap = GetHBitmap(size);

				// what if?
				if (hBitmap == IntPtr.Zero)
				{
					return CreateEmptyImage(size);
				}

				// return a System.Media.Imaging.BitmapSource
				// Use interop to create a BitmapSource from hBitmap.
				BitmapSource returnValue = Imaging.CreateBitmapSourceFromHBitmap(
					hBitmap,
					IntPtr.Zero,
					System.Windows.Int32Rect.Empty,
					BitmapSizeOptions.FromEmptyOptions());

				// delete HBitmap to avoid memory leaks
				ShellNativeMethods.DeleteObject(hBitmap);

				return returnValue;
			//}
			//catch (Exception ex)
			//{
			//	return CreateEmptyImage(size);
			//}
        }

		private BitmapSource CreateEmptyImage(System.Windows.Size size)
		{
			int width = Convert.ToInt32(size.Width);
			var height = Convert.ToInt32(size.Height);

			int stride = width / 8;
			byte[] pixels = new byte[height * stride];

			List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
			colors.Add(Colors.White);
			BitmapPalette myPalette = new BitmapPalette(colors);



			var i = BitmapImage.Create(
				width,
				height,
				96,
				96,
				PixelFormats.Indexed1,
				myPalette,
				pixels,
				stride);

			return i;
		}

        #endregion

    }
}