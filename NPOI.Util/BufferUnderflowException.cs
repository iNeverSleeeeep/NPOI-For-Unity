using System;

namespace NPOI.Util
{
	[Serializable]
	public class BufferUnderflowException : RuntimeException
	{
		public BufferUnderflowException()
			: base("Buffer Underflow")
		{
		}
	}
}
