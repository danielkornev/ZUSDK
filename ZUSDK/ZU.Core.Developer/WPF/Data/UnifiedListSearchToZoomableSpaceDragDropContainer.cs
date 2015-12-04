using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZU.Semantic;

namespace ZU.WPF.Data
{
	public class UnifiedListSearchToZoomableSpaceDragDropContainer
	{
		/// <summary>
		/// Each EntityRef contains two things: Entity Id and Model Id. This is already good enough for drag-n-drop. The good question is whether we should define one action for all items, or rather we should define it per each item, or should we infer it from each item's source?
		/// </summary>
		public List<EntityRef> EntityReferences { get; set; }

		/// <summary>
		/// Required for figuring out what should be done with the given entities
		/// </summary>
		public DataAction Action { get; set; }

		/// <summary>
		/// Required for obtaining the location in the zoomable space
		/// </summary>
		public InputType InputType { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public UnifiedListSearchToZoomableSpaceDragDropContainer()
		{
			this.EntityReferences = new List<EntityRef>();
		}
	} // class
} // namespace
