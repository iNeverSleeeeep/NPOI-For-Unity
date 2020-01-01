using System;

namespace NPOI
{
	[Serializable]
	public class OldFileFormatException : Exception
	{
		public OldFileFormatException(string s)
			: base(s)
		{
		}
	}
}
