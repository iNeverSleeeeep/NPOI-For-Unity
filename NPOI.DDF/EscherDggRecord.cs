using NPOI.Util;
using System;
using System.Collections;
using System.Text;

namespace NPOI.DDF
{
	/// <summary>
	/// This record defines the drawing groups used for a particular sheet.
	/// </summary>
	public class EscherDggRecord : EscherRecord
	{
		public class FileIdCluster
		{
			private int field_1_drawingGroupId;

			private int field_2_numShapeIdsUsed;

			public int DrawingGroupId => field_1_drawingGroupId;

			public int NumShapeIdsUsed => field_2_numShapeIdsUsed;

			public FileIdCluster(int drawingGroupId, int numShapeIdsUsed)
			{
				field_1_drawingGroupId = drawingGroupId;
				field_2_numShapeIdsUsed = numShapeIdsUsed;
			}

			public void IncrementShapeId()
			{
				field_2_numShapeIdsUsed++;
			}
		}

		private class EscherDggRecordComparer : IComparer
		{
			public int Compare(object o1, object o2)
			{
				FileIdCluster fileIdCluster = (FileIdCluster)o1;
				FileIdCluster fileIdCluster2 = (FileIdCluster)o2;
				if (fileIdCluster.DrawingGroupId == fileIdCluster2.DrawingGroupId)
				{
					return 0;
				}
				if (fileIdCluster.DrawingGroupId < fileIdCluster2.DrawingGroupId)
				{
					return -1;
				}
				return 1;
			}
		}

		public const short RECORD_ID = -4090;

		public const string RECORD_DESCRIPTION = "MsofbtDgg";

		private int field_1_shapeIdMax;

		private int field_3_numShapesSaved;

		private int field_4_drawingsSaved;

		private FileIdCluster[] field_5_fileIdClusters;

		private int maxDgId;

		/// <summary>
		/// Returns the number of bytes that are required to Serialize this record.
		/// </summary>
		/// <value>Number of bytes</value>
		public override int RecordSize => 24 + 8 * field_5_fileIdClusters.Length;

		/// <summary>
		/// Return the current record id.
		/// </summary>
		/// <value>The 16 bit record id.</value>
		public override short RecordId => -4090;

		/// <summary>
		/// The short name for this record
		/// </summary>
		/// <value></value>
		public override string RecordName => "Dgg";

		/// <summary>
		/// Gets or sets the shape id max.
		/// </summary>
		/// <value>The shape id max.</value>
		public int ShapeIdMax
		{
			get
			{
				return field_1_shapeIdMax;
			}
			set
			{
				field_1_shapeIdMax = value;
			}
		}

		/// <summary>
		/// Gets the Number of id clusters + 1
		/// </summary>
		/// <value>The num id clusters.</value>
		public int NumIdClusters => field_5_fileIdClusters.Length + 1;

		/// <summary>
		/// Gets or sets the num shapes saved.
		/// </summary>
		/// <value>The num shapes saved.</value>
		public int NumShapesSaved
		{
			get
			{
				return field_3_numShapesSaved;
			}
			set
			{
				field_3_numShapesSaved = value;
			}
		}

		/// <summary>
		/// Gets or sets the drawings saved.
		/// </summary>
		/// <value>The drawings saved.</value>
		public int DrawingsSaved
		{
			get
			{
				return field_4_drawingsSaved;
			}
			set
			{
				field_4_drawingsSaved = value;
			}
		}

		/// <summary>
		/// Gets or sets the max drawing group id.
		/// </summary>
		/// <value>The max drawing group id.</value>
		public int MaxDrawingGroupId
		{
			get
			{
				return maxDgId;
			}
			set
			{
				maxDgId = value;
			}
		}

		/// <summary>
		/// Gets or sets the file id clusters.
		/// </summary>
		/// <value>The file id clusters.</value>
		public FileIdCluster[] FileIdClusters
		{
			get
			{
				return field_5_fileIdClusters;
			}
			set
			{
				field_5_fileIdClusters = value;
			}
		}

		/// <summary>
		/// This method deSerializes the record from a byte array.
		/// </summary>
		/// <param name="data">The byte array containing the escher record information</param>
		/// <param name="offset">The starting offset into data</param>
		/// <param name="recordFactory">May be null since this is not a container record.</param>
		/// <returns>The number of bytes Read from the byte array.</returns>
		public override int FillFields(byte[] data, int offset, IEscherRecordFactory recordFactory)
		{
			int num = ReadHeader(data, offset);
			int num2 = offset + 8;
			int num3 = 0;
			field_1_shapeIdMax = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_3_numShapesSaved = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_4_drawingsSaved = LittleEndian.GetInt(data, num2 + num3);
			num3 += 4;
			field_5_fileIdClusters = new FileIdCluster[(num - num3) / 8];
			for (int i = 0; i < field_5_fileIdClusters.Length; i++)
			{
				field_5_fileIdClusters[i] = new FileIdCluster(LittleEndian.GetInt(data, num2 + num3), LittleEndian.GetInt(data, num2 + num3 + 4));
				maxDgId = Math.Max(maxDgId, field_5_fileIdClusters[i].DrawingGroupId);
				num3 += 8;
			}
			num -= num3;
			if (num != 0)
			{
				throw new RecordFormatException("Expecting no remaining data but got " + num + " byte(s).");
			}
			return 8 + num3 + num;
		}

		/// <summary>
		/// This method Serializes this escher record into a byte array.
		/// </summary>
		/// <param name="offset">The offset into data to start writing the record data to.</param>
		/// <param name="data">The byte array to Serialize to.</param>
		/// <param name="listener">a listener for begin and end serialization events.</param>
		/// <returns>The number of bytes written.</returns>
		public override int Serialize(int offset, byte[] data, EscherSerializationListener listener)
		{
			listener.BeforeRecordSerialize(offset, RecordId, this);
			LittleEndian.PutShort(data, offset, Options);
			int num = offset + 2;
			LittleEndian.PutShort(data, num, RecordId);
			num += 2;
			int value = RecordSize - 8;
			LittleEndian.PutInt(data, num, value);
			num += 4;
			LittleEndian.PutInt(data, num, field_1_shapeIdMax);
			num += 4;
			LittleEndian.PutInt(data, num, NumIdClusters);
			num += 4;
			LittleEndian.PutInt(data, num, field_3_numShapesSaved);
			num += 4;
			LittleEndian.PutInt(data, num, field_4_drawingsSaved);
			num += 4;
			for (int i = 0; i < field_5_fileIdClusters.Length; i++)
			{
				LittleEndian.PutInt(data, num, field_5_fileIdClusters[i].DrawingGroupId);
				num += 4;
				LittleEndian.PutInt(data, num, field_5_fileIdClusters[i].NumShapeIdsUsed);
				num += 4;
			}
			listener.AfterRecordSerialize(num, RecordId, RecordSize, this);
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
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < field_5_fileIdClusters.Length; i++)
			{
				stringBuilder.Append("  DrawingGroupId").Append(i + 1).Append(": ");
				stringBuilder.Append(field_5_fileIdClusters[i].DrawingGroupId);
				stringBuilder.Append(newLine);
				stringBuilder.Append("  NumShapeIdsUsed").Append(i + 1).Append(": ");
				stringBuilder.Append(field_5_fileIdClusters[i].NumShapeIdsUsed);
				stringBuilder.Append(newLine);
			}
			return GetType().Name + ":" + newLine + "  RecordId: 0x" + HexDump.ToHex((short)(-4090)) + newLine + "  Version: 0x" + HexDump.ToHex(Version) + newLine + "  Instance: 0x" + HexDump.ToHex(Instance) + newLine + "  ShapeIdMax: " + field_1_shapeIdMax + newLine + "  NumIdClusters: " + NumIdClusters + newLine + "  NumShapesSaved: " + field_3_numShapesSaved + newLine + "  DrawingsSaved: " + field_4_drawingsSaved + newLine + stringBuilder.ToString();
		}

		public override string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append(FormatXmlRecordHeader(GetType().Name, HexDump.ToHex(RecordId), HexDump.ToHex(Version), HexDump.ToHex(Instance))).Append(tab)
				.Append("\t")
				.Append("<ShapeIdMax>")
				.Append(field_1_shapeIdMax)
				.Append("</ShapeIdMax>\n")
				.Append(tab)
				.Append("\t")
				.Append("<NumIdClusters>")
				.Append(NumIdClusters)
				.Append("</NumIdClusters>\n")
				.Append(tab)
				.Append("\t")
				.Append("<NumShapesSaved>")
				.Append(field_3_numShapesSaved)
				.Append("</NumShapesSaved>\n")
				.Append(tab)
				.Append("\t")
				.Append("<DrawingsSaved>")
				.Append(field_4_drawingsSaved)
				.Append("</DrawingsSaved>\n");
			stringBuilder.Append(tab).Append("</").Append(GetType().Name)
				.Append(">\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Adds the cluster.
		/// </summary>
		/// <param name="dgId">The dg id.</param>
		/// <param name="numShapedUsed">The num shaped used.</param>
		public void AddCluster(int dgId, int numShapedUsed)
		{
			AddCluster(dgId, numShapedUsed, sort: true);
		}

		/// <summary>
		/// Adds the cluster.
		/// </summary>
		/// <param name="dgId">id of the drawing group (stored in the record options)</param>
		/// <param name="numShapedUsed">initial value of the numShapedUsed field</param>
		/// <param name="sort">if set to <c>true</c> if true then sort clusters by drawing group id.(
		/// In Excel the clusters are sorted but in PPT they are not).</param>
		public void AddCluster(int dgId, int numShapedUsed, bool sort)
		{
			ArrayList arrayList = new ArrayList(field_5_fileIdClusters);
			arrayList.Add(new FileIdCluster(dgId, numShapedUsed));
			arrayList.Sort(new EscherDggRecordComparer());
			maxDgId = Math.Min(maxDgId, dgId);
			field_5_fileIdClusters = (FileIdCluster[])arrayList.ToArray(typeof(FileIdCluster));
		}
	}
}
