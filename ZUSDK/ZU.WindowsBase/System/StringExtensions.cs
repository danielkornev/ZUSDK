using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace System
{
	public static class StringExtensions
	{
		public static string ToFile(this string HTML)
		{
			string path = System.IO.Path.GetTempFileName();
			path = path + ".html";

			// writing to file
			System.IO.File.WriteAllText(path, HTML);

			return path;
		}

		/// <summary>
		/// Split that takes a function that has to decide whether the specified character should split the string:
		/// http://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
		/// </summary>
		/// <param name="str"></param>
		/// <param name="controller"></param>
		/// <returns></returns>
		public static IEnumerable<string> Split(this string str,
											Func<char, bool> controller)
		{
			int nextPiece = 0;

			for (int c = 0; c < str.Length; c++)
			{
				if (controller(str[c]))
				{
					yield return str.Substring(nextPiece, c - nextPiece);
					nextPiece = c + 1;
				}
			}

			yield return str.Substring(nextPiece);
		}

		public static IEnumerable<string> SplitCommandLine(this string commandLine)
		{
			bool inQuotes = false;

			return commandLine.Split(c =>
			{
				if (c == '\"')
					inQuotes = !inQuotes;

				return !inQuotes && c == ' ';
			})
							  .Select(arg => arg.Trim().TrimMatchingQuotes('\"'))
							  .Where(arg => !string.IsNullOrEmpty(arg));
		}

		public static string TrimMatchingQuotes(this string input, char quote)
		{
			if ((input.Length >= 2) &&
				(input[0] == quote) && (input[input.Length - 1] == quote))
				return input.Substring(1, input.Length - 2);

			return input;
		}

		public static string UppercaseFirst(this string input)
		{
			// Check for empty string.
			if (string.IsNullOrEmpty(input))
			{
				return string.Empty;
			}
			// Return char and concat substring.
			return char.ToUpper(input[0]) + input.Substring(1);
		}

		/// <summary>
		/// Returns true if <paramref name="path"/> starts with the path <paramref name="baseDirPath"/>.
		/// The comparison is case-insensitive, handles / and \ slashes as folder separators and
		/// only matches if the base dir folder name is matched exactly ("c:\foobar\file.txt" is not a sub path of "c:\foo").
		/// </summary>
		public static bool IsSubPathOf(this string path, string baseDirPath)
		{
			string normalizedPath = Path.GetFullPath(path.Replace('/', '\\')
				.WithEnding("\\"));

			string normalizedBaseDirPath = Path.GetFullPath(baseDirPath.Replace('/', '\\')
				.WithEnding("\\"));

			return normalizedPath.StartsWith(normalizedBaseDirPath, StringComparison.OrdinalIgnoreCase);
		}

		public static bool IsParentPathOf(this string baseDirPath, string path)
		{
			string normalizedPath = Path.GetFullPath(path.Replace('/', '\\')
				.WithEnding("\\"));

			string normalizedBaseDirPath = Path.GetFullPath(baseDirPath.Replace('/', '\\')
				.WithEnding("\\"));

			return normalizedPath.StartsWith(normalizedBaseDirPath, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Returns <paramref name="str"/> with the minimal concatenation of <paramref name="ending"/> (starting from end) that
		/// results in satisfying .EndsWith(ending).
		/// </summary>
		/// <example>"hel".WithEnding("llo") returns "hello", which is the result of "hel" + "lo".</example>
		public static string WithEnding(this string str, string ending)
		{
			if (str == null)
				return ending;

			string result = str;

			// Right() is 1-indexed, so include these cases
			// * Append no characters
			// * Append up to N characters, where N is ending length
			for (int i = 0; i <= ending.Length; i++)
			{
				string tmp = result + ending.Right(i);
				if (tmp.EndsWith(ending))
					return tmp;
			}

			return result;
		}

		/// <summary>Gets the rightmost <paramref name="length" /> characters from a string.</summary>
		/// <param name="value">The string to retrieve the substring from.</param>
		/// <param name="length">The number of characters to retrieve.</param>
		/// <returns>The substring.</returns>
		public static string Right(this string value, int length)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "Length is less than zero");
			}

			return (length < value.Length) ? value.Substring(value.Length - length) : value;
		}
	} // class
} // namespace
