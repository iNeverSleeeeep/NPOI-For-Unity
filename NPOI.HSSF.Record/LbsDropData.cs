using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// This structure specifies properties of the dropdown list control
	public class LbsDropData
	{
		/// Combo dropdown control
		public const int STYLE_COMBO_DROPDOWN = 0;

		/// Combo Edit dropdown control
		public const int STYLE_COMBO_EDIT_DROPDOWN = 1;

		/// Simple dropdown control (just the dropdown button)
		public const int STYLE_COMBO_SIMPLE_DROPDOWN = 2;

		/// An unsigned integer that specifies the style of this dropdown. 
		internal int _wStyle;

		/// An unsigned integer that specifies the number of lines to be displayed in the dropdown.
		internal int _cLine;

		/// An unsigned integer that specifies the smallest width in pixels allowed for the dropdown window
		private int _dxMin;

		/// a string that specifies the current string value in the dropdown
		private string _str;

		/// Optional, undefined and MUST be ignored.
		/// This field MUST exist if and only if the size of str in bytes is an odd number
		private byte _unused;

		public int DataSize
		{
			get
			{
				int num = 6;
				num += StringUtil.GetEncodedSize(_str);
				return num + _unused;
			}
		}

		public LbsDropData()
		{
			_str = "";
			_unused = 0;
		}

		public LbsDropData(ILittleEndianInput in1)
		{
			_wStyle = in1.ReadUShort();
			_cLine = in1.ReadUShort();
			_dxMin = in1.ReadUShort();
			_str = StringUtil.ReadUnicodeString(in1);
			if (StringUtil.GetEncodedSize(_str) % 2 != 0)
			{
				_unused = (byte)in1.ReadByte();
			}
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_wStyle);
			out1.WriteShort(_cLine);
			out1.WriteShort(_dxMin);
			StringUtil.WriteUnicodeString(out1, _str);
			out1.WriteByte(_unused);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[LbsDropData]\n");
			stringBuilder.Append("  ._wStyle:  ").Append(_wStyle).Append('\n');
			stringBuilder.Append("  ._cLine:  ").Append(_cLine).Append('\n');
			stringBuilder.Append("  ._dxMin:  ").Append(_dxMin).Append('\n');
			stringBuilder.Append("  ._str:  ").Append(_str).Append('\n');
			stringBuilder.Append("  ._unused:  ").Append(_unused).Append('\n');
			stringBuilder.Append("[/LbsDropData]\n");
			return stringBuilder.ToString();
		}
	}
}
