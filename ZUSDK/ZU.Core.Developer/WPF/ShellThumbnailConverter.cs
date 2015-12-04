//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Data;
//using System.Windows.Media.Imaging;
//using ZU.Core;
//using ZU.Core.Apps;

//namespace System.Windows
//{
//	public class ShellThumbnailConverter : IValueConverter
//	{
//		#region Internal (in-memory) thumb cache
//		// WARNING: Not intended to use with big data!
//		public static Dictionary<string, System.Windows.Media.ImageSource> File2Thumb = new Dictionary<string, System.Windows.Media.ImageSource>();
//		#endregion

//		public static void Clear()
//		{
//			File2Thumb = new Dictionary<string, System.Windows.Media.ImageSource>();
//		}

//		#region IValueConverter Members

//		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//		{
//			//if (value == null)
//				return new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/Empty.png"));

//			var ent = value as Entity;

//			if (ent.NotifyThumbnailToUpdate == null)
//				ent.NotifyThumbnailToUpdate = NotifyEntityThumbnailToUpdate;

//			bool useStdThumbnail = true;

//			if (parameter != null)
//			{
//				var par = parameter as string;

//				if (par == "large")
//					useStdThumbnail = false;

//			}
//			System.Windows.Media.ImageSource imgSrc = null;

//			Platform.InvokeInUI(() =>
//				{
//					imgSrc = GetOrUpdateThumbnail(ent, useStdThumbnail);

//					//if (imgSrc.CanFreeze)
//					//	imgSrc.Freeze();
//				});

//			return imgSrc;
//		}

//		private static Media.ImageSource GetOrUpdateThumbnail(Entity ent, bool useStdThumbnail)
//		{
//			var path = (ent == null) ? string.Empty : ent.Id; // changed from Uri to Id

//			if ((ent != null) && (ent.Kind == Constants.Kinds.ProjectSpace))
//			{
//				if (System.IO.File.Exists(ent.ThumbnailUri) && (
//					System.IO.Path.GetExtension(ent.ThumbnailUri).Contains(".png")
//					||
//					System.IO.Path.GetExtension(ent.ThumbnailUri).Contains(".jpg"))
//					)
//				{
//					// then Thumbnail Uri should be used
//					path = ent.Id; // crazy?
//				}
//				else
//				{
//					if (!System.IO.Path.IsPathRooted(ent.Uri))
//					{
//						// dropbox space
//						path = "*dropbox_space";
//					}
//					else // personal space
//					{
//						path = "*personal_space";
//					}
//				}

//			}

//			if ((ent != null) && (ent.Kind == Constants.Kinds.VisualCluster))
//			{
//				path = "*topic";
//			}

//			//
//			if ((ent != null && ent.Kind == Constants.Kinds.List))
//			{
//				// for now, all lists will have same thumbnail
//				path = "*list";
//			}

//			if (ent!=null && ent.Kind == Constants.Kinds.Person && string.IsNullOrEmpty(ent.ThumbnailUri))
//			{
//				path = "*person";
//			}

//			if (ent!=null && ent.Kind == Constants.Kinds.ZetUniverse)
//			{
//				path = "*system";
//			}

//			//
//			System.Windows.Media.ImageSource imgSrc = null;
//			// default values
//			//if (File2Thumb.Count == 0)
//			//{
			
			
//			// Default
//			if (!File2Thumb.ContainsKey(string.Empty))
//			{
//				imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/Empty.png"));
//				if (imgSrc.CanFreeze)
//					imgSrc.Freeze();
				
//				File2Thumb.Add(string.Empty, imgSrc);
//			}

//				// Personal Space
//				//imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/private_folder.png"));

//			if (!File2Thumb.ContainsKey("*personal_space"))
//			{
//				imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/Local_gray.jpg"));
//				if (imgSrc.CanFreeze)
//					imgSrc.Freeze();
				
//				File2Thumb.Add("*personal_space", imgSrc);
//			}

//				// Dropbox Space
//				//imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/shared_folder.png"));

//			if (!File2Thumb.ContainsKey("*dropbox_space"))
//			{
//				imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/Local_colored.jpg"));
//				if (imgSrc.CanFreeze)
//					imgSrc.Freeze();
				
//				File2Thumb.Add("*dropbox_space", imgSrc);
//			}

//			if (!File2Thumb.ContainsKey("*topic"))
//			{
//				// Topic Space
//				imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/Cluster_New.png"));
//				if (imgSrc.CanFreeze)
//					imgSrc.Freeze();
				
//				File2Thumb.Add("*topic", imgSrc);
//			}

//			if (!File2Thumb.ContainsKey("*list"))
//			{	// List
//				imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/Stack.png"));
//				if (imgSrc.CanFreeze)
//					imgSrc.Freeze();
				
//				File2Thumb.Add("*list", imgSrc);
//			}

//			if (!File2Thumb.ContainsKey("*person"))
//			{
//				// Person
//				imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/Person.png"));
//				if (imgSrc.CanFreeze)
//					imgSrc.Freeze();
				
//				File2Thumb.Add("*person", imgSrc);
//			}

//			if (!File2Thumb.ContainsKey("*system"))
//			{
//				// System
//				imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/System.png"));
//				if (imgSrc.CanFreeze)
//					imgSrc.Freeze();
				
//				File2Thumb.Add("*system", imgSrc);
//			}
//			//}

//			if (!useStdThumbnail)
//			{
//				if (System.IO.File.Exists(ent.FullPath))
//					return FileToThumb(ent.FullPath, ent, useStdThumbnail);

//				else
//					// we need to obtain large size
//					return FileToThumb(path, ent, useStdThumbnail);
//			}

//			//
//			if (File2Thumb.ContainsKey(path))
//			{
//				imgSrc = File2Thumb[path];
//			}
//			else
//			{
//				try
//				{
//					imgSrc = FileToThumb(ent.FullPath, ent, useStdThumbnail);
//				}
//				catch (Exception ex)
//				{
//					Log.Exception(ex);
//				}
//				if (imgSrc != null)
//				{
//					if (imgSrc.CanFreeze)
//						imgSrc.Freeze();
				
//					File2Thumb.Add(path, imgSrc);
//				}
//				else
//				{
//					imgSrc = File2Thumb[string.Empty];
//				}
//			}
//			return imgSrc;
//		}

//		private void NotifyEntityThumbnailToUpdate(Entity sender, bool isLinkedItemNoLongerAvailable)
//		{
//			Platform.InvokeInUI(() =>
//				{
//					// we try to get or update thumbnail
//					if (isLinkedItemNoLongerAvailable == true)
//					{
//						ClearThumbnail(sender);
//					}
//					else if (isLinkedItemNoLongerAvailable == false)
//					{
//						// by doing this we ask system to re-obtain our icon
//						GetOrUpdateThumbnail(sender, true); // this is a system-wide thumbnail, we use a standard size (128x128)
//					}
//				});
//		}

//		internal static void ClearThumbnail(Entity sender)
//		{
//			// we should destroy the thumbnail
//			if (File2Thumb.ContainsKey(sender.Id))
//			{
//				// we don't set it to null, we remove it.
//				File2Thumb.Remove(sender.Id);
//			}
//		}

//		/// <summary>
//		/// Default method
//		/// </summary>
//		/// <param name="path"></param>
//		/// <param name="ent"></param>
//		/// <returns></returns>
//		public static System.Windows.Media.ImageSource FileToThumb(string path, Entity ent)
//		{
//			return FileToThumb(path, ent, true);
//		}

//		public static System.Windows.Media.ImageSource FileToThumb(string path, Entity ent, bool useStdThumbnail)
//		{
//			System.Windows.Media.ImageSource imgSrc = null;

//			#region Web Page
//			if (ent.Kind == Constants.Kinds.WebPage)
//			{
//				if (!string.IsNullOrEmpty(ent.ThumbnailUri))
//				{
//					var thumbnailUri = ent.ThumbnailUri.ToString();

//					string thumbnailPath =
//						System.IO.Path.Combine(ent.ModelContext.BaseFolder,
//						thumbnailUri);

//					if (System.IO.File.Exists(thumbnailPath))
//					{
//						System.Windows.Media.Imaging.BitmapImage bi
//					= new BitmapImage();
//						bi.BeginInit();
//						bi.UriSource = new Uri(thumbnailPath);

//						// we use std thumbnail size: 128x128
//						if (useStdThumbnail)
//						{
//							bi.DecodePixelWidth = 128;
//							bi.DecodePixelHeight = 128;
//							bi.CacheOption = BitmapCacheOption.OnLoad;
//						}
//						else
//						{
//							bi.DecodePixelWidth = 1024;
//							bi.DecodePixelHeight = 1024;
//							bi.CacheOption = BitmapCacheOption.OnLoad;
//						}
						
//						bi.EndInit();
//						bi.Freeze();

//						//int width = 0;

//						//if (bi.Width <= bi.Height)
//						//	width = (int)bi.Width;
//						//else
//						//	width = (int)bi.Height;

//						imgSrc = bi;
//						if (imgSrc.CanFreeze)
//							imgSrc.Freeze();
				
//						return imgSrc;
//					}
//				}

//				// we use default
//				// Web Page w/o Thumbnail
//				imgSrc = new BitmapImage(new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/Web.png"));
//				if (imgSrc.CanFreeze)
//					imgSrc.Freeze();
				
//				return imgSrc;
//				//File2Thumb.Add("*topic", imgSrc);
//			}
//			#endregion

//			#region Space
//			else if (ent.Kind == Constants.Kinds.ProjectSpace)
//			{
//				// using ThumbnailUri property
//				Microsoft.WindowsAPICodePack.Shell.ShellFile file = Microsoft.WindowsAPICodePack.Shell.ShellFile.FromFilePath(ent.ThumbnailUri);
//				//got file
//				if (file != null)
//				{
//					// obtaining thumbnail
//					imgSrc = file.Thumbnail.LargeBitmapSource;
//				}
//			}
//			#endregion

//			#region Everything Else

//			var kind = (from k in ent.ModelContext.SIM.AppEntityKindDefinitions
//						where k.Kind == ent.Kind
//						select k).FirstOrDefault();

//			if (kind != null)
//			{
//				if (System.IO.File.Exists(ent.ThumbnailUri) && (
//				System.IO.Path.GetExtension(ent.ThumbnailUri).Contains(".png")
//				||
//				System.IO.Path.GetExtension(ent.ThumbnailUri).Contains(".jpg"))
//				)
//				{
//					// using ThumbnailUri property
//					Microsoft.WindowsAPICodePack.Shell.ShellFile file = Microsoft.WindowsAPICodePack.Shell.ShellFile.FromFilePath(ent.ThumbnailUri);
//					//got file
//					if (file != null)
//					{
//						try
//						{
//							// obtaining thumbnail
//							imgSrc = file.Thumbnail.LargeBitmapSource;
//							if (imgSrc.CanFreeze)
//								imgSrc.Freeze();
				
//						}
//						catch (Exception ex)
//						{
//							Log.Exception(ex);
//						}
//					}
//				}
//				else
//				{
					
				
//				}
//			}


//			else
//			{
//				if (!string.IsNullOrEmpty(path))
//				{
//					var useOriginalFileThumbnail = false;

//					// now, interesting thing. We need to know if we are in file operation (in progress) or not
//					var foundFileCopyOperationStatusProperties = ent.Properties.Where
//						(p => p.Id == "IsFileCopyOperationInProgress").ToList();

//					#region Copied File
//					if (foundFileCopyOperationStatusProperties.Count > 0)
//					{
//						// in this case we should use the thumbnail of original file
//						var foundOriginalFileNameProperties = ent.Properties.Where
//						(p => p.Id == "OriginalFileName").ToList();

//						if (foundOriginalFileNameProperties.Count > 0)
//						{
//							var originalFileName = foundOriginalFileNameProperties.First().Value.ToString();

//							// we should use it's thumbnail
//							if (System.IO.File.Exists(originalFileName))
//							{
//								// ok, excellent
//								Microsoft.WindowsAPICodePack.Shell.ShellFile file =
//									Microsoft.WindowsAPICodePack.Shell.ShellFile.FromFilePath(originalFileName);
//								//got file
//								if (file != null)
//								{
//									try
//									{
//										// obtaining thumbnail from the original file
//										imgSrc = file.Thumbnail.LargeBitmapSource;

//										// we need to break
//										useOriginalFileThumbnail = true;
//									}
//									catch (Exception ex)
//									{
//										Log.Exception(ex);
//									}
//								}
//							}
//						}
//					}
//					#endregion
//					#region Normal or Linked File
//					if (!useOriginalFileThumbnail)// otherwise we should use the thumbnail of copied file
//					{
//						#region original file
//						if (System.IO.File.Exists(path))
//						{
//							var process = true;

//							if (System.IO.Path.GetExtension(path) == ".pdf")
//							{
//								if (!ent.ModelContext.SIM.ConfigurationSettings.ShowPDFThumbnails)
//								{
//									imgSrc = new BitmapImage(
//										new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/PDF.png"));
//									if (imgSrc.CanFreeze)
//										imgSrc.Freeze();
				
//									process = false;
//								}
//							}

//							if (process)
//							{
//								Microsoft.WindowsAPICodePack.Shell.ShellFile file =
//									Microsoft.WindowsAPICodePack.Shell.ShellFile.FromFilePath(path);
//								//got file
//								if (file != null)
//								{
//									try
//									{
//										if (useStdThumbnail)
//										{
//											// obtaining thumbnail; let's use Medium Bitmap Source by default
//											imgSrc = file.Thumbnail.MediumBitmapSource;
//											if (imgSrc.CanFreeze)
//												imgSrc.Freeze();
				
//										}
//										else
//										{
//											// we return large image source
//											imgSrc = file.Thumbnail.ExtraLargeBitmapSource;
//											if (imgSrc.CanFreeze)
//												imgSrc.Freeze();
				
//										}
//									}
//									catch (Exception ex)
//									{
//										Log.Exception(ex);
//									}
//								}
//							}
//						}

//						#endregion
//					}
//					#endregion
					
//					#region Old Code
//					//		Microsoft.WindowsAPICodePack.Shell.ShellFile file = Microsoft.WindowsAPICodePack.Shell.ShellFile.FromFilePath(path);
//					//		//got file
//					//		if (file != null)
//					//		{
//					//			// obtaining thumbnail
//					//			imgSrc = file.Thumbnail.LargeBitmapSource;
//					//		}
//					//	}
//					//	#endregion
//					//	else
//					//	{
//					//		//#region file exists?
//					//		//if (System.IO.Directory.Exists(path))
//					//		//{
//					//		//	// TODO: Space thumb
//					//		//	// ?!!!
//					//		//}
//					//		//#endregion
//					//		//#region file doesn't exist?
//					//		//else
//					//		//{
//					//			//var modelRoot = ZU.WPF.Frame.Self.Model.BaseFolder;
//					//			// obtaining via Owner property instead


//					//		//var modelRoot = ent.ModelContext.BaseFolder;

//					//			var absolutePath = path;

//					//			//var itemRelativePath = System.IO.Path.Combine(modelRoot, path);

//					//			if (System.IO.File.Exists(itemRelativePath))
//					//			{
//					//				var process = true;

//					//				if (System.IO.Path.GetExtension(itemRelativePath) == ".pdf")
//					//				{
//					//					if (!ent.ModelContext.SIM.ConfigurationSettings.ShowPDFThumbnails)
//					//					{
//					//						imgSrc = new BitmapImage(
//					//							new Uri(@"pack://application:,,,/ZU.WPF.Resources;component/Images/PDF.png"));

//					//						process = false;
//					//					}
//					//				}

//					//				if (process)
//					//				{
//					//					Microsoft.WindowsAPICodePack.Shell.ShellFile file =
//					//						Microsoft.WindowsAPICodePack.Shell.ShellFile.FromFilePath(itemRelativePath);
//					//					//got file
//					//					if (file != null)
//					//					{
//					//						// obtaining thumbnail
//					//						imgSrc = file.Thumbnail.LargeBitmapSource;
//					//					}
//					//				}
//					//			}
//					//			else
//					//			{
//					//				// Space thumb (relative to Frame.Self.RootFolder)
//					//				modelRoot = System.IO.Path.GetDirectoryName(ZU.WPF.Frame.RootFolder);
//					//				itemRelativePath = System.IO.Path.Combine(modelRoot, path);
//					//				//
//					//				if (System.IO.Directory.Exists(itemRelativePath))
//					//				{
//					//					// TODO: Space thumb
//					//				}
//					//			}

//					//		}
//					//		#endregion
//					//	}
//					//}
//					#endregion

//				}
//			}
//			#endregion

//			return imgSrc;
//		}


//		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
//		{
//			throw new NotImplementedException();
//		}

//		#endregion

//		internal static System.Windows.Media.ImageSource GetThumbnail(Entity entity, bool useStdThumbnail)
//		{
//			System.Windows.Media.ImageSource imgSrc = null;

//			Platform.InvokeInUI(() =>
//			{
//				imgSrc = GetOrUpdateThumbnail(entity, useStdThumbnail);
//			});

//			return imgSrc;
//		}
//	} // class
//} // namespace
