using ZU.Core.Apps;

private void RegisterKinds()
{
	// 1. Declare new AppEntityKindDefinition
	AppEntityKindDefinition commercialOrganization = new AppEntityKindDefinition();

	// 2. Set it's AppId to your plugin's Id
	commercialOrganization.AppId = this.Id;

	// 3. Set it's AppTitle to your Plugin's Title
	commercialOrganization.AppTitle = this.Title;

	// 4. Define it's Plural and Singular Display Names in lower case
	commercialOrganization.DisplayNamePlural = "commercial organizations";
	commercialOrganization.DisplayNameSingular = "commercial organization";

	// 5. Define Full-Text aliases that will be used to identify your Kind in search
	commercialOrganization.FullTextAliases = "Commercial Organization Organizations Business Org";

	// 6. Define the Namespace Name for your new Kind
	commercialOrganization.Kind = "OrganizationStructureAnalyzer.Kinds.Organizations.CommercialOrganization";

	// 7. Set the Parent Kind for your new Kind. If in doubt, use ZU.Constants.Kinds.Kind.
	commercialOrganization.ParentKind = ZU.Constants.Kinds.Organization;

	// 8. Decide if the Entities of your new Kind will participate in the Visual Clusters (and in the Topographic Clusters Visualization), or not. 
	commercialOrganization.CanBePartOfVisualCluster = true;
	commercialOrganization.CanParticipateInTopographicClusterVisualization = true;

	// 9. Advanced (Optional): set custom WPF Data Template for your Kind, 
	// if you find it required. Otherwise, the default template (FileBackingEntity) 
	// will be used.
	//commercialOrganization.Template = dt;

	// 10. Provide Zet Universe with the default Thumbnail used to visually represent 
	// your Kind.
	commercialOrganization.DefaultThumbnail = new BitmapImage(new Uri(@"pack://application:,,,/OrganizationStructureAnalyzer.ZApp;component/Images/CommercialOrganization.png"));

	// 11. Register your Kind using IAppManager.RegisterKind() API. Note that you will need 
	// to provide a callback for opening Entities with your new Kind.
	_appManager.RegisterKind(OpenCommercialOrganizationEntity, commercialOrganization);
}