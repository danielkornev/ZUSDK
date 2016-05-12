using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAL.Flatbed;
using ZU.Semantic.Processors;
using ZU.Plugins;
using ZU.Core;

namespace ZU.Samples.Processors.BasicProcessorSample
{
	public partial class BasicProcessor : Plugins.IZetProcessor
	{
		#region Members
		private ISemanticPipelineProcessor processor;
		internal IZetHost ZetHost;

		internal bool subscribed;
		private string subscribedTopic;
		private string destinationTopic;
		#endregion

		public IHost Host
		{
			get;set;
		}

		public string Name
		{
			get
			{
				// TO DO: put your processor name here
				return "Basic Zet Universe Processor";
			}

			set
			{
				// will be not required in subsequent releases of the ZU.Core.Developer package
			}
		}

		public string Status
		{
			get
			{
				if (subscribed == false)
					return "Disabled";

				// TO DO: implement your own status check here
				return "Idle";
			}
		}

		public object Invoke(string message, params object[] args)
		{
			return null;
		}

		public bool OnConnection(ConnectMode mode)
		{
			if (this.Host == null) throw new NotImplementedException("Host is not available");

			this.ZetHost = this.Host as IZetHost;

			if (this.ZetHost == null) throw new PlatformNotSupportedException("This build of Zet Universe platform is not supported. Host doesn't implement ZU.Plugins.IZetHost interface");

			// obtaining ISemanticPipelineProcessor
			var processor = this.ZetHost.SIM.SemanticPipelineProcessor;

			if (processor == null) throw new PlatformNotSupportedException("This build of Zet Universe platform is not supported. Host doesn't provide access to a functioning Semantic Processor");

			// Cache the interface for later use
			this.processor = processor;

			// Register your Processor with the Semantic Pipeline Processor:
			this.processor.RegisterProcessor(this);

			// Topic(s) of interest (either an existing one, in ZU.Constants.Topics.*, or to a custom one)
			this.subscribedTopic = Constants.Topics.Classifications.Kinds;

			// Optional: Define the destination topic where the successfully processed entity will be published
			this.destinationTopic = "/BasicProcessorSample/Processed/";

			// Subscribe to the topic of interest 
			subscribed = processor.Subscribe(new OnPublishedDelegate(OnEntityKindIdentified), subscribedTopic, this.Name);

			return true;
		}

		private void OnEntityKindIdentified(IEntity entity)
		{
			// Optional: if the piece of an IEntity that is expected to be set and is needed for your plugin 
			// is not set, use ISemanticPipelineProcessor.ReportFailedProcessing() to report a failure and end processing.
			//if (string.IsNullOrEmpty(fullText))
			//{
			//	processor.ReportFailedProcessing(entity, topic);
			//  return;
			//}

			bool succeeded = ProcessEntity(entity);

			if (succeeded)
			{
				// do what's needed with the IEntity here (e.g., add/change property, or add a relationship, etc.)

				// 
				Platform.InvokeInUI(() =>
				{
					// use Platform.InvokeInUI() to make changes to IEntity properties in a proper way

					
				});

				// Optional: publishing your IEntity back to the system into your new topic
				//processor.Publish(entity, this.destinationTopic);
				processor.ReportSuccessfulProcessing(entity, this.subscribedTopic);
			}
			else
			{
				// he's dead, Jim
				processor.ReportFailedProcessing(entity, this.subscribedTopic);
			} 
		}

		private bool ProcessEntity(IEntity entity)
		{
			// TO DO: implement your entity processing here
			return false;
		}

		public bool OnDisconnection(DisconnectMode mode)
		{
			this.Shutdown();
			return true;
		}

		public void Shutdown()
		{
			// TO DO: add anything required to properly shutdown your plugin if necessary
		}
	} // class
} // namespace
