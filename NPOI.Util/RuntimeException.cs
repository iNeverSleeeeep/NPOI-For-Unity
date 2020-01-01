using System;

namespace NPOI.Util
{
	[Serializable]
	public class RuntimeException : Exception
	{
		public RuntimeException()
		{
		}

		public RuntimeException(string message)
			: base(message)
		{
		}

		public RuntimeException(Exception e)
			: base("", e)
		{
		}

		public RuntimeException(string exception, Exception ex)
			: base(exception, ex)
		{
		}
	}
}
