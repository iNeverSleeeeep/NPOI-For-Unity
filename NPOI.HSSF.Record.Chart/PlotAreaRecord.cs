using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// * preceeds and identifies a frame as belonging to the plot area.
	/// * NOTE: This source is automatically generated please do not modify this file.  Either subclass or
	/// *       Remove the record in src/records/definitions.
	///
	/// * @author Andrew C. Oliver (acoliver at apache.org)
	public class PlotAreaRecord : StandardRecord
	{
		public const short sid = 4149;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 0;

		public override short Sid => 4149;

		public PlotAreaRecord()
		{
		}

		/// Constructs a PlotArea record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public PlotAreaRecord(RecordInputStream in1)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PLOTAREA]\n");
			stringBuilder.Append("[/PLOTAREA]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
		}

		public override object Clone()
		{
			return new PlotAreaRecord();
		}
	}
}
