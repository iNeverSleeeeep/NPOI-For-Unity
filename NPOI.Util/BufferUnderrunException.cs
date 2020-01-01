using System;
using System.IO;

namespace NPOI.Util
{
	[Serializable]
	public class BufferUnderrunException : IOException
	{
		internal BufferUnderrunException()
			: base("buffer underrun")
		{
		}
	}
}
