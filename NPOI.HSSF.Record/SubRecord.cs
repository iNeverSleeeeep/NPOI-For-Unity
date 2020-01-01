using NPOI.Util;
using System;
using System.IO;

namespace NPOI.HSSF.Record
{
	/// Subrecords are part of the OBJ class.
	public abstract class SubRecord : ICloneable
	{
		public abstract short Sid
		{
			get;
		}

		public abstract int DataSize
		{
			get;
		}

		/// Wether this record terminates the sub-record stream.
		/// There are two cases when this method must be overridden and return <c>true</c>
		///  - EndSubRecord (sid = 0x00)
		///  - LbsDataSubRecord (sid = 0x12)
		///
		/// @return whether this record is the last in the sub-record stream
		public virtual bool IsTerminating => false;

		public static SubRecord CreateSubRecord(ILittleEndianInput in1, CommonObjectType cmoOt)
		{
			int num = in1.ReadUShort();
			int num2 = in1.ReadUShort();
			switch (num)
			{
			case 21:
				return new CommonObjectDataSubRecord(in1, num2);
			case 9:
				return new EmbeddedObjectRefSubRecord(in1, num2);
			case 6:
				return new GroupMarkerSubRecord(in1, num2);
			case 0:
				return new EndSubRecord(in1, num2);
			case 13:
				return new NoteStructureSubRecord(in1, num2);
			case 19:
				return new LbsDataSubRecord(in1, num2, (int)cmoOt);
			case 12:
				return new FtCblsSubRecord(in1, num2);
			default:
				return new UnknownSubRecord(in1, num, num2);
			}
		}

		public abstract void Serialize(ILittleEndianOutput out1);

		public byte[] Serialize()
		{
			int num = DataSize + 4;
			using (MemoryStream memoryStream = new MemoryStream(num))
			{
				Serialize(new LittleEndianOutputStream(memoryStream));
				if (memoryStream.Length != num)
				{
					throw new Exception("write size mismatch");
				}
				return memoryStream.ToArray();
			}
		}

		public abstract object Clone();
	}
}
