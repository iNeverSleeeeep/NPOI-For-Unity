using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS;
using NPOI.SS.Formula;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;
using System;

namespace NPOI.XSSF.UserModel
{
	/// Internal POI use only
	///
	/// @author Josh Micich
	public class XSSFEvaluationWorkbook : IFormulaRenderingWorkbook, IEvaluationWorkbook, IFormulaParsingWorkbook
	{
		private class Name : IEvaluationName
		{
			private XSSFName _nameRecord;

			private int _index;

			private IFormulaParsingWorkbook _fpBook;

			public Ptg[] NameDefinition
			{
				get
				{
					return FormulaParser.Parse(_nameRecord.RefersToFormula, _fpBook, FormulaType.NamedRange, _nameRecord.SheetIndex);
				}
			}

			public string NameText
			{
				get
				{
					return _nameRecord.NameName;
				}
			}

			public bool HasFormula
			{
				get
				{
					CT_DefinedName cTName = _nameRecord.GetCTName();
					string value = cTName.Value;
					if (!cTName.function && value != null)
					{
						return value.Length > 0;
					}
					return false;
				}
			}

			public bool IsFunctionName
			{
				get
				{
					return _nameRecord.IsFunctionName;
				}
			}

			public bool IsRange
			{
				get
				{
					return HasFormula;
				}
			}

			public Name(IName name, int index, IFormulaParsingWorkbook fpBook)
			{
				_nameRecord = (XSSFName)name;
				_index = index;
				_fpBook = fpBook;
			}

			public NamePtg CreatePtg()
			{
				return new NamePtg(_index);
			}
		}

		private XSSFWorkbook _uBook;

		public static XSSFEvaluationWorkbook Create(IWorkbook book)
		{
			if (book == null)
			{
				return null;
			}
			return new XSSFEvaluationWorkbook(book);
		}

		private XSSFEvaluationWorkbook(IWorkbook book)
		{
			_uBook = (XSSFWorkbook)book;
		}

		private int ConvertFromExternalSheetIndex(int externSheetIndex)
		{
			return externSheetIndex;
		}

		/// @return the sheet index of the sheet with the given external index.
		public int ConvertFromExternSheetIndex(int externSheetIndex)
		{
			return externSheetIndex;
		}

		/// @return  the external sheet index of the sheet with the given internal
		/// index. Used by some of the more obscure formula and named range things.
		/// Fairly easy on XSSF (we think...) since the internal and external
		/// indicies are the same
		private int ConvertToExternalSheetIndex(int sheetIndex)
		{
			return sheetIndex;
		}

		public int GetExternalSheetIndex(string sheetName)
		{
			int sheetIndex = _uBook.GetSheetIndex(sheetName);
			return ConvertToExternalSheetIndex(sheetIndex);
		}

		public IEvaluationName GetName(string name, int sheetIndex)
		{
			for (int i = 0; i < _uBook.NumberOfNames; i++)
			{
				IName nameAt = _uBook.GetNameAt(i);
				string nameName = nameAt.NameName;
				if (name.Equals(nameName, StringComparison.InvariantCultureIgnoreCase) && nameAt.SheetIndex == sheetIndex)
				{
					return new Name(_uBook.GetNameAt(i), i, this);
				}
			}
			if (sheetIndex != -1)
			{
				return GetName(name, -1);
			}
			return null;
		}

		public int GetSheetIndex(IEvaluationSheet EvalSheet)
		{
			XSSFSheet xSSFSheet = ((XSSFEvaluationSheet)EvalSheet).GetXSSFSheet();
			return _uBook.GetSheetIndex(xSSFSheet);
		}

		public string GetSheetName(int sheetIndex)
		{
			return _uBook.GetSheetName(sheetIndex);
		}

		public ExternalName GetExternalName(int externSheetIndex, int externNameIndex)
		{
			throw new NotImplementedException();
		}

		public NameXPtg GetNameXPtg(string name)
		{
			IndexedUDFFinder indexedUDFFinder = (IndexedUDFFinder)GetUDFFinder();
			FreeRefFunction freeRefFunction = indexedUDFFinder.FindFunction(name);
			if (freeRefFunction == null)
			{
				return null;
			}
			return new NameXPtg(0, indexedUDFFinder.GetFunctionIndex(name));
		}

		public string ResolveNameXText(NameXPtg n)
		{
			int nameIndex = n.NameIndex;
			IndexedUDFFinder indexedUDFFinder = (IndexedUDFFinder)GetUDFFinder();
			return indexedUDFFinder.GetFunctionName(nameIndex);
		}

		public IEvaluationSheet GetSheet(int sheetIndex)
		{
			return new XSSFEvaluationSheet(_uBook.GetSheetAt(sheetIndex));
		}

		public ExternalSheet GetExternalSheet(int externSheetIndex)
		{
			return null;
		}

		public int GetExternalSheetIndex(string workbookName, string sheetName)
		{
			throw new Exception("not implemented yet");
		}

		public int GetSheetIndex(string sheetName)
		{
			return _uBook.GetSheetIndex(sheetName);
		}

		public string GetSheetNameByExternSheet(int externSheetIndex)
		{
			int sheetIx = ConvertFromExternalSheetIndex(externSheetIndex);
			return _uBook.GetSheetName(sheetIx);
		}

		public string GetNameText(NamePtg namePtg)
		{
			return _uBook.GetNameAt(namePtg.Index).NameName;
		}

		public IEvaluationName GetName(NamePtg namePtg)
		{
			int index = namePtg.Index;
			return new Name(_uBook.GetNameAt(index), index, this);
		}

		public Ptg[] GetFormulaTokens(IEvaluationCell EvalCell)
		{
			XSSFCell xSSFCell = ((XSSFEvaluationCell)EvalCell).GetXSSFCell();
			XSSFEvaluationWorkbook workbook = Create(_uBook);
			return FormulaParser.Parse(xSSFCell.CellFormula, workbook, FormulaType.Cell, _uBook.GetSheetIndex(xSSFCell.Sheet));
		}

		public UDFFinder GetUDFFinder()
		{
			return _uBook.GetUDFFinder();
		}

		/// XSSF allows certain extra textual characters in the formula that
		///  HSSF does not. As these can't be composed down to HSSF-compatible
		///  Ptgs, this method strips them out for us.
		private string CleanXSSFFormulaText(string text)
		{
			text = text.Replace("\\n", "").Replace("\\r", "");
			return text;
		}

		public SpreadsheetVersion GetSpreadsheetVersion()
		{
			return SpreadsheetVersion.EXCEL2007;
		}
	}
}
