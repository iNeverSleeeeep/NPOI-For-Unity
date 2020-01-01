using NPOI.HSSF.Model;
using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	/// <summary>
	/// FONTLIST = FrtFontList StartObject *(Font [Fbi]) EndObject
	/// </summary>
	public class FontListAggregate : ChartRecordAggregate
	{
		private FrtFontListRecord frtFontList;

		private ChartStartObjectRecord startObject;

		private Dictionary<FontRecord, FbiRecord> dicFonts = new Dictionary<FontRecord, FbiRecord>();

		private ChartEndObjectRecord endObject;

		public FontListAggregate(RecordStream rs, ChartRecordAggregate container)
			: base("FONTLIST", container)
		{
			frtFontList = (FrtFontListRecord)rs.GetNext();
			startObject = (ChartStartObjectRecord)rs.GetNext();
			FontRecord fontRecord = null;
			FbiRecord fbiRecord = null;
			while (rs.PeekNextChartSid() == 49)
			{
				fontRecord = (FontRecord)rs.GetNext();
				fbiRecord = ((rs.PeekNextChartSid() != 4192) ? null : ((FbiRecord)rs.GetNext()));
				dicFonts.Add(fontRecord, fbiRecord);
			}
			endObject = (ChartEndObjectRecord)rs.GetNext();
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			WriteStartBlock(rv);
			rv.VisitRecord(frtFontList);
			rv.VisitRecord(startObject);
			base.IsInStartObject = true;
			foreach (KeyValuePair<FontRecord, FbiRecord> dicFont in dicFonts)
			{
				rv.VisitRecord(dicFont.Key);
				if (dicFont.Value != null)
				{
					rv.VisitRecord(dicFont.Value);
				}
			}
			base.IsInStartObject = false;
			rv.VisitRecord(endObject);
		}
	}
}
