using ZU.Core.Apps;

private void RegisterRelationships()
{
	// 1. Declare new AppRelationshipDefinition
	AppRelationshipDefinition organized = new AppRelationshipDefinition();

	// 2. Set it's AppId to your plugin's Id
	organized.AppId = this.Id.ToString();

	// 3. Define Description From
	organized.DescriptionFrom = "organized";

	// 4. Define Description
	organized.Description = "organized by";
	
	// 5. Define the Relation Namespace Name for your new Relationship
	organized.Relation = "Organized";

	// 6. Add pairs of participating Kinds
	organized.AllowedParticipatingKinds.Add(new Tuple<string, string>("OrganizationStructureAnalyzer.Kinds.Organizations.CommercialOrganization", ZU.Constants.Kinds.Meeting));

	// 7. Register your Relationship using IAppManager.RegisterRelationship() API.
	_appManager.RegisterRelationship(organized);
}