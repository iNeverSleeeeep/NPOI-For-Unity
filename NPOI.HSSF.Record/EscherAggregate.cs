using NPOI.DDF;
using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// This class Is used to aggregate the MSODRAWING and OBJ record
	/// combinations.  This Is necessary due to the bizare way in which
	/// these records are Serialized.  What happens Is that you Get a
	/// combination of MSODRAWING -&gt; OBJ -&gt; MSODRAWING -&gt; OBJ records
	/// but the escher records are Serialized _across_ the MSODRAWING
	/// records.
	///
	/// It Gets even worse when you start looking at TXO records.
	///
	/// So what we do with this class Is aggregate lazily.  That Is
	/// we don't aggregate the MSODRAWING -&gt; OBJ records Unless we
	/// need to modify them.
	///
	/// At first document contains 4 types of records which belong to drawing layer.
	/// There are can be such sequence of record:
	/// <p />
	/// DrawingRecord
	/// ContinueRecord
	/// ...
	/// ContinueRecord
	/// ObjRecord | TextObjectRecord
	/// .....
	/// ContinueRecord
	/// ...
	/// ContinueRecord
	/// ObjRecord | TextObjectRecord
	/// NoteRecord
	/// ...
	/// NoteRecord
	/// <p />
	/// To work with shapes we have to read data from Drawing and Continue records into single array of bytes and
	/// build escher(office art) records tree from this array.
	/// Each shape in drawing layer matches corresponding ObjRecord
	/// Each textbox matches corresponding TextObjectRecord
	/// <p />
	/// ObjRecord contains information about shape. Thus each ObjRecord corresponds EscherContainerRecord(SPGR)
	/// <p />
	/// EscherAggrefate contains also NoteRecords
	/// NoteRecords must be serial
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	public class EscherAggregate : AbstractEscherHolderRecord
	{
		internal class CustomEscherRecordFactory : DefaultEscherRecordFactory
		{
			private List<EscherRecord> shapeRecords;

			public CustomEscherRecordFactory(List<EscherRecord> shapeRecords)
			{
				this.shapeRecords = shapeRecords;
			}

			public override EscherRecord CreateRecord(byte[] data, int offset)
			{
				EscherRecord escherRecord = base.CreateRecord(data, offset);
				if (escherRecord.RecordId == -4079 || escherRecord.RecordId == -4083)
				{
					shapeRecords.Add(escherRecord);
				}
				return escherRecord;
			}
		}

		public const short sid = 9876;

		public const short ST_MIN = 0;

		public const short ST_NOT_PRIMATIVE = 0;

		public const short ST_RECTANGLE = 1;

		public const short ST_ROUNDRECTANGLE = 2;

		public const short ST_ELLIPSE = 3;

		public const short ST_DIAMOND = 4;

		public const short ST_ISOCELESTRIANGLE = 5;

		public const short ST_RIGHTTRIANGLE = 6;

		public const short ST_PARALLELOGRAM = 7;

		public const short ST_TRAPEZOID = 8;

		public const short ST_HEXAGON = 9;

		public const short ST_OCTAGON = 10;

		public const short ST_PLUS = 11;

		public const short ST_STAR = 12;

		public const short ST_ARROW = 13;

		public const short ST_THICKARROW = 14;

		public const short ST_HOMEPLATE = 15;

		public const short ST_CUBE = 16;

		public const short ST_BALLOON = 17;

		public const short ST_SEAL = 18;

		public const short ST_ARC = 19;

		public const short ST_LINE = 20;

		public const short ST_PLAQUE = 21;

		public const short ST_CAN = 22;

		public const short ST_DONUT = 23;

		public const short ST_TEXTSIMPLE = 24;

		public const short ST_TEXTOCTAGON = 25;

		public const short ST_TEXTHEXAGON = 26;

		public const short ST_TEXTCURVE = 27;

		public const short ST_TEXTWAVE = 28;

		public const short ST_TEXTRING = 29;

		public const short ST_TEXTONCURVE = 30;

		public const short ST_TEXTONRING = 31;

		public const short ST_STRAIGHTCONNECTOR1 = 32;

		public const short ST_BENTCONNECTOR2 = 33;

		public const short ST_BENTCONNECTOR3 = 34;

		public const short ST_BENTCONNECTOR4 = 35;

		public const short ST_BENTCONNECTOR5 = 36;

		public const short ST_CURVEDCONNECTOR2 = 37;

		public const short ST_CURVEDCONNECTOR3 = 38;

		public const short ST_CURVEDCONNECTOR4 = 39;

		public const short ST_CURVEDCONNECTOR5 = 40;

		public const short ST_CALLOUT1 = 41;

		public const short ST_CALLOUT2 = 42;

		public const short ST_CALLOUT3 = 43;

		public const short ST_ACCENTCALLOUT1 = 44;

		public const short ST_ACCENTCALLOUT2 = 45;

		public const short ST_ACCENTCALLOUT3 = 46;

		public const short ST_BORDERCALLOUT1 = 47;

		public const short ST_BORDERCALLOUT2 = 48;

		public const short ST_BORDERCALLOUT3 = 49;

		public const short ST_ACCENTBORDERCALLOUT1 = 50;

		public const short ST_ACCENTBORDERCALLOUT2 = 51;

		public const short ST_ACCENTBORDERCALLOUT3 = 52;

		public const short ST_RIBBON = 53;

		public const short ST_RIBBON2 = 54;

		public const short ST_CHEVRON = 55;

		public const short ST_PENTAGON = 56;

		public const short ST_NOSMOKING = 57;

		public const short ST_SEAL8 = 58;

		public const short ST_SEAL16 = 59;

		public const short ST_SEAL32 = 60;

		public const short ST_WEDGERECTCALLOUT = 61;

		public const short ST_WEDGERRECTCALLOUT = 62;

		public const short ST_WEDGEELLIPSECALLOUT = 63;

		public const short ST_WAVE = 64;

		public const short ST_FOLDEDCORNER = 65;

		public const short ST_LEFTARROW = 66;

		public const short ST_DOWNARROW = 67;

		public const short ST_UPARROW = 68;

		public const short ST_LEFTRIGHTARROW = 69;

		public const short ST_UPDOWNARROW = 70;

		public const short ST_IRREGULARSEAL1 = 71;

		public const short ST_IRREGULARSEAL2 = 72;

		public const short ST_LIGHTNINGBOLT = 73;

		public const short ST_HEART = 74;

		public const short ST_PICTUREFRAME = 75;

		public const short ST_QUADARROW = 76;

		public const short ST_LEFTARROWCALLOUT = 77;

		public const short ST_RIGHTARROWCALLOUT = 78;

		public const short ST_UPARROWCALLOUT = 79;

		public const short ST_DOWNARROWCALLOUT = 80;

		public const short ST_LEFTRIGHTARROWCALLOUT = 81;

		public const short ST_UPDOWNARROWCALLOUT = 82;

		public const short ST_QUADARROWCALLOUT = 83;

		public const short ST_BEVEL = 84;

		public const short ST_LEFTBRACKET = 85;

		public const short ST_RIGHTBRACKET = 86;

		public const short ST_LEFTBRACE = 87;

		public const short ST_RIGHTBRACE = 88;

		public const short ST_LEFTUPARROW = 89;

		public const short ST_BENTUPARROW = 90;

		public const short ST_BENTARROW = 91;

		public const short ST_SEAL24 = 92;

		public const short ST_STRIPEDRIGHTARROW = 93;

		public const short ST_NOTCHEDRIGHTARROW = 94;

		public const short ST_BLOCKARC = 95;

		public const short ST_SMILEYFACE = 96;

		public const short ST_VERTICALSCROLL = 97;

		public const short ST_HORIZONTALSCROLL = 98;

		public const short ST_CIRCULARARROW = 99;

		public const short ST_NOTCHEDCIRCULARARROW = 100;

		public const short ST_UTURNARROW = 101;

		public const short ST_CURVEDRIGHTARROW = 102;

		public const short ST_CURVEDLEFTARROW = 103;

		public const short ST_CURVEDUPARROW = 104;

		public const short ST_CURVEDDOWNARROW = 105;

		public const short ST_CLOUDCALLOUT = 106;

		public const short ST_ELLIPSERIBBON = 107;

		public const short ST_ELLIPSERIBBON2 = 108;

		public const short ST_FLOWCHARTProcess = 109;

		public const short ST_FLOWCHARTDECISION = 110;

		public const short ST_FLOWCHARTINPUTOUTPUT = 111;

		public const short ST_FLOWCHARTPREDEFINEDProcess = 112;

		public const short ST_FLOWCHARTINTERNALSTORAGE = 113;

		public const short ST_FLOWCHARTDOCUMENT = 114;

		public const short ST_FLOWCHARTMULTIDOCUMENT = 115;

		public const short ST_FLOWCHARTTERMINATOR = 116;

		public const short ST_FLOWCHARTPREPARATION = 117;

		public const short ST_FLOWCHARTMANUALINPUT = 118;

		public const short ST_FLOWCHARTMANUALOPERATION = 119;

		public const short ST_FLOWCHARTCONNECTOR = 120;

		public const short ST_FLOWCHARTPUNCHEDCARD = 121;

		public const short ST_FLOWCHARTPUNCHEDTAPE = 122;

		public const short ST_FLOWCHARTSUMMINGJUNCTION = 123;

		public const short ST_FLOWCHARTOR = 124;

		public const short ST_FLOWCHARTCOLLATE = 125;

		public const short ST_FLOWCHARTSORT = 126;

		public const short ST_FLOWCHARTEXTRACT = 127;

		public const short ST_FLOWCHARTMERGE = 128;

		public const short ST_FLOWCHARTOFFLINESTORAGE = 129;

		public const short ST_FLOWCHARTONLINESTORAGE = 130;

		public const short ST_FLOWCHARTMAGNETICTAPE = 131;

		public const short ST_FLOWCHARTMAGNETICDISK = 132;

		public const short ST_FLOWCHARTMAGNETICDRUM = 133;

		public const short ST_FLOWCHARTDISPLAY = 134;

		public const short ST_FLOWCHARTDELAY = 135;

		public const short ST_TEXTPLAINTEXT = 136;

		public const short ST_TEXTSTOP = 137;

		public const short ST_TEXTTRIANGLE = 138;

		public const short ST_TEXTTRIANGLEINVERTED = 139;

		public const short ST_TEXTCHEVRON = 140;

		public const short ST_TEXTCHEVRONINVERTED = 141;

		public const short ST_TEXTRINGINSIDE = 142;

		public const short ST_TEXTRINGOUTSIDE = 143;

		public const short ST_TEXTARCHUPCURVE = 144;

		public const short ST_TEXTARCHDOWNCURVE = 145;

		public const short ST_TEXTCIRCLECURVE = 146;

		public const short ST_TEXTBUTTONCURVE = 147;

		public const short ST_TEXTARCHUPPOUR = 148;

		public const short ST_TEXTARCHDOWNPOUR = 149;

		public const short ST_TEXTCIRCLEPOUR = 150;

		public const short ST_TEXTBUTTONPOUR = 151;

		public const short ST_TEXTCURVEUP = 152;

		public const short ST_TEXTCURVEDOWN = 153;

		public const short ST_TEXTCASCADEUP = 154;

		public const short ST_TEXTCASCADEDOWN = 155;

		public const short ST_TEXTWAVE1 = 156;

		public const short ST_TEXTWAVE2 = 157;

		public const short ST_TEXTWAVE3 = 158;

		public const short ST_TEXTWAVE4 = 159;

		public const short ST_TEXTINFLATE = 160;

		public const short ST_TEXTDEFLATE = 161;

		public const short ST_TEXTINFLATEBOTTOM = 162;

		public const short ST_TEXTDEFLATEBOTTOM = 163;

		public const short ST_TEXTINFLATETOP = 164;

		public const short ST_TEXTDEFLATETOP = 165;

		public const short ST_TEXTDEFLATEINFLATE = 166;

		public const short ST_TEXTDEFLATEINFLATEDEFLATE = 167;

		public const short ST_TEXTFADERIGHT = 168;

		public const short ST_TEXTFADELEFT = 169;

		public const short ST_TEXTFADEUP = 170;

		public const short ST_TEXTFADEDOWN = 171;

		public const short ST_TEXTSLANTUP = 172;

		public const short ST_TEXTSLANTDOWN = 173;

		public const short ST_TEXTCANUP = 174;

		public const short ST_TEXTCANDOWN = 175;

		public const short ST_FLOWCHARTALTERNATEProcess = 176;

		public const short ST_FLOWCHARTOFFPAGECONNECTOR = 177;

		public const short ST_CALLOUT90 = 178;

		public const short ST_ACCENTCALLOUT90 = 179;

		public const short ST_BORDERCALLOUT90 = 180;

		public const short ST_ACCENTBORDERCALLOUT90 = 181;

		public const short ST_LEFTRIGHTUPARROW = 182;

		public const short ST_SUN = 183;

		public const short ST_MOON = 184;

		public const short ST_BRACKETPAIR = 185;

		public const short ST_BRACEPAIR = 186;

		public const short ST_SEAL4 = 187;

		public const short ST_DOUBLEWAVE = 188;

		public const short ST_ACTIONBUTTONBLANK = 189;

		public const short ST_ACTIONBUTTONHOME = 190;

		public const short ST_ACTIONBUTTONHELP = 191;

		public const short ST_ACTIONBUTTONINFORMATION = 192;

		public const short ST_ACTIONBUTTONFORWARDNEXT = 193;

		public const short ST_ACTIONBUTTONBACKPREVIOUS = 194;

		public const short ST_ACTIONBUTTONEND = 195;

		public const short ST_ACTIONBUTTONBEGINNING = 196;

		public const short ST_ACTIONBUTTONRETURN = 197;

		public const short ST_ACTIONBUTTONDOCUMENT = 198;

		public const short ST_ACTIONBUTTONSOUND = 199;

		public const short ST_ACTIONBUTTONMOVIE = 200;

		public const short ST_HOSTCONTROL = 201;

		public const short ST_TEXTBOX = 202;

		public const short ST_NIL = 4095;

		private static POILogger log = POILogFactory.GetLogger(typeof(EscherAggregate));

		protected HSSFPatriarch patriarch;

		/// Maps shape container objects to their OBJ records 
		private Dictionary<EscherRecord, Record> shapeToObj = new Dictionary<EscherRecord, Record>();

		private DrawingManager2 drawingManager;

		/// list of "tail" records that need to be Serialized after all drawing Group records
		internal Dictionary<int, NoteRecord> tailRec = new Dictionary<int, NoteRecord>();

		/// @return  Returns the current sid.
		public override short Sid => 9876;

		/// @return record size, including header size of obj, text, note, drawing, continue records
		public override int RecordSize
		{
			get
			{
				int num = 0;
				List<EscherRecord> escherRecords = base.EscherRecords;
				int escherRecordSize = GetEscherRecordSize(escherRecords);
				byte[] data = new byte[escherRecordSize];
				List<int> list = new List<int>();
				int num2 = 0;
				foreach (EscherRecord item in escherRecords)
				{
					num2 += item.Serialize(num2, data, new RecordSizeListener(list, item));
				}
				list.Insert(0, 0);
				for (int i = 1; i < list.Count; i++)
				{
					if (i == list.Count - 1 && list[i] < num2)
					{
						num += 4;
					}
					if (list[i] - list[i - 1] > 8224)
					{
						num += (list[i] - list[i - 1]) / 8224 * 4;
					}
				}
				int num3 = escherRecordSize + shapeToObj.Count * 4;
				if (escherRecordSize != 0 && list.Count == 1)
				{
					num += 4;
				}
				int num4 = 0;
				foreach (Record value in shapeToObj.Values)
				{
					num4 += value.RecordSize;
				}
				int num5 = 0;
				foreach (NoteRecord value2 in tailRec.Values)
				{
					num5 += value2.RecordSize;
				}
				return num3 + num4 + num5 + num;
			}
		}

		public HSSFPatriarch Patriarch
		{
			get
			{
				return patriarch;
			}
			set
			{
				patriarch = value;
			}
		}

		protected override string RecordName => "ESCHERAGGREGATE";

		/// @return unmodifiable copy of tail records. We need to access them when building shapes.
		///         Every HSSFComment shape has a link to a NoteRecord from the tailRec collection.
		public Dictionary<int, NoteRecord> TailRecords => tailRec;

		public EscherAggregate(bool createDefaultTree)
		{
			if (createDefaultTree)
			{
				BuildBaseTree();
			}
		}

		/// Calculates the string representation of this record.  This Is
		/// simply a dump of all the records.
		public override string ToString()
		{
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[').Append(RecordName).Append(']' + newLine);
			IEnumerator enumerator = base.EscherRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				stringBuilder.Append(escherRecord.ToString() + newLine);
			}
			stringBuilder.Append("[/").Append(RecordName).Append(']' + newLine);
			return stringBuilder.ToString();
		}

		/// Calculates the xml representation of this record.  This is
		/// simply a dump of all the records.
		/// @param tab - string which must be added before each line (used by default '\t')
		/// @return xml representation of the all aggregated records
		public string ToXml(string tab)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tab).Append("<").Append(RecordName)
				.Append(">\n");
			IEnumerator enumerator = base.EscherRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				stringBuilder.Append(escherRecord.ToXml(tab + "\t"));
			}
			stringBuilder.Append(tab).Append("</").Append(RecordName)
				.Append(">\n");
			return stringBuilder.ToString();
		}

		/// @param sid - record sid we want to check if it belongs to drawing layer
		/// @return true if record is instance of DrawingRecord or ContinueRecord or ObjRecord or TextObjRecord
		private static bool IsDrawingLayerRecord(short sid)
		{
			if (sid != 236 && sid != 60 && sid != 93)
			{
				return sid == 438;
			}
			return true;
		}

		public static EscherAggregate CreateAggregate(List<RecordBase> records, int locFirstDrawingRecord)
		{
			List<EscherRecord> list = new List<EscherRecord>();
			IEscherRecordFactory escherRecordFactory = new CustomEscherRecordFactory(list);
			MemoryStream memoryStream = new MemoryStream();
			EscherAggregate escherAggregate = new EscherAggregate(createDefaultTree: false);
			int num = locFirstDrawingRecord;
			while (num + 1 < records.Count && IsDrawingLayerRecord(GetSid(records, num)))
			{
				try
				{
					if (GetSid(records, num) == 236 || GetSid(records, num) == 60)
					{
						if (GetSid(records, num) == 236)
						{
							byte[] data = ((DrawingRecord)records[num]).Data;
							memoryStream.Write(data, 0, data.Length);
						}
						else
						{
							byte[] data2 = ((ContinueRecord)records[num]).Data;
							memoryStream.Write(data2, 0, data2.Length);
						}
						goto IL_00a9;
					}
					num++;
				}
				catch (IOException ex)
				{
					throw new RuntimeException("Couldn't get data from drawing/continue records", ex);
				}
				continue;
				IL_00a9:
				num++;
			}
			int i = 0;
			int num2;
			for (byte[] array = memoryStream.ToArray(); i < array.Length; i += num2)
			{
				EscherRecord escherRecord = escherRecordFactory.CreateRecord(array, i);
				num2 = escherRecord.FillFields(array, i, escherRecordFactory);
				escherAggregate.AddEscherRecord(escherRecord);
			}
			num = locFirstDrawingRecord + 1;
			int num3 = 0;
			while (num < records.Count && IsDrawingLayerRecord(GetSid(records, num)))
			{
				if (!IsObjectRecord(records, num))
				{
					num++;
				}
				else
				{
					Record value = (Record)records[num];
					escherAggregate.shapeToObj[list[num3++]] = value;
					num++;
				}
			}
			for (; num < records.Count && GetSid(records, num) == 28; num++)
			{
				NoteRecord noteRecord = (NoteRecord)records[num];
				escherAggregate.tailRec[noteRecord.ShapeId] = noteRecord;
			}
			int num5 = num;
			records.RemoveRange(locFirstDrawingRecord, num5 - locFirstDrawingRecord);
			records.Insert(locFirstDrawingRecord, escherAggregate);
			return escherAggregate;
		}

		/// Serializes this aggregate to a byte array.  Since this Is an aggregate
		/// record it will effectively Serialize the aggregated records.
		///
		/// @param offset    The offset into the start of the array.
		/// @param data      The byte array to Serialize to.
		/// @return          The number of bytes Serialized.
		public override int Serialize(int offset, byte[] data)
		{
			List<EscherRecord> escherRecords = base.EscherRecords;
			int escherRecordSize = GetEscherRecordSize(escherRecords);
			byte[] array = new byte[escherRecordSize];
			List<int> list = new List<int>();
			List<EscherRecord> list2 = new List<EscherRecord>();
			int num = 0;
			foreach (EscherRecord item in escherRecords)
			{
				num += item.Serialize(num, array, new SerializationListener(list, list2, item));
			}
			list2.Insert(0, null);
			list.Insert(0, 0);
			num = offset;
			int num2 = 0;
			int i;
			for (i = 1; i < list2.Count; i++)
			{
				int num3 = list[i] - 1;
				int num4 = (i != 1) ? list[i - 1] : 0;
				byte[] array2 = new byte[num3 - num4 + 1];
				Array.Copy(array, num4, array2, 0, array2.Length);
				num += WriteDataIntoDrawingRecord(array2, num2, num, data, i);
				num2 += array2.Length;
				Record record = shapeToObj[list2[i]];
				num += record.Serialize(num, data);
				if (i == list2.Count - 1 && num3 < array.Length - 1)
				{
					array2 = new byte[array.Length - num3 - 1];
					Array.Copy(array, num3 + 1, array2, 0, array2.Length);
					num += WriteDataIntoDrawingRecord(array2, num2, num, data, i);
				}
			}
			if (num - offset < array.Length - 1)
			{
				byte[] array3 = new byte[array.Length - (num - offset)];
				Array.Copy(array, num - offset, array3, 0, array3.Length);
				num += WriteDataIntoDrawingRecord(array3, num2, num, data, i);
			}
			foreach (NoteRecord value in tailRec.Values)
			{
				num += value.Serialize(num, data);
			}
			int num5 = num - offset;
			if (num5 != RecordSize)
			{
				throw new RecordFormatException(num5 + " bytes written but RecordSize reports " + RecordSize);
			}
			return num5;
		}

		/// @param drawingData - escher records saved into single byte array
		/// @param writtenEscherBytes - count of bytes already saved into drawing records (we should know it to decide create
		///                           drawing or continue record)
		/// @param pos current position of data array
		/// @param data - array of bytes where drawing records must be serialized
		/// @param i - number of shape, saved into data array
		/// @return offset of data array after serialization
		private int WriteDataIntoDrawingRecord(byte[] drawingData, int writtenEscherBytes, int pos, byte[] data, int i)
		{
			int num = 0;
			if (writtenEscherBytes + drawingData.Length > 8224 && i != 1)
			{
				for (int j = 0; j < drawingData.Length; j += 8224)
				{
					byte[] array = new byte[Math.Min(8224, drawingData.Length - j)];
					Array.Copy(drawingData, j, array, 0, Math.Min(8224, drawingData.Length - j));
					ContinueRecord continueRecord = new ContinueRecord(array);
					num += continueRecord.Serialize(pos + num, data);
				}
			}
			else
			{
				for (int k = 0; k < drawingData.Length; k += 8224)
				{
					if (k == 0)
					{
						DrawingRecord drawingRecord = new DrawingRecord();
						byte[] array2 = new byte[Math.Min(8224, drawingData.Length - k)];
						Array.Copy(drawingData, k, array2, 0, Math.Min(8224, drawingData.Length - k));
						drawingRecord.Data = array2;
						num += drawingRecord.Serialize(pos + num, data);
					}
					else
					{
						byte[] array3 = new byte[Math.Min(8224, drawingData.Length - k)];
						Array.Copy(drawingData, k, array3, 0, Math.Min(8224, drawingData.Length - k));
						ContinueRecord continueRecord2 = new ContinueRecord(array3);
						num += continueRecord2.Serialize(pos + num, data);
					}
				}
			}
			return num;
		}

		/// How many bytes do the raw escher records contain.
		///
		/// @param records List of escher records
		/// @return the number of bytes
		private int GetEscherRecordSize(List<EscherRecord> records)
		{
			int num = 0;
			foreach (EscherRecord record in records)
			{
				num += record.RecordSize;
			}
			return num;
		}

		/// @param records list of records to look into
		/// @param loc - location of the record which sid must be returned
		/// @return sid of the record with selected location
		private static short GetSid(List<RecordBase> records, int loc)
		{
			return ((Record)records[loc]).Sid;
		}

		/// create base tree with such structure:
		/// EscherDgContainer
		/// -EscherSpgrContainer
		/// --EscherSpContainer
		/// ---EscherSpRecord
		/// ---EscherSpgrRecord
		/// ---EscherSpRecord
		/// -EscherDgRecord
		///
		/// id of DgRecord and SpRecord are empty and must be set later by HSSFPatriarch
		private void BuildBaseTree()
		{
			EscherContainerRecord escherContainerRecord = new EscherContainerRecord();
			EscherContainerRecord escherContainerRecord2 = new EscherContainerRecord();
			EscherContainerRecord escherContainerRecord3 = new EscherContainerRecord();
			EscherSpgrRecord escherSpgrRecord = new EscherSpgrRecord();
			EscherSpRecord escherSpRecord = new EscherSpRecord();
			escherContainerRecord.RecordId = -4094;
			escherContainerRecord.Options = 15;
			EscherDgRecord escherDgRecord = new EscherDgRecord();
			escherDgRecord.RecordId = -4088;
			short num = 1;
			escherDgRecord.Options = (short)(num << 4);
			escherDgRecord.NumShapes = 0;
			escherDgRecord.LastMSOSPID = 1024;
			escherContainerRecord2.RecordId = -4093;
			escherContainerRecord2.Options = 15;
			escherContainerRecord3.RecordId = -4092;
			escherContainerRecord3.Options = 15;
			escherSpgrRecord.RecordId = -4087;
			escherSpgrRecord.Options = 1;
			escherSpgrRecord.RectX1 = 0;
			escherSpgrRecord.RectY1 = 0;
			escherSpgrRecord.RectX2 = 1023;
			escherSpgrRecord.RectY2 = 255;
			escherSpRecord.RecordId = -4086;
			escherSpRecord.Options = 2;
			escherSpRecord.Version = 2;
			escherSpRecord.ShapeId = -1;
			escherSpRecord.Flags = 5;
			escherContainerRecord.AddChildRecord(escherDgRecord);
			escherContainerRecord.AddChildRecord(escherContainerRecord2);
			escherContainerRecord2.AddChildRecord(escherContainerRecord3);
			escherContainerRecord3.AddChildRecord(escherSpgrRecord);
			escherContainerRecord3.AddChildRecord(escherSpRecord);
			AddEscherRecord(escherContainerRecord);
		}

		public EscherAggregate(DrawingManager2 drawingManager)
		{
			this.drawingManager = drawingManager;
		}

		/// Unused since this Is an aggregate record.  Use CreateAggregate().
		///
		/// @see #CreateAggregate
		public IList Children(byte[] data, short size, int offset)
		{
			throw new InvalidOperationException("Should not reach here");
		}

		/// Converts the Records into UserModel
		///  objects on the bound HSSFPatriarch
		private EscherRecord GetEscherChild(EscherContainerRecord owner, int recordId)
		{
			IEnumerator enumerator = owner.ChildRecords.GetEnumerator();
			while (enumerator.MoveNext())
			{
				EscherRecord escherRecord = (EscherRecord)enumerator.Current;
				if (escherRecord.RecordId == recordId)
				{
					return escherRecord;
				}
			}
			return null;
		}

		public void Clear()
		{
			ClearEscherRecords();
			shapeToObj.Clear();
		}

		private static bool IsObjectRecord(List<RecordBase> records, int loc)
		{
			if (GetSid(records, loc) != 93)
			{
				return GetSid(records, loc) == 438;
			}
			return true;
		}

		/// <summary>
		/// Associates an escher record to an OBJ record or a TXO record.
		/// </summary>
		/// <param name="r">ClientData or Textbox record</param>
		/// <param name="objRecord">Obj or TextObj record</param>
		public void AssociateShapeToObjRecord(EscherRecord r, Record objRecord)
		{
			if (!shapeToObj.ContainsKey(r))
			{
				shapeToObj.Add(r, objRecord);
			}
			else
			{
				shapeToObj[r] = objRecord;
			}
		}

		/// <summary>
		/// Remove echerRecord and associated to it Obj or TextObj record
		/// </summary>
		/// <param name="rec">clientData or textbox record to be removed</param>
		public void RemoveShapeToObjRecord(EscherRecord rec)
		{
			shapeToObj.Remove(rec);
		}

		internal void SetDgId(short dgId)
		{
			EscherContainerRecord escherContainer = GetEscherContainer();
			EscherDgRecord escherDgRecord = (EscherDgRecord)escherContainer.GetChildById(-4088);
			escherDgRecord.Options = (short)(dgId << 4);
		}

		internal void SetMainSpRecordId(int shapeId)
		{
			EscherContainerRecord escherContainer = GetEscherContainer();
			EscherContainerRecord escherContainerRecord = (EscherContainerRecord)escherContainer.GetChildById(-4093);
			EscherContainerRecord escherContainerRecord2 = (EscherContainerRecord)escherContainerRecord.GetChild(0);
			EscherSpRecord escherSpRecord = (EscherSpRecord)escherContainerRecord2.GetChildById(-4086);
			escherSpRecord.ShapeId = shapeId;
		}

		public void RemoveTailRecord(NoteRecord note)
		{
			tailRec.Remove(note.ShapeId);
		}

		internal void AddTailRecord(NoteRecord note)
		{
			if (tailRec.ContainsKey(note.ShapeId))
			{
				tailRec.Add(note.ShapeId, note);
			}
			else
			{
				tailRec[note.ShapeId] = note;
			}
		}

		internal Dictionary<EscherRecord, Record> GetShapeToObjMapping()
		{
			return shapeToObj;
		}

		/// @param obj - ObjRecord with id == NoteRecord.id
		/// @return null if note record is not found else returns note record with id == obj.id
		internal NoteRecord GetNoteRecordByObj(ObjRecord obj)
		{
			CommonObjectDataSubRecord commonObjectDataSubRecord = (CommonObjectDataSubRecord)obj.SubRecords[0];
			return tailRec[commonObjectDataSubRecord.ObjectId];
		}
	}
}
