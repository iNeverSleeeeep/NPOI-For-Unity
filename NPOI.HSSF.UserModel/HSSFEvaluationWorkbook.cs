using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.SS;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;

namespace NPOI.HSSF.UserModel
{
	/// Internal POI use only
	///
	/// @author Josh Micich
	public class HSSFEvaluationWorkbook : IFormulaRenderingWorkbook, IEvaluationWorkbook, IFormulaParsingWorkbook
	{
		private class Name : IEvaluationName
		{
			private NameRecord _nameRecord;

			private int _index;

			public Ptg[] NameDefinition => _nameRecord.NameDefinition;

			public string NameText => _nameRecord.NameText;

			public bool HasFormula => _nameRecord.HasFormula;

			public bool IsFunctionName => _nameRecord.IsFunctionName;

			public bool IsRange => _nameRecord.HasFormula;

			public Name(NameRecord nameRecord, int index)
			{
				_nameRecord = nameRecord;
				_index = index;
			}

			public NamePtg CreatePtg()
			{
				return new NamePtg(_index);
			}
		}

		private static POILogger logger = POILogFactory.GetLogger(typeof(HSSFEvaluationWorkbook));

		private HSSFWorkbook _uBook;

		private InternalWorkbook _iBook;

		public static HSSFEvaluationWorkbook Create(IWorkbook book)
		{
			if (book == null)
			{
				return null;
			}
			return new HSSFEvaluationWorkbook((HSSFWorkbook)book);
		}

		private HSSFEvaluationWorkbook(HSSFWorkbook book)
		{
			_uBook = book;
			_iBook = book.Workbook;
		}

		public int GetExternalSheetIndex(string sheetName)
		{
			int sheetIndex = _uBook.GetSheetIndex(sheetName);
			return _iBook.CheckExternSheet(sheetIndex);
		}

		public int GetExternalSheetIndex(string workbookName, string sheetName)
		{
			return _iBook.GetExternalSheetIndex(workbookName, sheetName);
		}

		public ExternalName GetExternalName(int externSheetIndex, int externNameIndex)
		{
			return _iBook.GetExternalName(externSheetIndex, externNameIndex);
		}

		public NameXPtg GetNameXPtg(string name)
		{
			return _iBook.GetNameXPtg(name, _uBook.GetUDFFinder());
		}

		public IEvaluationName GetName(string name, int sheetIndex)
		{
			for (int i = 0; i < _iBook.NumNames; i++)
			{
				NameRecord nameRecord = _iBook.GetNameRecord(i);
				if (nameRecord.SheetNumber == sheetIndex + 1 && name.Equals(nameRecord.NameText, StringComparison.OrdinalIgnoreCase))
				{
					return new Name(nameRecord, i);
				}
			}
			if (sheetIndex != -1)
			{
				return GetName(name, -1);
			}
			return null;
		}

		public int GetSheetIndex(IEvaluationSheet evalSheet)
		{
			HSSFSheet hSSFSheet = ((HSSFEvaluationSheet)evalSheet).HSSFSheet;
			return _uBook.GetSheetIndex(hSSFSheet);
		}

		public int GetSheetIndex(string sheetName)
		{
			return _uBook.GetSheetIndex(sheetName);
		}

		public string GetSheetName(int sheetIndex)
		{
			return _uBook.GetSheetName(sheetIndex);
		}

		public IEvaluationSheet GetSheet(int sheetIndex)
		{
			return new HSSFEvaluationSheet((HSSFSheet)_uBook.GetSheetAt(sheetIndex));
		}

		public int ConvertFromExternSheetIndex(int externSheetIndex)
		{
			return _iBook.GetSheetIndexFromExternSheetIndex(externSheetIndex);
		}

		public ExternalSheet GetExternalSheet(int externSheetIndex)
		{
			return _iBook.GetExternalSheet(externSheetIndex);
		}

		public string ResolveNameXText(NameXPtg n)
		{
			return _iBook.ResolveNameXText(n.SheetRefIndex, n.NameIndex);
		}

		public string GetSheetNameByExternSheet(int externSheetIndex)
		{
			return _iBook.FindSheetNameFromExternSheet(externSheetIndex);
		}

		public string GetNameText(NamePtg namePtg)
		{
			return _iBook.GetNameRecord(namePtg.Index).NameText;
		}

		public IEvaluationName GetName(NamePtg namePtg)
		{
			int index = namePtg.Index;
			return new Name(_iBook.GetNameRecord(index), index);
		}

		public Ptg[] GetFormulaTokens(IEvaluationCell evalCell)
		{
			ICell hSSFCell = ((HSSFEvaluationCell)evalCell).HSSFCell;
			FormulaRecordAggregate formulaRecordAggregate = (FormulaRecordAggregate)((HSSFCell)hSSFCell).CellValueRecord;
			return formulaRecordAggregate.FormulaTokens;
		}

		public UDFFinder GetUDFFinder()
		{
			return _uBook.GetUDFFinder();
		}

		public SpreadsheetVersion GetSpreadsheetVersion()
		{
			return SpreadsheetVersion.EXCEL97;
		}
	}
}
