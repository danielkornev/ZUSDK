using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZU.Core;
using ZU.Telemetry;

namespace ZU.WPF
{
	public interface IFrame
	{ 
		int SelectedIndex { get; }
		List<IEntity> SelectedEntities { get; }

		void Telemetry(Exception exception);

		// 
		void Telemetry(OperationKind kind, string text = "");
	} // interface
} // namespace
