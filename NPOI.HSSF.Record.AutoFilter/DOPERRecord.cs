using NPOI.Util;
using System;

namespace NPOI.HSSF.Record.AutoFilter
{
	/// <summary>
	/// DOPER Structure for AutoFilter record
	/// </summary>
	/// <remarks>author: Tony Qu</remarks>
	public class DOPERRecord : RecordBase
	{
		private DOPERType vt;

		private byte grbitSgn;

		private RKRecord _RK;

		private double _IEEENumber;

		private byte CCH;

		private byte fError;

		private byte bBoolErr;

		public override int RecordSize => 10;

		public DOPERType DataType
		{
			get
			{
				return vt;
			}
			set
			{
				vt = value;
			}
		}

		public DOPERComparisonCode ComparisonCode
		{
			get
			{
				return (DOPERComparisonCode)grbitSgn;
			}
			set
			{
				grbitSgn = (byte)value;
			}
		}

		public double IEEENumber
		{
			get
			{
				return _IEEENumber;
			}
			set
			{
				_IEEENumber = value;
				vt = DOPERType.IEEENumber;
			}
		}

		/// <summary>
		/// get or set the RK record
		/// </summary>
		public RKRecord RK
		{
			get
			{
				return _RK;
			}
			set
			{
				_RK = value;
				vt = DOPERType.RKNumber;
			}
		}

		/// <summary>
		/// Gets or sets Length of the string (the string is stored in the rgch field that follows the DOPER structures)
		/// </summary>
		public byte LengthOfString
		{
			get
			{
				return CCH;
			}
			set
			{
				if (value > 252)
				{
					throw new ArgumentOutOfRangeException("The length of string must be less than or equal to 252");
				}
				CCH = value;
				vt = DOPERType.String;
			}
		}

		/// <summary>
		/// Whether the bBoolErr field contains a Boolean value
		/// </summary>
		public bool IsBooleanValue => fError == 0;

		/// <summary>
		/// Whether the bBoolErr field contains a Error value
		/// </summary>
		public bool IsErrorValue => fError == 1;

		/// <summary>
		/// Get or sets the boolean value
		/// </summary>
		public bool BooleanValue
		{
			get
			{
				return bBoolErr == 1;
			}
			set
			{
				bBoolErr = (byte)(value ? 1 : 0);
				fError = 0;
				vt = DOPERType.BooleanOrErrors;
			}
		}

		/// <summary>
		/// Get or sets the boolean value
		/// </summary>
		public DOPERErrorValue ErrorValue
		{
			get
			{
				return (DOPERErrorValue)bBoolErr;
			}
			set
			{
				bBoolErr = (byte)value;
				fError = 1;
				vt = DOPERType.BooleanOrErrors;
			}
		}

		public DOPERRecord()
		{
		}

		public DOPERRecord(RecordInputStream in1)
		{
			vt = (DOPERType)in1.ReadByte();
			switch (vt)
			{
			case DOPERType.RKNumber:
				grbitSgn = (byte)in1.ReadByte();
				_RK = new RKRecord(in1);
				in1.ReadInt();
				break;
			case DOPERType.IEEENumber:
				grbitSgn = (byte)in1.ReadByte();
				_IEEENumber = in1.ReadDouble();
				break;
			case DOPERType.String:
				grbitSgn = (byte)in1.ReadByte();
				in1.ReadInt();
				CCH = (byte)in1.ReadByte();
				in1.ReadByte();
				in1.ReadShort();
				break;
			case DOPERType.BooleanOrErrors:
				grbitSgn = (byte)in1.ReadByte();
				fError = (byte)in1.ReadByte();
				bBoolErr = (byte)in1.ReadByte();
				in1.ReadShort();
				in1.ReadInt();
				break;
			default:
				grbitSgn = 0;
				in1.ReadByte();
				in1.ReadLong();
				break;
			}
		}

		public int Serialize(ILittleEndianOutput out1)
		{
			out1.WriteByte((int)vt);
			switch (vt)
			{
			case DOPERType.RKNumber:
				out1.WriteByte(grbitSgn);
				_RK.Serialize(out1);
				out1.WriteInt(0);
				break;
			case DOPERType.IEEENumber:
				out1.WriteByte(grbitSgn);
				out1.WriteDouble(_IEEENumber);
				break;
			case DOPERType.String:
				out1.WriteByte(grbitSgn);
				out1.WriteInt(0);
				out1.WriteByte(CCH);
				out1.WriteByte(0);
				out1.WriteShort(0);
				break;
			case DOPERType.BooleanOrErrors:
				out1.WriteByte(grbitSgn);
				out1.WriteByte(fError);
				out1.WriteByte(bBoolErr);
				out1.WriteShort(0);
				out1.WriteInt(0);
				break;
			default:
				out1.WriteByte(0);
				out1.WriteLong(0L);
				break;
			}
			return RecordSize;
		}

		public virtual Record CloneViaReserialise()
		{
			throw new NotImplementedException("Please implement it in subclass");
		}

		public override int Serialize(int offset, byte[] data)
		{
			LittleEndianByteArrayOutputStream littleEndianByteArrayOutputStream = new LittleEndianByteArrayOutputStream(data, offset, RecordSize);
			int result = Serialize(littleEndianByteArrayOutputStream);
			if (littleEndianByteArrayOutputStream.WriteIndex - offset != RecordSize)
			{
				throw new InvalidOperationException("Error in serialization of (" + GetType().Name + "): Incorrect number of bytes written - expected " + RecordSize + " but got " + (littleEndianByteArrayOutputStream.WriteIndex - offset));
			}
			return result;
		}
	}
}
