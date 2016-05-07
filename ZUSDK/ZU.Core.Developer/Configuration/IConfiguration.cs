using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZU.Semantic;

namespace ZU.Core.Configuration
{
	/// <summary>
	/// This interface supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public interface IConfiguration
	{
		IModel Model
		{ get; set; }

		EntityRef UID
		{ get; set; }
	} // interface
} // namespace
