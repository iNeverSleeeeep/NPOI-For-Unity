using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Sheet Tab Index Array Record
	/// Description:  Contains an array of sheet id's.  Sheets always keep their ID
	///               regardless of what their name Is.
	/// REFERENCE:  PG 412 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class TabIdRecord : StandardRecord
	{
		public const short sid = 317;

		private static short[] EMPTY_SHORT_ARRAY = new short[0];

		public short[] _tabids;

		protected override int DataSize => _tabids.Length * 2;

		public override short Sid => 317;

		public TabIdRecord()
		{
			_tabids = EMPTY_SHORT_ARRAY;
		}

		/// Constructs a TabID record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public TabIdRecord(RecordInputStream in1)
		{
			_tabids = new short[in1.Remaining / 2];
			for (int i = 0; i < _tabids.Length; i++)
			{
				_tabids[i] = in1.ReadShort();
			}
		}

		/// Set the tab array.  (0,1,2).
		/// @param array of tab id's {0,1,2}
		public void SetTabIdArray(short[] array)
		{
			_tabids = array;
		}

		/// Get the tab array.  (0,1,2).
		/// @return array of tab id's {0,1,2}
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[TABID]\n");
			stringBuilder.Append("    .elements        = ").Append(_tabids.Length).Append("\n");
			for (int i = 0; i < _tabids.Length; i++)
			{
				stringBuilder.Append("    .element_" + i + "       = ").Append(_tabids[i]).Append("\n");
			}
			stringBuilder.Append("[/TABID]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			short[] tabids = _tabids;
			for (int i = 0; i < tabids.Length; i++)
			{
				out1.WriteShort(tabids[i]);
			}
		}
	}
}
