using NPOI.Util;
using System.Collections;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The AlRuns record specifies Rich Text Formatting within chart 
	/// titles (section 2.2.3.3), trendline (section 2.2.3.12), and 
	/// data labels (section 2.2.3.11).
	/// </summary>
	public class AlRunsRecord : StandardRecord
	{
		private class CTFormat
		{
			private short m_offset;

			private short m_fontIndex;

			public short Offset
			{
				get
				{
					return m_offset;
				}
				set
				{
					m_offset = value;
				}
			}

			public short FontIndex => m_fontIndex;

			public CTFormat(short offset, short fontIdx)
			{
				m_offset = offset;
				m_fontIndex = fontIdx;
			}

			public void Serialize(ILittleEndianOutput out1)
			{
				out1.WriteShort(m_offset);
				out1.WriteShort(m_fontIndex);
			}
		}

		public const short sid = 4176;

		private int m_recs;

		private ArrayList m_formats;

		protected override int DataSize => 2 + 4 * m_formats.Count;

		public override short Sid => 4176;

		public AlRunsRecord()
		{
		}

		public AlRunsRecord(RecordInputStream in1)
		{
			m_recs = in1.ReadUShort();
			if (m_formats == null)
			{
				m_formats = new ArrayList(m_recs);
			}
			for (int i = 0; i < m_recs; i++)
			{
				CTFormat value = new CTFormat(in1.ReadShort(), in1.ReadShort());
				m_formats.Add(value);
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(m_formats.Count);
			for (int i = 0; i < m_formats.Count; i++)
			{
				((CTFormat)m_formats[i]).Serialize(out1);
			}
		}

		public int GetFormatCount()
		{
			return m_formats.Count;
		}

		public void ModifyFormatRun(short oldPos, short newLen)
		{
			short num = 0;
			for (int i = 0; i < m_formats.Count; i++)
			{
				CTFormat cTFormat = (CTFormat)m_formats[i];
				if (num != 0)
				{
					cTFormat.Offset += num;
				}
				else if (oldPos == cTFormat.Offset && i < m_formats.Count - 1)
				{
					CTFormat cTFormat2 = (CTFormat)m_formats[i + 1];
					num = (short)(newLen - (cTFormat2.Offset - cTFormat.Offset));
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ALRUNS]\n");
			stringBuilder.Append("    .format_runs       = ").Append(m_recs).Append("\n");
			for (int i = 0; i < m_formats.Count; i++)
			{
				CTFormat cTFormat = (CTFormat)m_formats[i];
				stringBuilder.Append("       .char_offset= ").Append(cTFormat.Offset);
				stringBuilder.Append(",.fontidx= ").Append(cTFormat.FontIndex);
				stringBuilder.Append("\n");
			}
			stringBuilder.Append("[/ALRUNS]\n");
			return stringBuilder.ToString();
		}
	}
}
