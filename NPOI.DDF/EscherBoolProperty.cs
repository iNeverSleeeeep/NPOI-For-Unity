using NPOI.Util;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// Represents a bool property.  The actual utility of this property is in doubt because many
	/// of the properties marked as bool seem to actually contain special values.  In other words
	/// they're not true bools.
	/// @author Glen Stampoultzis
	/// </summary>
	public class EscherBoolProperty : EscherSimpleProperty
	{
		/// <summary>
		/// Whether this bool property is true
		/// </summary>
		/// <value><c>true</c> if this instance is true; otherwise, <c>false</c>.</value>
		public bool IsTrue => propertyValue != 0;

		/// <summary>
		/// Whether this bool property is false
		/// </summary>
		/// <value><c>true</c> if this instance is false; otherwise, <c>false</c>.</value>
		public bool IsFalse => propertyValue == 0;

		/// <summary>
		/// Create an instance of an escher bool property.
		/// </summary>
		/// <param name="propertyNumber">The property number (or id)</param>
		/// <param name="value">The 32 bit value of this bool property</param>
		public EscherBoolProperty(short propertyNumber, int value)
			: base(propertyNumber, value)
		{
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append("<").Append(GetType().Name)
				.Append(" id=\"0x")
				.Append(HexDump.ToHex(Id))
				.Append("\" name=\"")
				.Append(Name)
				.Append("\" simpleValue=\"")
				.Append(base.PropertyValue)
				.Append("\" blipId=\"")
				.Append(IsBlipId)
				.Append("\" value=\"")
				.Append(IsTrue)
				.Append("\"")
				.Append("/>\n");
			return stringBuilder.ToString();
		}
	}
}
