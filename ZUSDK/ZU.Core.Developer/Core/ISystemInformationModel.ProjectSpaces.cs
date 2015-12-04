using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZU.Core
{
	public partial interface ISystemInformationModel
	{
		#region Properties
		/// <summary>
		/// Current loaded model.
		/// </summary>
		IModel CurrentModel { get; set; }

		event OnProjectSpaceChangedEventHandler OnProjectSpaceChanged;
		event EventHandler OnAllProjectSpacesLoaded;
		/// <summary>
		/// List of all loaded model.
		/// </summary>
		System.Collections.ObjectModel.ObservableCollection<IModel> Models { get; set; }
		#endregion

		#region Methods
		IModel CreateAndLoadNewModel(IEntity ent);
		#endregion

		#region ModelConfiguration-related stuff
		///// <summary>
		///// Current loaded model's Configuration.
		///// </summary>
		//ModelConfiguration CurrentModelConfiguration { get; }
		//bool AddChildModelConfig(Entity space, out ModelConfiguration childModelConfig);
		//IModel LoadChildModel(ModelConfiguration childModelConfig);
		//int LoadChildModels();
		
		//bool RemoveChildModelConfig(ModelConfiguration childModelConfig);
		//bool UnloadChildModel(ModelConfiguration childModelConfig);
		void SetLastChangedModelAsCurrent();

		void UpdateCurrentModel(string newCurrentModelId);

		IEntity CreateSpaceEntity(string spaceTitle, string relativeName, ModelKind spaceKind, string spaceEntityId);
		#endregion

	} // interace
} // namespace
