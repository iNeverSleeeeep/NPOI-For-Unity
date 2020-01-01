using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	public class CommonObjectDataSubRecord : SubRecord
	{
		public const short sid = 21;

		private short field_1_objectType;

		private int field_2_objectId;

		private short field_3_option;

		private BitField locked = BitFieldFactory.GetInstance(1);

		private BitField printable = BitFieldFactory.GetInstance(16);

		private BitField autoFill = BitFieldFactory.GetInstance(8192);

		private BitField autoline = BitFieldFactory.GetInstance(16384);

		private int field_4_reserved1;

		private int field_5_reserved2;

		private int field_6_reserved3;

		/// Size of record (exluding 4 byte header)
		public override int DataSize => 18;

		public override short Sid => 21;

		/// Get the object type field for the CommonObjectData record.
		public CommonObjectType ObjectType
		{
			get
			{
				return (CommonObjectType)field_1_objectType;
			}
			set
			{
				field_1_objectType = (short)value;
			}
		}

		/// Get the object id field for the CommonObjectData record.
		public int ObjectId
		{
			get
			{
				return field_2_objectId;
			}
			set
			{
				field_2_objectId = value;
			}
		}

		/// Get the option field for the CommonObjectData record.
		public short Option
		{
			get
			{
				return field_3_option;
			}
			set
			{
				field_3_option = value;
			}
		}

		/// Get the reserved1 field for the CommonObjectData record.
		public int Reserved1
		{
			get
			{
				return field_4_reserved1;
			}
			set
			{
				field_4_reserved1 = value;
			}
		}

		/// Get the reserved2 field for the CommonObjectData record.
		public int Reserved2
		{
			get
			{
				return field_5_reserved2;
			}
			set
			{
				field_5_reserved2 = value;
			}
		}

		/// Get the reserved3 field for the CommonObjectData record.
		public int Reserved3
		{
			get
			{
				return field_6_reserved3;
			}
			set
			{
				field_6_reserved3 = value;
			}
		}

		/// true if object is locked when sheet has been protected
		/// @return  the locked field value.
		public bool IsLocked
		{
			get
			{
				return locked.IsSet(field_3_option);
			}
			set
			{
				field_3_option = locked.SetShortBoolean(field_3_option, value);
			}
		}

		/// object appears when printed
		/// @return  the printable field value.
		public bool IsPrintable
		{
			get
			{
				return printable.IsSet(field_3_option);
			}
			set
			{
				field_3_option = printable.SetShortBoolean(field_3_option, value);
			}
		}

		/// whether object uses an automatic Fill style
		/// @return  the autoFill field value.
		public bool IsAutoFill
		{
			get
			{
				return autoFill.IsSet(field_3_option);
			}
			set
			{
				field_3_option = autoFill.SetShortBoolean(field_3_option, value);
			}
		}

		/// whether object uses an automatic line style
		/// @return  the autoline field value.
		public bool IsAutoline
		{
			get
			{
				return autoline.IsSet(field_3_option);
			}
			set
			{
				field_3_option = autoline.SetShortBoolean(field_3_option, value);
			}
		}

		public CommonObjectDataSubRecord()
		{
		}

		/// Constructs a CommonObjectData record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public CommonObjectDataSubRecord(ILittleEndianInput in1, int size)
		{
			if (size != 18)
			{
				throw new RecordFormatException("Expected size 18 but got (" + size + ")");
			}
			field_1_objectType = in1.ReadShort();
			field_2_objectId = in1.ReadUShort();
			field_3_option = in1.ReadShort();
			field_4_reserved1 = in1.ReadInt();
			field_5_reserved2 = in1.ReadInt();
			field_6_reserved3 = in1.ReadInt();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ftCmo]\n");
			stringBuilder.Append("    .objectType           = ").Append("0x").Append(HexDump.ToHex((short)ObjectType))
				.Append(" (")
				.Append(ObjectType)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .objectId             = ").Append("0x").Append(HexDump.ToHex(ObjectId))
				.Append(" (")
				.Append(ObjectId)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .option               = ").Append("0x").Append(HexDump.ToHex(Option))
				.Append(" (")
				.Append(Option)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .locked                   = ").Append(IsLocked).Append('\n');
			stringBuilder.Append("         .printable                = ").Append(IsPrintable).Append('\n');
			stringBuilder.Append("         .autoFill                 = ").Append(IsAutoFill).Append('\n');
			stringBuilder.Append("         .autoline                 = ").Append(IsAutoline).Append('\n');
			stringBuilder.Append("    .reserved1            = ").Append("0x").Append(HexDump.ToHex(Reserved1))
				.Append(" (")
				.Append(Reserved1)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .reserved2            = ").Append("0x").Append(HexDump.ToHex(Reserved2))
				.Append(" (")
				.Append(Reserved2)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .reserved3            = ").Append("0x").Append(HexDump.ToHex(Reserved3))
				.Append(" (")
				.Append(Reserved3)
				.Append(" )");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("[/ftCmo]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(21);
			out1.WriteShort(DataSize);
			out1.WriteShort(field_1_objectType);
			out1.WriteShort(field_2_objectId);
			out1.WriteShort(field_3_option);
			out1.WriteInt(field_4_reserved1);
			out1.WriteInt(field_5_reserved2);
			out1.WriteInt(field_6_reserved3);
		}

		public override object Clone()
		{
			CommonObjectDataSubRecord commonObjectDataSubRecord = new CommonObjectDataSubRecord();
			commonObjectDataSubRecord.field_1_objectType = field_1_objectType;
			commonObjectDataSubRecord.field_2_objectId = field_2_objectId;
			commonObjectDataSubRecord.field_3_option = field_3_option;
			commonObjectDataSubRecord.field_4_reserved1 = field_4_reserved1;
			commonObjectDataSubRecord.field_5_reserved2 = field_5_reserved2;
			commonObjectDataSubRecord.field_6_reserved3 = field_6_reserved3;
			return commonObjectDataSubRecord;
		}
	}
}
