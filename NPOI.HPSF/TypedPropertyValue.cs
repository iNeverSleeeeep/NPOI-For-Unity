using NPOI.Util;
using System;

namespace NPOI.HPSF
{
	public class TypedPropertyValue
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(TypedPropertyValue));

		private int _type;

		private object _value;

		public object Value => _value;

		public TypedPropertyValue()
		{
		}

		public TypedPropertyValue(byte[] data, int startOffset)
		{
			Read(data, startOffset);
		}

		public TypedPropertyValue(int type, object value)
		{
			_type = type;
			_value = value;
		}

		public int Read(byte[] data, int startOffset)
		{
			_type = LittleEndian.GetShort(data, startOffset);
			int num = startOffset + 2;
			short @short = LittleEndian.GetShort(data, num);
			num += 2;
			if (@short != 0)
			{
				logger.Log(5, "TypedPropertyValue padding at offset " + num + " MUST be 0, but it's value is " + @short);
			}
			num += ReadValue(data, num);
			return num - startOffset;
		}

		public int ReadValue(byte[] data, int offset)
		{
			switch (_type)
			{
			case 0:
			case 1:
				_value = null;
				return 0;
			case 2:
				_value = LittleEndian.GetShort(data, offset);
				return 4;
			case 3:
				_value = LittleEndian.GetInt(data, offset);
				return 4;
			case 4:
				_value = LittleEndian.GetShort(data, offset);
				return 4;
			case 5:
				_value = LittleEndian.GetDouble(data, offset);
				return 8;
			case 6:
				_value = new Currency(data, offset);
				return 8;
			case 7:
				_value = new Date(data, offset);
				return 8;
			case 8:
				_value = new CodePageString(data, offset);
				return ((CodePageString)_value).Size;
			case 10:
				_value = LittleEndian.GetUInt(data, offset);
				return 4;
			case 11:
				_value = new VariantBool(data, offset);
				return 2;
			case 14:
				_value = new Decimal(data, offset);
				return 16;
			case 16:
				_value = data[offset];
				return 1;
			case 17:
				_value = LittleEndian.GetUByte(data, offset);
				return 2;
			case 18:
				_value = LittleEndian.GetUShort(data, offset);
				return 4;
			case 19:
				_value = LittleEndian.GetUInt(data, offset);
				return 4;
			case 20:
				_value = LittleEndian.GetLong(data, offset);
				return 8;
			case 21:
				_value = LittleEndian.GetByteArray(data, offset, 8);
				return 8;
			case 22:
				_value = LittleEndian.GetInt(data, offset);
				return 4;
			case 23:
				_value = LittleEndian.GetUInt(data, offset);
				return 4;
			case 30:
				_value = new CodePageString(data, offset);
				return ((CodePageString)_value).Size;
			case 31:
				_value = new UnicodeString(data, offset);
				return ((UnicodeString)_value).Size;
			case 64:
				_value = new Filetime(data, offset);
				return 8;
			case 65:
				_value = new Blob(data, offset);
				return ((Blob)_value).Size;
			case 66:
			case 67:
			case 68:
			case 69:
				_value = new IndirectPropertyName(data, offset);
				return ((IndirectPropertyName)_value).Size;
			case 70:
				_value = new Blob(data, offset);
				return ((Blob)_value).Size;
			case 71:
				_value = new ClipboardData(data, offset);
				return ((ClipboardData)_value).Size;
			case 72:
				_value = new GUID(data, offset);
				return 16;
			case 73:
				_value = new VersionedStream(data, offset);
				return ((VersionedStream)_value).Size;
			case 4098:
			case 4099:
			case 4100:
			case 4101:
			case 4102:
			case 4103:
			case 4104:
			case 4106:
			case 4107:
			case 4108:
			case 4112:
			case 4113:
			case 4114:
			case 4115:
			case 4116:
			case 4117:
			case 4126:
			case 4127:
			case 4160:
			case 4167:
			case 4168:
				_value = new Vector((short)(_type & 0xFFF));
				return ((Vector)_value).Read(data, offset);
			case 8194:
			case 8195:
			case 8196:
			case 8197:
			case 8198:
			case 8199:
			case 8200:
			case 8202:
			case 8203:
			case 8204:
			case 8206:
			case 8208:
			case 8209:
			case 8210:
			case 8211:
			case 8214:
			case 8215:
				_value = new Array();
				return ((Array)_value).Read(data, offset);
			default:
				throw new InvalidOperationException("Unknown (possibly, incorrect) TypedPropertyValue type: " + _type);
			}
		}

		internal int ReadValuePadded(byte[] data, int offset)
		{
			int num = ReadValue(data, offset);
			if ((num & 3) != 0)
			{
				return num + (4 - (num & 3));
			}
			return num;
		}
	}
}
