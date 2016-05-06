using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Core;

namespace ZU.Plugins
{
	public class FullTextExtractionResult
	{
		public string FullText { get; set; }
		public bool Succeeded { get; set; }
	}

	public interface IFullTextSearchExtractionProcessor
	{
		Task<FullTextExtractionResult> TryExtract(IEntity entity);
		bool CanExtractFromKind(string kind, string fileExt = "");
	} // interface
} // namespace
