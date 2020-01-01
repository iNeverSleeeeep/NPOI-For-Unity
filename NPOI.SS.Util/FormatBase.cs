using System.Globalization;
using System.Text;

namespace NPOI.SS.Util
{
	/// <summary>
	/// A substitute class for Format class in Java
	/// </summary>
	public abstract class FormatBase
	{
		public FormatBase()
		{
		}

		public virtual string Format(object obj, CultureInfo culture)
		{
			return obj.ToString();
		}

		public abstract StringBuilder Format(object obj, StringBuilder toAppendTo, CultureInfo culture);

		public abstract object ParseObject(string source, int pos);
	}
}
