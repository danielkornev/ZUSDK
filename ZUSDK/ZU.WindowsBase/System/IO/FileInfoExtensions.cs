using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace System.IO
{
	public static class FileInfoExtensions
	{
		/// <summary>
		/// Computes MD5 Hash for a given file.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static string ComputeMD5Hash(this System.IO.FileInfo file)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(file.FullName))
				{
					return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
				}
			}
		}

		/// <summary>
		/// From: http://stackoverflow.com/questions/1177607/what-is-the-fastest-way-to-create-a-checksum-for-large-files-in-c-sharp
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static bool TryComputeSHAChecksum(this System.IO.FileInfo file, out string hash)
		{
			var filePath = file.FullName;

			hash = string.Empty;

			try
			{
				// Not sure if BufferedStream should be wrapped in using block
				using (var stream = new BufferedStream(File.OpenRead(filePath), 1200000))
				{
					SHA256Managed sha = new SHA256Managed();
					byte[] checksum = sha.ComputeHash(stream);
					hash = BitConverter.ToString(checksum).Replace("-", String.Empty);

					return true;
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public static bool TryFindMovedFile(string fileHash, long fileSize, string originalPath, out string newFullPath)
		{
			// by default, it will fail
			newFullPath = string.Empty;

			try
			{
				// 0. find top folder (by default, we assume that user could move file within the folder)
				var parentFolder = System.IO.Directory.GetParent(originalPath);

				var allFiles = parentFolder.GetFiles();


				foreach (var file in allFiles)
				{
					if (file.Length != fileSize)
						continue;


					var foundFileHash = string.Empty;

					if (file.TryComputeSHAChecksum(out foundFileHash))
					{
						if (fileHash == foundFileHash)
						{
							newFullPath = file.FullName;
							return true;
						}
					}
					// TO DO: CHECK IF ITS FINE
				}
			}

			catch (Exception ex)
			{
				Trace.WriteLine("EXCEPTION: Failed to find moved file for the one that was known as \"" + originalPath + "\":" + ex.Message);
			}

			return false;
		}
	} // class
} // namespace
