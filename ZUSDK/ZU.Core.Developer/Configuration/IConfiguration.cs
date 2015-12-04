using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZU.Semantic;

namespace ZU.Core.Configuration
{
	public interface IConfiguration
	{
		IModel Model
		{ get; set; }

		EntityRef UID
		{ get; set; }
	} // interface
} // namespace
