using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZU
{
	public static class Utils
	{

		public static string CleanFolderName(string folderName)
		{
			string file = folderName;
			file = string.Concat(file.Split(System.IO.Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));

			if (file.Length > 250)
			{
				file = file.Substring(0, 250);
			}
			return file;
		}

		private static string ToShortName(string folderName, int number)
		{
			string shortName = string.Empty;

			if (folderName.Count() <= 40)
			{
				shortName = folderName;
			}
			else
			{
				shortName = new String(folderName.Take(40).ToArray());
			}

			return shortName;
		}

		private static string ToValidSpaceName(List<string> existingFolderNames, string spaceName)
		{
			string result = string.Empty;

			// 1. We need to clean space name from invalid symbols
			var r1 = CleanFolderName(spaceName);

			// 2. We need to obtain short name
			var r2 = ToShortName(r1);

			// 3. We need to be sure we don't have folder with the same name
			var r3 = FindFirstValidName(existingFolderNames, r2);

			// 4. result
			result = r3;

			// 5. done
			return result;
		}

		public static string FindFirstValidName(List<string> existingNames, string desiredName)
		{
			return FindFirstValidName(existingNames, desiredName, desiredName, 0);
		}

		private static string FindFirstValidName(List<string> existingNames, string originalDesiredName, string proposedName, int count)
		{
			var result = string.Empty;

			var existing = existingNames.Where(f => f.ToLower() == proposedName.ToLower()).ToList();

			if (existing.Count > 0)
			{
				var take = 40 - 2;

				var count1 = count + 1;

				if (count1 < 9)
					take = 40 - 2;
				else if (count > 9 & count1 < 99)
					take = 40 - 3;
				else if (count > 99 & count1 < 999)
					take = 40 - 4;

				var part1 = new string(originalDesiredName.Take(take).ToArray()).TrimEnd(' ');

				var dName1 = part1 + " " + count1.ToString();

				var desiredName = dName1;

				var r = FindFirstValidName(existingNames, originalDesiredName, desiredName, count1);

				result = r;
			}
			else
			{
				result = proposedName;
			}

			return result;
		}

		/// <summary>
		/// By default, we provide a name with 40 symbols, no more.
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		private static string ToShortName(string folderName)
		{
			return ToShortName(folderName, 40);
		}
	}
}
