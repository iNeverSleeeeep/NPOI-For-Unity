using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// A complex property differs from a simple property in that the data can not fit inside a 32 bit
	/// integer.  See the specification for more detailed information regarding exactly what is
	/// stored here.
	/// @author Glen Stampoultzis
	/// </summary>
	public class EscherComplexProperty : EscherProperty
	{
		protected byte[] _complexData = new byte[0];

		/// <summary>
		/// Gets the complex data.
		/// </summary>
		/// <value>The complex data.</value>
		public byte[] ComplexData => _complexData;

		/// <summary>
		/// Caclulates the number of bytes required to Serialize this property.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int PropertySize => 6 + _complexData.Length;

		/// <summary>
		/// Create a complex property using the property id and a byte array containing the complex
		/// data value.
		/// </summary>
		/// <param name="id"> The id consists of the property number, a flag indicating whether this is a blip id and a flag
		/// indicating that this is a complex property.</param>
		/// <param name="complexData">The value of this property.</param>
		public EscherComplexProperty(short id, byte[] complexData)
			: base(id)
		{
			_complexData = complexData;
		}

		/// <summary>
		/// Create a complex property using the property number, a flag to indicate whether this is a
		/// blip reference and the complex property data.
		/// </summary>
		/// <param name="propertyNumber">The property number.</param>
		/// <param name="isBlipId">Whether this is a blip id.  Should be false.</param>
		/// <param name="complexData">The value of this complex property.</param> 
		public EscherComplexProperty(short propertyNumber, bool isBlipId, byte[] complexData)
			: base(propertyNumber, isComplex: true, isBlipId)
		{
			_complexData = complexData;
		}

		/// <summary>
		/// Serializes the simple part of this property.  ie the first 6 bytes.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="pos"></param>
		/// <returns></returns>
		public override int SerializeSimplePart(byte[] data, int pos)
		{
			LittleEndian.PutShort(data, pos, Id);
			LittleEndian.PutInt(data, pos + 2, _complexData.Length);
			return 6;
		}

		/// <summary>
		/// Serializes the complex part of this property
		/// </summary>
		/// <param name="data">The data array to Serialize to</param>
		/// <param name="pos">The offset within data to start serializing to.</param>
		/// <returns>The number of bytes Serialized.</returns>
		public override int SerializeComplexPart(byte[] data, int pos)
		{
			Array.Copy(_complexData, 0, data, pos, _complexData.Length);
			return _complexData.Length;
		}

		/// <summary>
		/// Determine whether this property is equal to another property.
		/// </summary>
		/// <param name="o">The object to compare to.</param>
		/// <returns>True if the objects are equal.</returns>
		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (!(o is EscherComplexProperty))
			{
				return false;
			}
			EscherComplexProperty escherComplexProperty = (EscherComplexProperty)o;
			if (!Arrays.Equals(_complexData, escherComplexProperty._complexData))
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		public override int GetHashCode()
		{
			return Id * 11;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					HexDump.Dump(_complexData, 0L, memoryStream, 0);
					text = memoryStream.ToString();
				}
				catch (Exception ex)
				{
					text = ex.ToString();
				}
			}
			return "propNum: " + PropertyNumber + ", propName: " + EscherProperties.GetPropertyName(PropertyNumber) + ", complex: " + IsComplex + ", blipId: " + IsBlipId + ", data: " + Environment.NewLine + text;
		}

		public override string ToXml(string tab)
		{
			HexDump.ToHex(_complexData, 32);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append("<").Append(GetType().Name)
				.Append(" id=\"0x")
				.Append(HexDump.ToHex(Id))
				.Append("\" name=\"")
				.Append(Name)
				.Append("\" blipId=\"")
				.Append(IsBlipId)
				.Append("\">\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}
	}
}
