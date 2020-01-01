using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Refresh All Record 
	/// Description:  Flag whether to refresh all external data when loading a sheet.
	///               (which hssf doesn't support anyhow so who really cares?)
	/// REFERENCE:  PG 376 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class RefreshAllRecord : StandardRecord
	{
		public const short sid = 439;

		private static BitField refreshFlag = BitFieldFactory.GetInstance(1);

		private int _options;

		/// Get whether to refresh all external data when loading a sheet
		/// @return refreshall or not
		public bool RefreshAll
		{
			get
			{
				return refreshFlag.IsSet(_options);
			}
			set
			{
				_options = refreshFlag.SetBoolean(_options, value);
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 439;

		public RefreshAllRecord(int options)
		{
			_options = options;
		}

		/// Constructs a RefreshAll record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public RefreshAllRecord(RecordInputStream in1)
			: this(in1.ReadUShort())
		{
		}

		public RefreshAllRecord(bool refreshAll)
			: this(0)
		{
			RefreshAll = refreshAll;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[REFRESHALL]\n");
			stringBuilder.Append("    .refreshall      = ").Append(RefreshAll).Append("\n");
			stringBuilder.Append("[/REFRESHALL]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_options);
		}

		public override object Clone()
		{
			return new RefreshAllRecord(_options);
		}
	}
}
