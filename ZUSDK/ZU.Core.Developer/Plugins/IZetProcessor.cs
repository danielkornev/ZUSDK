using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Semantic.Processors;

namespace ZU.Plugins
{
	public interface IZetProcessor : SAL.Flatbed.IPlugin
	{
		string Name { get; }
		//void Initialize(ISemanticPipelineProcessor processor);
		string Status { get; }
		void Shutdown();
	} // interface
} // namespace
