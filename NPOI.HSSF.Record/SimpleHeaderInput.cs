using NPOI.Util;
using System.IO;

namespace NPOI.HSSF.Record
{
	internal class SimpleHeaderInput : BiffHeaderInput
	{
		private ILittleEndianInput _lei;

		internal static ILittleEndianInput GetLEI(Stream in1)
		{
			if (in1 is ILittleEndianInput)
			{
				return (ILittleEndianInput)in1;
			}
			return new LittleEndianInputStream(in1);
		}

		public SimpleHeaderInput(Stream in1)
		{
			_lei = GetLEI(in1);
		}

		public int Available()
		{
			return _lei.Available();
		}

		public int ReadDataSize()
		{
			return _lei.ReadUShort();
		}

		public int ReadRecordSID()
		{
			return _lei.ReadUShort();
		}
	}
}
