using SAL.Flatbed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZU
{
	// Contains ITraceProvider implementation details
	partial class Log : ITraceProvider
	{
		public static ITraceProvider Instance { get; internal set; } 

		public void Add(object source, Exception exc)
		{
			if (!Log.IsFatal(exc))
				Log.Exception("An exception happened with \"{0}\"", exc, source);
		}

		public void Add(EventType type, object source, string message)
		{
			switch (type)
			{
				case EventType.Log:
					Log.Debug(message + ": \"{0}\"", source);
					break;
				case EventType.Status:
					Log.Info(message + ": \"{0}\"", source);
					break;
				case EventType.Notify:
					Log.Notify(message + ": \"{0}\"", source);
					break;
				default:
					Log.Info(message + ": \"{0}\"", source);
					break;
			}
		}

		/// <summary>Фатальная ошибка, которую обрабатывать не надо</summary>
		/// <param name="exception">Ошибка для проверки</param>
		/// <returns>Ошибка фатальная и обрабатывать нет смысла</returns>
		public static Boolean IsFatal(Exception exception)
		{
			while (exception != null)
			{
				if ((exception is OutOfMemoryException && !(exception is InsufficientMemoryException))//Нет смысла занимать больше памяти
					|| exception is ThreadAbortException//Ошибка происходит при редиректе с одной страницы на другую
					|| exception is AccessViolationException
					|| exception is SEHException)
					return true;
				if (!(exception is TypeInitializationException) && !(exception is TargetInvocationException))
					break;
				exception = exception.InnerException;
			}
			return false;
		}
	} // class
} // namespace
