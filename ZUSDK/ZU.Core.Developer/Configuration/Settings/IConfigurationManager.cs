using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Configuration.Settings
{
	public interface IConfigurationManager
	{
		/// <summary>
		/// Required for the built-in Full-Text Extraction Processor
		/// </summary>
		/// <param name="fileFullPath"></param>
		/// <returns></returns>
		bool IsFileExtensionNonIndexable(string fileFullPath);
	} // interface
} // namespace
