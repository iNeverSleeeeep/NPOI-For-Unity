using System;

namespace NPOI.POIFS.FileSystem
{
	[Serializable]
	public class Ole10NativeException : Exception
	{
		public Ole10NativeException(string message)
			: base(message)
		{
		}
	}
}
