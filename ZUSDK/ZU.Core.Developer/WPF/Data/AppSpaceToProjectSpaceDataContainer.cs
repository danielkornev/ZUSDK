using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.WPF.Data
{
	/// <summary>
	/// This class supports Zet Universe and is not intended to be used directly from your code.
	/// </summary>
	public partial class AppSpaceItemToProjectSpaceDataContainer
	{
		public string EntityId { get; internal set; }
		public string EntityKind { get; internal set; }
		public Guid AppId { get; internal set; }
		public bool IsContainer { get; internal set; }

		public AppSpaceItemToProjectSpaceDataContainer(string entityId, string entityKind, Guid appId, bool isContainer)
		{
			this.AppId = appId;
			this.EntityKind = entityKind;
			this.EntityId = entityId;
			this.IsContainer = IsContainer;
		}

	} // class
} // namespace
