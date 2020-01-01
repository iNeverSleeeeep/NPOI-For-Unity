using NPOI.Util;
using System;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// Holds data from the parent application. Most commonly used to store
	/// text in the format of the parent application, rather than in
	/// Escher format. We don't attempt to understand the contents, since
	/// they will be in the parent's format, not Escher format.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @author Nick Burch  (nick at torchbox dot com)
	/// </summary>
	public class EscherTextboxRecord : EscherRecord
	{
		public const short RECORD_ID = -4083;

		public const string RECORD_DESCRIPTION = "msofbtClientTextbox";

		private static readonly byte[] NO_BYTES = new byte[0];

		/// The data for this record not including the the 8 byte header 
		private byte[] _thedata = NO_BYTES;

		/// <summary>
		/// Returns any extra data associated with this record.  In practice excel
		/// does not seem to put anything here, but with PowerPoint this will
		/// contain the bytes that make up a TextHeaderAtom followed by a
		/// TextBytesAtom/TextCharsAtom
		/// </summary>
		/// <value>The data.</value>
		public byte[] Data => _thedata;

		/// <summary>
		/// Returns the number of bytes that are required to serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize => 8 + _thedata.Length;

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "ClientTextbox";

		/// This method deserializes the record from a byte array.
		///
		/// @param data          The byte array containing the escher record information
		/// @param offset        The starting offset into <c>data</c>.
		/// @param recordFactory May be null since this is not a container record.
		/// @return The number of bytes Read from the byte array.
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			int num = ReadHeader(data, offset);
			_thedata = new byte[num];
			Array.Copy(data, offset + 8, _thedata, 0, num);
			return num + 8;
		}

		/// <summary>
		/// Writes this record and any contained records to the supplied byte
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="data"></param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>the number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			LittleEndian.PutShort(data, offset + 2, RecordId);
			int value = _thedata.Length;
			LittleEndian.PutInt(data, offset + 4, value);
			Array.Copy(_thedata, 0, data, offset + 8, _thedata.Length);
			int num = offset + 8 + _thedata.Length;
			listener.AfterRecordSerialize(num, RecordId, num - offset, this);
			int num2 = num - offset;
			if (num2 != RecordSize)
			{
				throw new RecordFormatException(num2 + " bytes written but RecordSize reports " + RecordSize);
			}
			return num2;
		}

		/// <summary>
		/// Sets the extra data (in the parent application's format) to be
		/// contained by the record. Used when the parent application changes
		/// the contents.
		/// </summary>
		/// <param name="b">The b.</param>
		/// <param name="start">The start.</param>
		/// <param name="length">The length.</param>
		public void SetData(byte[] b, int start, int length)
		{
			_thedata = new byte[length];
			Array.Copy(b, start, _thedata, 0, length);
		}

		/// <summary>
		/// Sets the data.
		/// </summary>
		/// <param name="b">The b.</param>
		public void SetData(byte[] b)
		{
			SetData(b, 0, b.Length);
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
			string text = "";
			try
			{
				if (_thedata.Length != 0)
				{
					text = "  Extra Data:" + newLine;
					text += HexDump.Dump(_thedata, 0L, 0);
				}
			}
			catch (Exception)
			{
				text = "Error!!";
			}
			return GetType().Name + ":" + newLine + "  isContainer: " + base.IsContainerRecord + newLine + "  options: 0x" + HexDump.ToHex(Options) + newLine + "  recordId: 0x" + HexDump.ToHex(RecordId) + newLine + "  numchildren: " + ChildRecords.Count + newLine + text;
		}

		public override string ToXml(string tab)
		{
			string text = "";
			try
			{
				if (_thedata.Length != 0)
				{
					text += HexDump.Dump(_thedata, 0L, 0);
				}
			}
			catch (Exception)
			{
				text = "Error!!";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<ExtraData>")
				.Append(text)
				.Append("</ExtraData>\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}
	}
}
