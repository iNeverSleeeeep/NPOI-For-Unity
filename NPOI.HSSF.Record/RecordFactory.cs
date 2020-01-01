using NPOI.HSSF.Record.AutoFilter;
using NPOI.HSSF.Record.Chart;
using NPOI.HSSF.Record.PivotTable;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace NPOI.HSSF.Record
{
	/// Title:  Record Factory
	/// Description:  Takes a stream and outputs an array of Record objects.
	///
	/// @deprecated use {@link org.apache.poi.hssf.eventmodel.EventRecordFactory} instead
	/// @see org.apache.poi.hssf.eventmodel.EventRecordFactory
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @author Csaba Nagy (ncsaba at yahoo dot com)
	public class RecordFactory
	{
		private interface I_RecordCreator
		{
			Record Create(RecordInputStream in1);

			Type GetRecordClass();
		}

		private class ReflectionConstructorRecordCreator : I_RecordCreator
		{
			private ConstructorInfo _c;

			public ReflectionConstructorRecordCreator(ConstructorInfo c)
			{
				_c = c;
			}

			public Record Create(RecordInputStream in1)
			{
				object[] parameters = new object[1]
				{
					in1
				};
				try
				{
					return (Record)_c.Invoke(parameters);
				}
				catch (Exception ex)
				{
					throw new RecordFormatException("Unable to construct record instance", ex.InnerException);
				}
			}

			public Type GetRecordClass()
			{
				return _c.DeclaringType;
			}
		}

		/// A "create" method is used instead of the usual constructor if the created record might
		/// be of a different class to the declaring class.
		private class ReflectionMethodRecordCreator : I_RecordCreator
		{
			private MethodInfo _m;

			public ReflectionMethodRecordCreator(MethodInfo m)
			{
				_m = m;
			}

			public Record Create(RecordInputStream in1)
			{
				object[] parameters = new object[1]
				{
					in1
				};
				try
				{
					return (Record)_m.Invoke(null, parameters);
				}
				catch (Exception ex)
				{
					throw new RecordFormatException("Unable to construct record instance", ex.InnerException);
				}
			}

			public Type GetRecordClass()
			{
				return _m.DeclaringType;
			}
		}

		private static int NUM_RECORDS;

		private static Type[] recordClasses;

		private static Type[] CONSTRUCTOR_ARGS;

		/// cache of the recordsToMap();
		private static Dictionary<short, I_RecordCreator> _recordCreatorsById;

		private static short[] _allKnownRecordSIDs;

		static RecordFactory()
		{
			NUM_RECORDS = 512;
			CONSTRUCTOR_ARGS = new Type[1]
			{
				typeof(RecordInputStream)
			};
			_recordCreatorsById = null;
			recordClasses = new Type[201]
			{
				typeof(ArrayRecord),
				typeof(AutoFilterInfoRecord),
				typeof(BackupRecord),
				typeof(BlankRecord),
				typeof(BOFRecord),
				typeof(BookBoolRecord),
				typeof(BoolErrRecord),
				typeof(BottomMarginRecord),
				typeof(BoundSheetRecord),
				typeof(CalcCountRecord),
				typeof(CalcModeRecord),
				typeof(CFHeaderRecord),
				typeof(CFRuleRecord),
				typeof(ChartRecord),
				typeof(AlRunsRecord),
				typeof(CodepageRecord),
				typeof(ColumnInfoRecord),
				typeof(ContinueRecord),
				typeof(CountryRecord),
				typeof(CRNCountRecord),
				typeof(CRNRecord),
				typeof(DateWindow1904Record),
				typeof(DBCellRecord),
				typeof(DConRefRecord),
				typeof(DefaultColWidthRecord),
				typeof(DefaultRowHeightRecord),
				typeof(DeltaRecord),
				typeof(DimensionsRecord),
				typeof(DrawingGroupRecord),
				typeof(DrawingRecord),
				typeof(DrawingSelectionRecord),
				typeof(DSFRecord),
				typeof(DVALRecord),
				typeof(DVRecord),
				typeof(EOFRecord),
				typeof(ExtendedFormatRecord),
				typeof(ExternalNameRecord),
				typeof(ExternSheetRecord),
				typeof(ExtSSTRecord),
				typeof(FilePassRecord),
				typeof(FileSharingRecord),
				typeof(FnGroupCountRecord),
				typeof(FontRecord),
				typeof(FooterRecord),
				typeof(FormatRecord),
				typeof(FormulaRecord),
				typeof(GridsetRecord),
				typeof(GutsRecord),
				typeof(HCenterRecord),
				typeof(HeaderRecord),
				typeof(HeaderFooterRecord),
				typeof(HideObjRecord),
				typeof(HorizontalPageBreakRecord),
				typeof(HyperlinkRecord),
				typeof(IndexRecord),
				typeof(InterfaceEndRecord),
				typeof(InterfaceHdrRecord),
				typeof(IterationRecord),
				typeof(LabelRecord),
				typeof(LabelSSTRecord),
				typeof(LeftMarginRecord),
				typeof(MergeCellsRecord),
				typeof(MMSRecord),
				typeof(MulBlankRecord),
				typeof(MulRKRecord),
				typeof(NameRecord),
				typeof(NameCommentRecord),
				typeof(NoteRecord),
				typeof(NumberRecord),
				typeof(ObjectProtectRecord),
				typeof(ObjRecord),
				typeof(PaletteRecord),
				typeof(PaneRecord),
				typeof(PasswordRecord),
				typeof(PasswordRev4Record),
				typeof(PrecisionRecord),
				typeof(PrintGridlinesRecord),
				typeof(PrintHeadersRecord),
				typeof(PrintSetupRecord),
				typeof(PrintSizeRecord),
				typeof(ProtectionRev4Record),
				typeof(ProtectRecord),
				typeof(RecalcIdRecord),
				typeof(RefModeRecord),
				typeof(RefreshAllRecord),
				typeof(RightMarginRecord),
				typeof(RKRecord),
				typeof(RowRecord),
				typeof(SaveRecalcRecord),
				typeof(ScenarioProtectRecord),
				typeof(SCLRecord),
				typeof(SelectionRecord),
				typeof(SeriesRecord),
				typeof(SeriesTextRecord),
				typeof(SharedFormulaRecord),
				typeof(SSTRecord),
				typeof(StringRecord),
				typeof(StyleRecord),
				typeof(SupBookRecord),
				typeof(TabIdRecord),
				typeof(TableRecord),
				typeof(TableStylesRecord),
				typeof(TextObjectRecord),
				typeof(TopMarginRecord),
				typeof(UncalcedRecord),
				typeof(UseSelFSRecord),
				typeof(UserSViewBegin),
				typeof(UserSViewEnd),
				typeof(ValueRangeRecord),
				typeof(VCenterRecord),
				typeof(VerticalPageBreakRecord),
				typeof(WindowOneRecord),
				typeof(WindowProtectRecord),
				typeof(WindowTwoRecord),
				typeof(WriteAccessRecord),
				typeof(WriteProtectRecord),
				typeof(WSBoolRecord),
				typeof(SheetExtRecord),
				typeof(AreaFormatRecord),
				typeof(AreaRecord),
				typeof(AttachedLabelRecord),
				typeof(AxcExtRecord),
				typeof(AxisLineRecord),
				typeof(AxisParentRecord),
				typeof(AxisRecord),
				typeof(AxesUsedRecord),
				typeof(BarRecord),
				typeof(BeginRecord),
				typeof(BopPopCustomRecord),
				typeof(BopPopRecord),
				typeof(BRAIRecord),
				typeof(CatLabRecord),
				typeof(CatSerRangeRecord),
				typeof(Chart3DBarShapeRecord),
				typeof(Chart3dRecord),
				typeof(ChartEndObjectRecord),
				typeof(ChartFormatRecord),
				typeof(ChartFRTInfoRecord),
				typeof(ChartStartObjectRecord),
				typeof(CrtLayout12ARecord),
				typeof(CrtLayout12Record),
				typeof(CrtLineRecord),
				typeof(CrtLinkRecord),
				typeof(CrtMlFrtContinueRecord),
				typeof(CrtMlFrtRecord),
				typeof(DataFormatRecord),
				typeof(DataLabExtContentsRecord),
				typeof(DataLabExtRecord),
				typeof(DatRecord),
				typeof(DefaultTextRecord),
				typeof(DropBarRecord),
				typeof(EndBlockRecord),
				typeof(EndRecord),
				typeof(Fbi2Record),
				typeof(FbiRecord),
				typeof(FontXRecord),
				typeof(FrameRecord),
				typeof(FrtFontListRecord),
				typeof(GelFrameRecord),
				typeof(IFmtRecordRecord),
				typeof(LegendExceptionRecord),
				typeof(LegendRecord),
				typeof(LineFormatRecord),
				typeof(MarkerFormatRecord),
				typeof(ObjectLinkRecord),
				typeof(PicFRecord),
				typeof(PieFormatRecord),
				typeof(PieRecord),
				typeof(PlotAreaRecord),
				typeof(PlotGrowthRecord),
				typeof(PosRecord),
				typeof(RadarAreaRecord),
				typeof(RadarRecord),
				typeof(RichTextStreamRecord),
				typeof(ScatterRecord),
				typeof(SerAuxErrBarRecord),
				typeof(SerAuxTrendRecord),
				typeof(SerFmtRecord),
				typeof(SeriesIndexRecord),
				typeof(SeriesListRecord),
				typeof(SerParentRecord),
				typeof(SerToCrtRecord),
				typeof(ShapePropsStreamRecord),
				typeof(ShtPropsRecord),
				typeof(StartBlockRecord),
				typeof(SurfRecord),
				typeof(TextPropsStreamRecord),
				typeof(TextRecord),
				typeof(TickRecord),
				typeof(UnitsRecord),
				typeof(YMultRecord),
				typeof(DataItemRecord),
				typeof(ExtendedPivotTableViewFieldsRecord),
				typeof(PageItemRecord),
				typeof(StreamIDRecord),
				typeof(ViewDefinitionRecord),
				typeof(ViewFieldsRecord),
				typeof(ViewSourceRecord),
				typeof(AutoFilterRecord),
				typeof(FilterModeRecord),
				typeof(Excel9FileRecord)
			};
			_recordCreatorsById = RecordsToMap(recordClasses);
		}

		/// Debug / diagnosis method<br />
		/// Gets the POI implementation class for a given <tt>sid</tt>.  Only a subset of the any BIFF
		/// records are actually interpreted by POI.  A few others are known but not interpreted
		/// (see {@link UnknownRecord#getBiffName(int)}).
		/// @return the POI implementation class for the specified record <tt>sid</tt>.
		/// <code>null</code> if the specified record is not interpreted by POI.
		public static Type GetRecordClass(int sid)
		{
			return _recordCreatorsById[(short)sid]?.GetRecordClass();
		}

		/// Changes the default capacity (10000) to handle larger files
		public static void SetCapacity(int capacity)
		{
			NUM_RECORDS = capacity;
		}

		/// Create an array of records from an input stream
		///
		/// @param in the InputStream from which the records will be
		///           obtained
		///
		/// @return an array of Records Created from the InputStream
		///
		/// @exception RecordFormatException on error Processing the
		///            InputStream
		public static List<Record> CreateRecords(Stream in1)
		{
			List<Record> list = new List<Record>(NUM_RECORDS);
			RecordFactoryInputStream recordFactoryInputStream = new RecordFactoryInputStream(in1, shouldIncludeContinueRecords: true);
			Record item;
			while ((item = recordFactoryInputStream.NextRecord()) != null)
			{
				list.Add(item);
			}
			return list;
		}

		[Obsolete]
		private static void AddAll(List<Record> destList, Record[] srcRecs)
		{
			for (int i = 0; i < srcRecs.Length; i++)
			{
				destList.Add(srcRecs[i]);
			}
		}

		public static Record[] CreateRecord(RecordInputStream in1)
		{
			Record record = CreateSingleRecord(in1);
			if (record is DBCellRecord)
			{
				return new Record[1];
			}
			if (record is RKRecord)
			{
				return new Record[1]
				{
					ConvertToNumberRecord((RKRecord)record)
				};
			}
			if (record is MulRKRecord)
			{
				return ConvertRKRecords((MulRKRecord)record);
			}
			return new Record[1]
			{
				record
			};
		}

		/// Converts a {@link MulBlankRecord} into an equivalent array of {@link BlankRecord}s
		public static BlankRecord[] ConvertBlankRecords(MulBlankRecord mbk)
		{
			BlankRecord[] array = new BlankRecord[mbk.NumColumns];
			for (int i = 0; i < mbk.NumColumns; i++)
			{
				BlankRecord blankRecord = new BlankRecord();
				blankRecord.Column = i + mbk.FirstColumn;
				blankRecord.Row = mbk.Row;
				blankRecord.XFIndex = mbk.GetXFAt(i);
				array[i] = blankRecord;
			}
			return array;
		}

		public static Record CreateSingleRecord(RecordInputStream in1)
		{
			if (_recordCreatorsById.ContainsKey(in1.Sid))
			{
				I_RecordCreator i_RecordCreator = _recordCreatorsById[in1.Sid];
				return i_RecordCreator.Create(in1);
			}
			return new UnknownRecord(in1);
		}

		/// <summary>
		/// RK record is a slightly smaller alternative to NumberRecord
		/// POI likes NumberRecord better
		/// </summary>
		/// <param name="rk">The rk.</param>
		/// <returns></returns>
		public static NumberRecord ConvertToNumberRecord(RKRecord rk)
		{
			NumberRecord numberRecord = new NumberRecord();
			numberRecord.Column = rk.Column;
			numberRecord.Row = rk.Row;
			numberRecord.XFIndex = rk.XFIndex;
			numberRecord.Value = rk.RKNumber;
			return numberRecord;
		}

		/// <summary>
		/// Converts a MulRKRecord into an equivalent array of NumberRecords
		/// </summary>
		/// <param name="mrk">The MRK.</param>
		/// <returns></returns>
		public static NumberRecord[] ConvertRKRecords(MulRKRecord mrk)
		{
			NumberRecord[] array = new NumberRecord[mrk.NumColumns];
			for (int i = 0; i < mrk.NumColumns; i++)
			{
				NumberRecord numberRecord = new NumberRecord();
				numberRecord.Column = (short)(i + mrk.FirstColumn);
				numberRecord.Row = mrk.Row;
				numberRecord.XFIndex = mrk.GetXFAt(i);
				numberRecord.Value = mrk.GetRKNumberAt(i);
				array[i] = numberRecord;
			}
			return array;
		}

		public static short[] GetAllKnownRecordSIDs()
		{
			if (_allKnownRecordSIDs == null)
			{
				short[] array = new short[_recordCreatorsById.Count];
				int num = 0;
				foreach (KeyValuePair<short, I_RecordCreator> item in _recordCreatorsById)
				{
					array[num++] = item.Key;
				}
				Array.Sort(array);
				_allKnownRecordSIDs = array;
			}
			return (short[])_allKnownRecordSIDs.Clone();
		}

		private static Dictionary<short, I_RecordCreator> RecordsToMap(Type[] records)
		{
			Dictionary<short, I_RecordCreator> dictionary = new Dictionary<short, I_RecordCreator>();
			Hashtable hashtable = new Hashtable(records.Length * 3 / 2);
			foreach (Type type in records)
			{
				if (!typeof(Record).IsAssignableFrom(type))
				{
					throw new Exception("Invalid record sub-class (" + type.Name + ")");
				}
				if (type.IsAbstract)
				{
					throw new Exception("Invalid record class (" + type.Name + ") - must not be abstract");
				}
				if (hashtable.Contains(type))
				{
					throw new Exception("duplicate record class (" + type.Name + ")");
				}
				hashtable.Add(type, type);
				short num = 0;
				try
				{
					num = (short)type.GetField("sid").GetValue(null);
				}
				catch (Exception ex)
				{
					throw new RecordFormatException("Unable to determine record types", ex);
				}
				if (dictionary.ContainsKey(num))
				{
					Type recordClass = dictionary[num].GetRecordClass();
					throw new RuntimeException("duplicate record sid 0x" + num.ToString("X", CultureInfo.CurrentCulture) + " for classes (" + type.Name + ") and (" + recordClass.Name + ")");
				}
				dictionary[num] = GetRecordCreator(type);
			}
			return dictionary;
		}

		[Obsolete]
		private static void CheckZeros(Stream in1, int avail)
		{
			int num = 0;
			while (true)
			{
				int num2 = in1.ReadByte();
				if (num2 < 0)
				{
					break;
				}
				if (num2 != 0)
				{
					Console.Error.WriteLine(HexDump.ByteToHex(num2));
				}
				num++;
			}
			if (avail != num)
			{
				Console.Error.WriteLine("avail!=count (" + avail + "!=" + num + ").");
			}
		}

		private static I_RecordCreator GetRecordCreator(Type recClass)
		{
			try
			{
				ConstructorInfo constructor = recClass.GetConstructor(CONSTRUCTOR_ARGS);
				if (constructor != null)
				{
					return new ReflectionConstructorRecordCreator(constructor);
				}
			}
			catch (Exception)
			{
			}
			try
			{
				MethodInfo method = recClass.GetMethod("Create", CONSTRUCTOR_ARGS);
				return new ReflectionMethodRecordCreator(method);
			}
			catch (Exception)
			{
				throw new RuntimeException("Failed to find constructor or create method for (" + recClass.Name + ").");
			}
		}
	}
}
