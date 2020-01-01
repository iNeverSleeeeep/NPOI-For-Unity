using NPOI.Util;
using System;

namespace NPOI.HSSF.Record
{
	[Serializable]
	public class LeftoverDataException : Exception
	{
		public LeftoverDataException(int sid, int remainingByteCount)
			: base("Initialisation of record 0x" + StringUtil.ToHexString(sid).ToUpper() + " left " + remainingByteCount + " bytes remaining still to be read.")
		{
		}
	}
}
