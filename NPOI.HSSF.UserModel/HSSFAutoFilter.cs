using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.AutoFilter;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using System;

namespace NPOI.HSSF.UserModel
{
	public class HSSFAutoFilter : IAutoFilter
	{
		private FilterModeRecord filtermode;

		private HSSFSheet _sheet;

		public HSSFAutoFilter(HSSFSheet sheet)
		{
			_sheet = sheet;
		}

		public HSSFAutoFilter(string formula, HSSFWorkbook workbook)
		{
			Ptg[] array = HSSFFormulaParser.Parse(formula, workbook);
			if (!(array[0] is Area3DPtg))
			{
				throw new ArgumentException("incorrect formula");
			}
			Area3DPtg area3DPtg = (Area3DPtg)array[0];
			HSSFSheet hSSFSheet = (HSSFSheet)workbook.GetSheetAt(area3DPtg.ExternSheetIndex);
			int num = hSSFSheet.Sheet.FindFirstRecordLocBySid(85);
			CreateFilterModeRecord(hSSFSheet, num + 1);
			CreateAutoFilterInfoRecord(hSSFSheet, num + 2, area3DPtg);
			NameRecord nameRecord = workbook.Workbook.GetSpecificBuiltinRecord(13, area3DPtg.ExternSheetIndex + 1);
			if (nameRecord == null)
			{
				nameRecord = workbook.Workbook.CreateBuiltInName(13, area3DPtg.ExternSheetIndex + 1);
			}
			nameRecord.IsHiddenName = true;
			nameRecord.NameDefinition = array;
		}

		private void CreateFilterModeRecord(HSSFSheet sheet, int insertPos)
		{
			NPOI.HSSF.Record.Record record = sheet.Sheet.FindFirstRecordBySid(155);
			if (record == null)
			{
				filtermode = new FilterModeRecord();
				sheet.Sheet.Records.Insert(insertPos, filtermode);
			}
		}

		private void CreateAutoFilterInfoRecord(HSSFSheet sheet, int insertPos, Area3DPtg ptg)
		{
			NPOI.HSSF.Record.Record record = sheet.Sheet.FindFirstRecordBySid(157);
			AutoFilterInfoRecord autoFilterInfoRecord;
			if (record == null)
			{
				autoFilterInfoRecord = new AutoFilterInfoRecord();
				sheet.Sheet.Records.Insert(insertPos, autoFilterInfoRecord);
			}
			else
			{
				autoFilterInfoRecord = (record as AutoFilterInfoRecord);
			}
			autoFilterInfoRecord.NumEntries = (short)(ptg.LastColumn - ptg.FirstColumn + 1);
		}

		private void RemoveFilterModeRecord(HSSFSheet sheet)
		{
			if (filtermode != null)
			{
				sheet.Sheet.Records.Remove(filtermode);
			}
		}
	}
}
