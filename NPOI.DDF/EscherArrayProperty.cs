using NPOI.Util;
using System;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// Escher array properties are the most wierd construction ever invented
	/// with all sorts of special cases.  I'm hopeful I've got them all.
	/// @author Glen Stampoultzis (glens at superlinksoftware.com)
	/// </summary>
	public class EscherArrayProperty : EscherComplexProperty
	{
		/// The size of the header that goes at the
		///  start of the array, before the data
		private const int FIXED_SIZE = 6;

		/// Normally, the size recorded in the simple data (for the complex
		///  data) includes the size of the header.
		/// There are a few cases when it doesn't though...
		private bool sizeIncludesHeaderSize = true;

		/// When Reading a property from data stream remeber if the complex part is empty and Set this flag.
		private bool emptyComplexPart;

		public int NumberOfElementsInArray
		{
			get
			{
				if (emptyComplexPart)
				{
					return 0;
				}
				return LittleEndian.GetUShort(_complexData, 0);
			}
			set
			{
				int num = value * GetActualSizeOfElements(SizeOfElements) + 6;
				if (num != _complexData.Length)
				{
					byte[] array = new byte[num];
					Array.Copy(_complexData, 0, array, 0, _complexData.Length);
					_complexData = array;
				}
				LittleEndian.PutShort(_complexData, 0, (short)value);
			}
		}

		public int NumberOfElementsInMemory
		{
			get
			{
				return LittleEndian.GetUShort(_complexData, 2);
			}
			set
			{
				int num = value * GetActualSizeOfElements(SizeOfElements) + 6;
				if (num != _complexData.Length)
				{
					byte[] array = new byte[num];
					Array.Copy(_complexData, 0, array, 0, num);
					_complexData = array;
				}
				LittleEndian.PutShort(_complexData, 2, (short)value);
			}
		}

		public short SizeOfElements
		{
			get
			{
				return LittleEndian.GetShort(_complexData, 4);
			}
			set
			{
				LittleEndian.PutShort(_complexData, 4, value);
				int num = NumberOfElementsInArray * GetActualSizeOfElements(SizeOfElements) + 6;
				if (num != _complexData.Length)
				{
					byte[] array = new byte[num];
					Array.Copy(_complexData, 0, array, 0, 6);
					_complexData = array;
				}
			}
		}

		public EscherArrayProperty(short id, byte[] complexData)
			: base(id, CheckComplexData(complexData))
		{
			emptyComplexPart = (complexData.Length == 0);
		}

		public EscherArrayProperty(short propertyNumber, bool isBlipId, byte[] complexData)
			: base(propertyNumber, isBlipId, CheckComplexData(complexData))
		{
		}

		private static byte[] CheckComplexData(byte[] complexData)
		{
			if (complexData == null || complexData.Length == 0)
			{
				complexData = new byte[6];
			}
			return complexData;
		}

		/// <summary>
		/// Gets the element.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public byte[] GetElement(int index)
		{
			int actualSizeOfElements = GetActualSizeOfElements(SizeOfElements);
			byte[] array = new byte[actualSizeOfElements];
			Array.Copy(_complexData, 6 + index * actualSizeOfElements, array, 0, array.Length);
			return array;
		}

		/// <summary>
		/// Sets the element.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="element">The element.</param>
		public void SetElement(int index, byte[] element)
		{
			int actualSizeOfElements = GetActualSizeOfElements(SizeOfElements);
			Array.Copy(element, 0, _complexData, 6 + index * actualSizeOfElements, actualSizeOfElements);
		}

		/// <summary>
		/// Retrieves the string representation for this property.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("    {EscherArrayProperty:" + newLine);
			stringBuilder.Append("     Num Elements: " + NumberOfElementsInArray + newLine);
			stringBuilder.Append("     Num Elements In Memory: " + NumberOfElementsInMemory + newLine);
			stringBuilder.Append("     Size of elements: " + SizeOfElements + newLine);
			for (int i = 0; i < NumberOfElementsInArray; i++)
			{
				stringBuilder.Append("     Element " + i + ": " + HexDump.ToHex(GetElement(i)) + newLine);
			}
			stringBuilder.Append("}" + newLine);
			return "propNum: " + PropertyNumber + ", propName: " + EscherProperties.GetPropertyName(PropertyNumber) + ", complex: " + IsComplex + ", blipId: " + IsBlipId + ", data: " + newLine + stringBuilder.ToString();
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append("<").Append(GetType().Name)
				.Append(" id=\"0x")
				.Append(HexDump.ToHex(Id))
				.Append("\" name=\"")
				.Append(Name)
				.Append("\" blipId=\"")
				.Append(IsBlipId)
				.Append("\">\n");
			for (int i = 0; i < NumberOfElementsInArray; i++)
			{
				stringBuilder.Append("\t").Append(tab).Append("<Element>")
					.Append(HexDump.ToHex(GetElement(i)))
					.Append("</Element>\n");
			}
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// We have this method because the way in which arrays in escher works
		/// is screwed for seemly arbitary reasons.  While most properties are
		/// fairly consistent and have a predictable array size, escher arrays
		/// have special cases.
		/// </summary>
		/// <param name="data">The data array containing the escher array information</param>
		/// <param name="offset">The offset into the array to start Reading from.</param>
		/// <returns>the number of bytes used by this complex property.</returns>
		public int SetArrayData(byte[] data, int offset)
		{
			if (emptyComplexPart)
			{
				_complexData = new byte[0];
			}
			else
			{
				short @short = LittleEndian.GetShort(data, offset);
				LittleEndian.GetShort(data, offset + 2);
				short short2 = LittleEndian.GetShort(data, offset + 4);
				int num = GetActualSizeOfElements(short2) * @short;
				if (num == _complexData.Length)
				{
					_complexData = new byte[num + 6];
					sizeIncludesHeaderSize = false;
				}
				Array.Copy(data, offset, _complexData, 0, _complexData.Length);
			}
			return _complexData.Length;
		}

		/// <summary>
		/// Serializes the simple part of this property.  ie the first 6 bytes.
		/// Needs special code to handle the case when the size doesn't
		/// include the size of the header block
		/// </summary>
		/// <param name="data"></param>
		/// <param name="pos"></param>
		/// <returns></returns>
		public override int SerializeSimplePart(byte[] data, int pos)
		{
			LittleEndian.PutShort(data, pos, Id);
			int num = _complexData.Length;
			if (!sizeIncludesHeaderSize)
			{
				num -= 6;
			}
			LittleEndian.PutInt(data, pos + 2, num);
			return 6;
		}

		/// <summary>
		/// Sometimes the element size is stored as a negative number.  We
		/// negate it and shift it to Get the real value.
		/// </summary>
		/// <param name="sizeOfElements">The size of elements.</param>
		/// <returns></returns>
		public static int GetActualSizeOfElements(short sizeOfElements)
		{
			if (sizeOfElements < 0)
			{
				return (short)(-sizeOfElements >> 2);
			}
			return sizeOfElements;
		}
	}
}
