using System;

namespace ZU.Core
{
	public interface IService
	{
		bool IsStarted
		{
			get;
		}
		void Start();
		void Stop();
	}//interface IService
}//namespace
