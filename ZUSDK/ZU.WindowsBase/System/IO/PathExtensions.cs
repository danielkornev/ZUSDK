using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	public static class PathExtensions
	{
		public static string GetRelativePath(string fileOrFolder, string folderRelativeTo)
		{
			Uri pathUri = new Uri(fileOrFolder);
			// Folders must end in a slash
			if (!folderRelativeTo.EndsWith(Path.DirectorySeparatorChar.ToString()))
			{
				folderRelativeTo += Path.DirectorySeparatorChar;
			}
			Uri folderUri = new Uri(folderRelativeTo);
			return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
		}

		public static bool IsDirectory(string fileSystemPath)
		{
			// get the file attributes for file or directory
			FileAttributes attr = File.GetAttributes(fileSystemPath);

			//detect whether its a directory or file
			if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
				return true;
			else
				return false;
		}

		/// <summary>
		/// From: http://stackoverflow.com/a/22834989/3846671
		/// </summary>
		/// <param name="parentPath"></param>
		/// <param name="childPath"></param>
		/// <returns></returns>
		public static bool IsSubDirectoryOf(this string childPath, string parentPath)
		{
			if (string.IsNullOrEmpty(parentPath)) throw new ArgumentNullException();
			if (string.IsNullOrEmpty(childPath)) throw new ArgumentNullException();

			return childPath.IsSubPathOf(parentPath);

			//var parentUri = new Uri(parentPath);
			//var childUri = new Uri(childPath);
			//if (parentUri != childUri && parentUri.IsBaseOf(childUri))
			//{
			//	//dowork
			//	return true;
			//}
			//return false;
		}

		public static bool IsSubFileOf(this string candidate, string other)
		{
			var isChild = false;
			try
			{
				var candidateInfo = new DirectoryInfo(candidate);
				var otherInfo = new FileInfo(other);

				while (candidateInfo.Parent != null)
				{
					if (candidateInfo.Parent.FullName == otherInfo.FullName)
					{
						isChild = true;
						break;
					}
					else candidateInfo = candidateInfo.Parent;
				}
			}
			catch (Exception error)
			{
				var message = String.Format("Unable to check directory {0} and file {1}: {2}", candidate, other, error);
			}

			return isChild;
		}

		public static bool IsRenameOfOfficeFileToTmpFile(string oldFullPath, string newFullPath)
		{
			var originalFileExt = System.IO.Path.GetExtension(oldFullPath);
			var updatedFileExt = System.IO.Path.GetExtension(newFullPath);

			bool isFileOffice = false;

			bool isTmpFile = false;

			// let's start with Word, Excel, and PowerPoint
			if (IsOfficeFile(originalFileExt))
			{
				isFileOffice = true;
			}

			else if (originalFileExt == ".tmp")
			{
				isTmpFile = true;
			}

			if (isFileOffice)
			{
				if (updatedFileExt == ".tmp")
					return true;
			}

			else if (isTmpFile == true)
			{
				if (IsOfficeFile(updatedFileExt))
					return true;
			}

			return false;
		}

		public static bool IsOfficeFile(string originalFileExt)
		{
			return originalFileExt == ".doc" ||
							originalFileExt == ".docx" ||
							originalFileExt == ".xls" ||
							originalFileExt == ".xlsx" ||
							originalFileExt == ".ppt" ||
							originalFileExt == ".pptx";
		}

		/// <summary>
		/// Source: http://stackoverflow.com/questions/354477/method-to-determine-if-path-string-is-local-or-remote-machine
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool IsLocalPath(String path)
		{
			if (!PathIsUNC(path))
			{
				return !PathIsNetworkPath(path);
			}

			Uri uri = new Uri(path);
			return System.Net.Host.IsLocalHost(uri.Host); // Refer to David's answer
		}

		[DllImport("Shlwapi.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool PathIsNetworkPath(String pszPath);

		[DllImport("Shlwapi.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool PathIsUNC(String pszPath);
	} // class
} // namespace
