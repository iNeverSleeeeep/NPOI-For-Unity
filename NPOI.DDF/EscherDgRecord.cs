using NPOI.Util;
using System;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// This record simply holds the number of shapes in the drawing group and the
	/// last shape id used for this drawing group.
	/// @author Glen Stampoultzis
	/// </summary>
	public class EscherDgRecord : EscherRecord
	{
		public const short RECORD_ID = -4088;

		public const string RECORD_DESCRIPTION = "MsofbtDg";

		private int field_1_numShapes;

		private int field_2_lastMSOSPID;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize => 16;

		/// <summary>
		/// Return the current record id.
		/// </summary>
		/// <value>The 16 bit record id.</value>
		public override short RecordId => -4088;

		/// <summary>
		///  The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "Dg";

		/// <summary>
		/// Gets or sets The number of shapes in this drawing group.
		/// </summary>
		/// <value>The num shapes.</value>
		public int NumShapes
		{
			get
			{
				return field_1_numShapes;
			}
			set
			{
				field_1_numShapes = value;
			}
		}

		/// <summary>
		/// Gets or sets The last shape id used in this drawing group.
		/// </summary>
		/// <value>The last MSOSPID.</value>
		public int LastMSOSPID
		{
			get
			{
				return field_2_lastMSOSPID;
			}
			set
			{
				field_2_lastMSOSPID = value;
			}
		}

		/// <summary>
		/// Gets the drawing group id for this record.  This is encoded in the
		/// instance part of the option record.
		/// </summary>
		/// <value>The drawing group id.</value>
		public short DrawingGroupId => (short)(Options >> 4);

		/// <summary>
		/// This method deSerializes the record from a byte array.
		/// </summary>
		/// <param name="data">The byte array containing the escher record information</param>
		/// <param name="offset">The starting offset into data</param>
		/// <param name="recordFactory">May be null since this is not a container record.</param>
		/// <returns>The number of bytes Read from the byte array.</returns>
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			ReadHeader(data, offset);
			int num = offset + 8;
			int num2 = 0;
			field_1_numShapes = LittleEndian.GetInt(data, num + num2);
			num2 += 4;
			field_2_lastMSOSPID = LittleEndian.GetInt(data, num + num2);
			num2 += 4;
			return RecordSize;
		}

		/// <summary>
		/// This method Serializes this escher record into a byte array.
		/// </summary>
		/// <param name="offset"> The offset into data to start writing the record data to.</param>
		/// <param name="data"> The byte array to Serialize to.</param>
		/// <returns>The number of bytes written.</returns>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			LittleEndian.PutInt(data, offset + 4, 8);
			LittleEndian.PutInt(data, offset + 8, field_1_numShapes);
			LittleEndian.PutInt(data, offset + 12, field_2_lastMSOSPID);
			listener.AfterRecordSerialize(offset + 16, RecordId, RecordSize, this);
			return RecordSize;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			string newLine = Environment.NewLine;
			return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex((short)(-4088)) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  Instance: 0x" + HexDump.ToHex(Instance) + newLine + "  NumShapes: " + field_1_numShapes + newLine + "  LastMSOSPID: " + field_2_lastMSOSPID + newLine;
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<NumShapes>")
				.Append(field_1_numShapes)
				.Append("</NumShapes>\n")
				.Append(tab)
				.Append("\t")
				.Append("<LastMSOSPID>")
				.Append(field_2_lastMSOSPID)
				.Append("</LastMSOSPID>\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Increments the shape count.
		/// </summary>
		public void IncrementShapeCount()
		{
			field_1_numShapes++;
		}
	}
}
