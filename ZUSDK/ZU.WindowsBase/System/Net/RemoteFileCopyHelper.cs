using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;
using System.IO;

namespace System.Net
{
	public partial class RemoteXCopy
	{
		public static void Download(string remoteFile, string localFile, Guid entityId, EventHandler<EntityCopyProgressChangedEventArgs> handler)
		{
			new RemoteXCopy().DownloadInternal(remoteFile, localFile, entityId, handler);
		}
	
		//string _remoteFile;
		private Guid _entityId;

		//private event EventHandler<EntityCopyProgressChangedEventArgs> ProgressChanged;

		internal void DownloadInternal(string remoteFile, string localFile, Guid entityId, EventHandler<EntityCopyProgressChangedEventArgs> handler)
		{
			_entityId = entityId;

			//WebClient webClient = new WebClient();
			try
			{
				//webClient.DownloadFile(remoteFile, localFile);

				//remoteFile = 

				var invalidChars = Path.GetInvalidFileNameChars();

				//var invalidCharsRemoved = remoteFile
				//.Where(x => !invalidChars.Contains(x))
				//.ToArray();

				//remoteFile = new string(invalidCharsRemoved);

				remoteFile = remoteFile.TrimEnd(' ');

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(remoteFile);

				// execute the request
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
				{

					// we will read data via the response stream
					Stream ReceiveStream = response.GetResponseStream();

					byte[] buffer = new byte[1024];
					FileStream outFile = new FileStream(localFile, FileMode.Create);

					int bytesRead;
					while ((bytesRead = ReceiveStream.Read(buffer, 0, buffer.Length)) != 0)
						outFile.Write(buffer, 0, bytesRead);

					// closing
					outFile.Close();

					// done downloading

					if (handler != null)
						handler(this, new EntityCopyProgressChangedEventArgs
						{
							EntityId = _entityId,
							Completed = true,
							FilePerecentCompleted = 100,
							FileDestination = localFile
						});
				}

				request.Abort();
					
			}
			catch
			{
				// nothing
				if (handler != null)
					handler(this, new EntityCopyProgressChangedEventArgs
					{
						EntityId = _entityId,
						Completed = true,
						FilePerecentCompleted = 0,
						FileDestination = null
					});
			}
		}
	} // class
} // namespace
