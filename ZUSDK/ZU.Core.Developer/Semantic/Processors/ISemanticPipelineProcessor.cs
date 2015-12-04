using System;
using System.Collections.Generic;
using ZU.Core;
namespace ZU.Semantic.Processors
{
	public interface ISemanticPipelineProcessor
	{
		void AddTopic(string topic);
		void AddTopic(string topic, string id);
		void Shutdown();

		ISystemInformationModel SIM { get; set; }

		bool Subscribe(OnPublishedDelegate onPublishedDelegate, string topic, string subscriberName);

		void Publish(IEntity entity, string topic);

		void ReportFailedProcessing(IEntity entity, string topic);

		void ReportSuccessfulProcessing(IEntity Entity, string topic);

		Dictionary<string, IInformationProcessor> Processors { get; }
		void Pause();
		void Continue();
		bool IsPaused { get; }
	} // interface
} // namespace
