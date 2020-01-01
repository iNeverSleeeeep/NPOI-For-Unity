using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using System.Collections;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Excel can Get cranky if you give it files containing too
	/// many (especially duplicate) objects, and this class can
	/// help to avoid those.
	/// In general, it's much better to make sure you don't
	/// duplicate the objects in your code, as this is likely
	/// to be much faster than creating lots and lots of
	/// excel objects+records, only to optimise them down to
	/// many fewer at a later stage.
	/// However, sometimes this is too hard / tricky to do, which
	/// is where the use of this class comes in.
	/// </summary>
	public class HSSFOptimiser
	{
		/// <summary>
		/// Goes through the Workbook, optimising the fonts by
		/// removing duplicate ones.
		/// For now, only works on fonts used in HSSFCellStyle
		/// and HSSFRichTextString. Any other font uses
		/// (eg charts, pictures) may well end up broken!
		/// This can be a slow operation, especially if you have
		/// lots of cells, cell styles or rich text strings
		/// </summary>
		/// <param name="workbook">The workbook in which to optimise the fonts</param>
		public static void OptimiseFonts(HSSFWorkbook workbook)
		{
			short[] array = new short[workbook.Workbook.NumberOfFontRecords + 1];
			bool[] array2 = new bool[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (short)i;
				array2[i] = false;
			}
			FontRecord[] array3 = new FontRecord[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				if (j != 4)
				{
					array3[j] = workbook.Workbook.GetFontRecordAt(j);
				}
			}
			for (int k = 5; k < array.Length; k++)
			{
				int num = -1;
				for (int l = 0; l < k; l++)
				{
					if (num != -1)
					{
						break;
					}
					if (l != 4)
					{
						FontRecord fontRecordAt = workbook.Workbook.GetFontRecordAt(l);
						if (fontRecordAt.SameProperties(array3[k]))
						{
							num = l;
						}
					}
				}
				if (num != -1)
				{
					array[k] = (short)num;
					array2[k] = true;
				}
			}
			for (int m = 5; m < array.Length; m++)
			{
				short num2 = array[m];
				short num3 = num2;
				for (int n = 0; n < num2; n++)
				{
					if (array2[n])
					{
						num3 = (short)(num3 - 1);
					}
				}
				array[m] = num3;
			}
			for (int num4 = 5; num4 < array.Length; num4++)
			{
				if (array2[num4])
				{
					workbook.Workbook.RemoveFontRecord(array3[num4]);
				}
			}
			workbook.ResetFontCache();
			for (int num5 = 0; num5 < workbook.Workbook.NumExFormats; num5++)
			{
				ExtendedFormatRecord exFormatAt = workbook.Workbook.GetExFormatAt(num5);
				exFormatAt.FontIndex = array[exFormatAt.FontIndex];
			}
			ArrayList arrayList = new ArrayList();
			for (int num6 = 0; num6 < workbook.NumberOfSheets; num6++)
			{
				ISheet sheetAt = workbook.GetSheetAt(num6);
				foreach (IRow item in sheetAt)
				{
					foreach (ICell item2 in item)
					{
						if (item2.CellType == CellType.String)
						{
							HSSFRichTextString hSSFRichTextString = (HSSFRichTextString)item2.RichStringCellValue;
							UnicodeString rawUnicodeString = hSSFRichTextString.RawUnicodeString;
							if (!arrayList.Contains(rawUnicodeString))
							{
								for (short num7 = 5; num7 < array.Length; num7 = (short)(num7 + 1))
								{
									if (num7 != array[num7])
									{
										rawUnicodeString.SwapFontUse(num7, array[num7]);
									}
								}
								arrayList.Add(rawUnicodeString);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Goes through the Wokrbook, optimising the cell styles
		/// by removing duplicate ones and ones that aren't used.
		/// For best results, optimise the fonts via a call to
		/// OptimiseFonts(HSSFWorkbook) first
		/// </summary>
		/// <param name="workbook">The workbook in which to optimise the cell styles</param>
		public static void OptimiseCellStyles(HSSFWorkbook workbook)
		{
			short[] array = new short[workbook.Workbook.NumExFormats];
			bool[] array2 = new bool[array.Length];
			bool[] array3 = new bool[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = false;
				array[i] = (short)i;
				array3[i] = false;
			}
			ExtendedFormatRecord[] array4 = new ExtendedFormatRecord[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array4[j] = workbook.Workbook.GetExFormatAt(j);
			}
			for (int k = 21; k < array.Length; k++)
			{
				int num = -1;
				for (int l = 0; l < k; l++)
				{
					if (num != -1)
					{
						break;
					}
					ExtendedFormatRecord exFormatAt = workbook.Workbook.GetExFormatAt(l);
					if (exFormatAt.Equals(array4[k]))
					{
						num = l;
					}
				}
				if (num != -1)
				{
					array[k] = (short)num;
					array3[k] = true;
				}
			}
			for (int m = 0; m < workbook.NumberOfSheets; m++)
			{
				HSSFSheet hSSFSheet = (HSSFSheet)workbook.GetSheetAt(m);
				foreach (IRow item in hSSFSheet)
				{
					foreach (ICell item2 in item)
					{
						HSSFCell hSSFCell = (HSSFCell)item2;
						short xFIndex = hSSFCell.CellValueRecord.XFIndex;
						array2[xFIndex] = true;
					}
				}
			}
			for (int n = 21; n < array2.Length; n++)
			{
				if (!array2[n])
				{
					array3[n] = true;
					array[n] = 0;
				}
			}
			for (int num2 = 21; num2 < array.Length; num2++)
			{
				short num3 = array[num2];
				short num4 = num3;
				for (int num5 = 0; num5 < num3; num5++)
				{
					if (array3[num5])
					{
						num4 = (short)(num4 - 1);
					}
				}
				array[num2] = num4;
			}
			int num6 = array.Length;
			int num7 = 0;
			for (int num8 = 21; num8 < num6; num8++)
			{
				if (array3[num8 + num7])
				{
					workbook.Workbook.RemoveExFormatRecord(num8);
					num8--;
					num6--;
					num7++;
				}
			}
			for (int num9 = 0; num9 < workbook.NumberOfSheets; num9++)
			{
				HSSFSheet hSSFSheet2 = (HSSFSheet)workbook.GetSheetAt(num9);
				foreach (IRow item3 in hSSFSheet2)
				{
					foreach (ICell item4 in item3)
					{
						short xFIndex2 = ((HSSFCell)item4).CellValueRecord.XFIndex;
						ICellStyle cellStyle = item4.CellStyle = workbook.GetCellStyleAt(array[xFIndex2]);
					}
				}
			}
		}
	}
}
