using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Win32
{
	/// <summary>
	/// PInvoke wrapper for CopyEx
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/aa363852.aspx
	/// 
	/// obtained from: http://stackoverflow.com/questions/187768/can-i-show-file-copy-progress-using-fileinfo-copyto-in-net
	/// </summary>
	public class XCopy
	{
		public static void Copy(string source, string destination, Guid entityId, bool overwrite, bool nobuffering, bool isMove)
		{
			new XCopy().CopyInternal(source, destination, null, entityId, string.Empty, overwrite, nobuffering, isMove, null, null);
		}

		public static void Copy(string source, string destination, object entity, Guid entityId, string appId, bool overwrite, bool nobuffering, bool isMove)
		{
			new XCopy().CopyInternal(source, destination, entity, entityId, appId, overwrite, nobuffering, isMove, null, null);
		}

		public static void Copy(string source, string destination, Guid entityId, bool overwrite, bool nobuffering, bool isMove, EventHandler<EntityCopyProgressChangedEventArgs> handler)
		{
			new XCopy().CopyInternal(source, destination, null, entityId, string.Empty, overwrite, nobuffering, isMove, null, handler);
		}

		public static void Copy(string source, string destination, object entity, Guid entityId, string appId, bool overwrite, bool nobuffering, bool isMove, EventHandler<EntityCopyProgressChangedEventArgs> handler)
		{
			new XCopy().CopyInternal(source, destination, entity, entityId, appId, overwrite, nobuffering, isMove, null, handler);
		}

		public static void Copy(string source, string destination, Guid entityId, bool overwrite, bool nobuffering, bool isMove, string originalTitle, EventHandler<EntityCopyProgressChangedEventArgs> handler)
		{
			new XCopy().CopyInternal(source, destination, null, entityId, string.Empty, overwrite, nobuffering, isMove, originalTitle, handler); 
		}

		public static void Copy(string source, string destination, object entity, Guid entityId, string appId, bool overwrite, bool nobuffering, bool isMove, string originalTitle, EventHandler<EntityCopyProgressChangedEventArgs> handler)
		{
			new XCopy().CopyInternal(source, destination, entity, entityId, appId, overwrite, nobuffering, isMove, originalTitle, handler);
		}

		//private event EventHandler Completed;
		private event EventHandler<EntityCopyProgressChangedEventArgs> ProgressChanged;

		private int IsCancelled;
		private int FilePercentCompleted;
		private string Source;
		private string Destination;

		private string _originalTitle;

		private bool _isMove;

		private object _entity;
		private Guid _entityId;
		private string _appId;

		private XCopy()
		{
			IsCancelled = 0;
		}

		private void CopyInternal(string source, string destination, object entity, Guid entityId, string appId, bool overwrite, bool nobuffering, bool isMove, string originalTitle, EventHandler<EntityCopyProgressChangedEventArgs> handler)
		{
			this._entityId = entityId;
			this._isMove = isMove;
			this._entity = entity;

			this._originalTitle = originalTitle;
			this._appId = appId;

			try
			{
				CopyFileFlags copyFileFlags = CopyFileFlags.COPY_FILE_RESTARTABLE;
				if (!overwrite)
					copyFileFlags |= CopyFileFlags.COPY_FILE_FAIL_IF_EXISTS;

				if (nobuffering)
					copyFileFlags |= CopyFileFlags.COPY_FILE_NO_BUFFERING;

				Source = source;
				Destination = destination;

				if (handler != null)
					ProgressChanged += handler;

				bool result = CopyFileEx(Source, Destination, new CopyProgressRoutine(CopyProgressHandler), IntPtr.Zero, ref IsCancelled, copyFileFlags);
				if (!result)
				{
					Trace.WriteLine("EXCEPTION: " +(new Win32Exception(Marshal.GetLastWin32Error())).Message);
				}
			}
			catch (Exception ex)
			{
				if (handler != null)
					ProgressChanged -= handler;

				throw ex;
			}
		}

		private void OnProgressChanged(double percent)
		{
			// only raise an event when progress has changed
			if ((int)percent > FilePercentCompleted)
			{
				FilePercentCompleted = (int)percent;

				var handler = ProgressChanged;
				if (handler != null)
					handler(this, new EntityCopyProgressChangedEventArgs 
					{
 						EntityId = _entityId,
						Entity = _entity,
						AppId = _appId,
						Completed = false,
						FilePerecentCompleted = (int)FilePercentCompleted
					});
						
						
						// ((int)FilePercentCompleted, null));
			}
		}

		private void OnCompleted()
		{
			if (_isMove)
				try
				{
					// we delete source, cause it was a "Move" operation 
					System.IO.File.Delete(Source);
				}
				catch (Exception ex)
				{
					Trace.WriteLine("EXCEPTION: " + ex.Message);
				}

			var args = new EntityCopyProgressChangedEventArgs
				{
					EntityId = _entityId,
					Entity = _entity,
					AppId = _appId,
					Completed = true,
					FilePerecentCompleted = 100,
					FileDestination = Destination,
					Succeeded = true
				};

			// we set title of final entity as "Copy of 'original entity's name'"
			if (!string.IsNullOrEmpty(_originalTitle))
				args.OriginalTitle = "Copy of " + _originalTitle;

			var handler = ProgressChanged;
			if (handler != null)
				handler(this, args);

			//var handler = Completed;
			//if (handler != null)
			//	handler(this, EventArgs.Empty);
		}

		#region PInvoke

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName, CopyProgressRoutine lpProgressRoutine, IntPtr lpData, ref Int32 pbCancel, CopyFileFlags dwCopyFlags);

		private delegate CopyProgressResult CopyProgressRoutine(long TotalFileSize, long TotalBytesTransferred, long StreamSize, long StreamBytesTransferred, uint dwStreamNumber, CopyProgressCallbackReason dwCallbackReason,
														IntPtr hSourceFile, IntPtr hDestinationFile, IntPtr lpData);

		private enum CopyProgressResult : uint
		{
			PROGRESS_CONTINUE = 0,
			PROGRESS_CANCEL = 1,
			PROGRESS_STOP = 2,
			PROGRESS_QUIET = 3
		}

		private enum CopyProgressCallbackReason : uint
		{
			CALLBACK_CHUNK_FINISHED = 0x00000000,
			CALLBACK_STREAM_SWITCH = 0x00000001
		}

		[Flags]
		private enum CopyFileFlags : uint
		{
			COPY_FILE_FAIL_IF_EXISTS = 0x00000001,
			COPY_FILE_NO_BUFFERING = 0x00001000,
			COPY_FILE_RESTARTABLE = 0x00000002,
			COPY_FILE_OPEN_SOURCE_FOR_WRITE = 0x00000004,
			COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 0x00000008
		}

		private CopyProgressResult CopyProgressHandler(long total, long transferred, long streamSize, long streamByteTrans, uint dwStreamNumber,
													   CopyProgressCallbackReason reason, IntPtr hSourceFile, IntPtr hDestinationFile, IntPtr lpData)
		{
			if (reason == CopyProgressCallbackReason.CALLBACK_CHUNK_FINISHED)
				OnProgressChanged((transferred / (double)total) * 100.0);

			if (transferred >= total)
				OnCompleted();

			return CopyProgressResult.PROGRESS_CONTINUE;
		}

		#endregion


		
	} // end of class

	public class EntityCopyProgressChangedEventArgs : System.EventArgs
	{
		public Guid EntityId
		{
			get;
			set;
		}

		public object Entity
		{
			get;
			set;
		}

		public string AppId
		{
			get;
			set;
		}

		public int FilePerecentCompleted
		{
			get;
			set;
		}

		public bool Completed
		{
			get;
			set;
		}

		public bool Succeeded
		{
			get;
			set;
		}

		public string OriginalTitle
		{
			get;
			set;
		}

		public string FileDestination
		{ get; set; }
	}


} // end of namespace
