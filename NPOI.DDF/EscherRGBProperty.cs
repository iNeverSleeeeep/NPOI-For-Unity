using NPOI.Util;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// A color property.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	public class EscherRGBProperty : EscherSimpleProperty
	{
		/// <summary>
		/// Gets the color of the RGB.
		/// </summary>
		/// <value>The color of the RGB.</value>
		public int RgbColor => propertyValue;

		/// <summary>
		/// Gets the red.
		/// </summary>
		/// <value>The red.</value>
		public byte Red => (byte)(propertyValue & 0xFF);

		/// <summary>
		/// Gets the green.
		/// </summary>
		/// <value>The green.</value>
		public byte Green => (byte)((propertyValue >> 8) & 0xFF);

		/// <summary>
		/// Gets the blue.
		/// </summary>
		/// <value>The blue.</value>
		public byte Blue => (byte)((propertyValue >> 16) & 0xFF);

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.DDF.EscherRGBProperty" /> class.
		/// </summary>
		/// <param name="propertyNumber">The property number.</param>
		/// <param name="rgbColor">Color of the RGB.</param>
		public EscherRGBProperty(short propertyNumber, int rgbColor)
			: base(propertyNumber, rgbColor)
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
				.Append("\" blipId=\"")
				.Append(IsBlipId)
				.Append("\" value=\"0x")
				.Append(HexDump.ToHex(propertyValue))
				.Append("\"/>\n");
			return stringBuilder.ToString();
		}
	}
}
