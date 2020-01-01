using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Format
{
	/// This class : printing out text.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public class CellTextFormatter : CellFormatter
	{
		private class PartHandler : CellFormatPart.IPartHandler
		{
			private int numplace;

			public int NumPlace => numplace;

			public PartHandler(int numPlace)
			{
				numplace = numPlace;
			}

			public string HandlePart(Match m, string part, CellFormatType type, StringBuilder desc)
			{
				if (part.Equals("@"))
				{
					numplace++;
					return "\0";
				}
				return null;
			}
		}

		private int[] textPos;

		private string desc;

		internal static CellFormatter SIMPLE_TEXT = new CellTextFormatter("@");

		public CellTextFormatter(string format)
			: base(format)
		{
			int[] array = new int[1];
			PartHandler partHandler = new PartHandler(array[0]);
			desc = CellFormatPart.ParseFormat(format, CellFormatType.TEXT, partHandler).ToString();
			textPos = new int[partHandler.NumPlace];
			int startIndex = desc.Length - 1;
			for (int i = 0; i < textPos.Length; i++)
			{
				textPos[i] = desc.LastIndexOf("\0", startIndex);
				startIndex = textPos[i] - 1;
			}
		}

		/// {@inheritDoc} 
		public override void FormatValue(StringBuilder toAppendTo, object obj)
		{
			int length = toAppendTo.Length;
			string text = obj.ToString();
			if (obj is bool)
			{
				text = text.ToUpper();
			}
			toAppendTo.Append(desc);
			for (int i = 0; i < textPos.Length; i++)
			{
				int num = length + textPos[i];
				toAppendTo.Remove(num, 1).Insert(num, text);
			}
		}

		/// {@inheritDoc}
		/// <p />
		/// For text, this is just printing the text.
		public override void SimpleValue(StringBuilder toAppendTo, object value)
		{
			SIMPLE_TEXT.FormatValue(toAppendTo, value);
		}
	}
}
