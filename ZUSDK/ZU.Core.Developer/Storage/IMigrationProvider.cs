using ZU.Core;

namespace ZU.Storage.Migration
{
	public interface IMigrationProvider
	{
		void ReportProgress(int progress, MigrationPhase phase, IStorageProvider provider, string spaceName, ModelKind spaceKind, int errorCode);
	} // interface
} // namespace