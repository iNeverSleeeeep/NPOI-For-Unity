using NPOI.SS.Formula.PTG;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;

namespace NPOI.SS.Formula.Eval.Forked
{
	/// Represents a workbook being used for forked Evaluation. Most operations are delegated to the
	/// shared master workbook, except those that potentially involve cell values that may have been
	/// updated After a call to {@link #getOrCreateUpdatableCell(String, int, int)}.
	///
	/// @author Josh Micich
	internal class ForkedEvaluationWorkbook : IEvaluationWorkbook
	{
		private class OrderedSheet : IComparable<OrderedSheet>
		{
			private string _sheetName;

			private int _index;

			public OrderedSheet(string sheetName, int index)
			{
				_sheetName = sheetName;
				_index = index;
			}

			public string GetSheetName()
			{
				return _sheetName;
			}

			public int CompareTo(OrderedSheet o)
			{
				return _index - o._index;
			}
		}

		private IEvaluationWorkbook _masterBook;

		private Dictionary<string, ForkedEvaluationSheet> _sharedSheetsByName;

		public ForkedEvaluationWorkbook(IEvaluationWorkbook master)
		{
			_masterBook = master;
			_sharedSheetsByName = new Dictionary<string, ForkedEvaluationSheet>();
		}

		public ForkedEvaluationCell GetOrCreateUpdatableCell(string sheetName, int rowIndex, int columnIndex)
		{
			ForkedEvaluationSheet sharedSheet = GetSharedSheet(sheetName);
			return sharedSheet.GetOrCreateUpdatableCell(rowIndex, columnIndex);
		}

		public IEvaluationCell GetEvaluationCell(string sheetName, int rowIndex, int columnIndex)
		{
			ForkedEvaluationSheet sharedSheet = GetSharedSheet(sheetName);
			return sharedSheet.GetCell(rowIndex, columnIndex);
		}

		private ForkedEvaluationSheet GetSharedSheet(string sheetName)
		{
			ForkedEvaluationSheet forkedEvaluationSheet = null;
			if (_sharedSheetsByName.ContainsKey(sheetName))
			{
				forkedEvaluationSheet = _sharedSheetsByName[sheetName];
			}
			if (forkedEvaluationSheet == null)
			{
				forkedEvaluationSheet = new ForkedEvaluationSheet(_masterBook.GetSheet(_masterBook.GetSheetIndex(sheetName)));
				if (_sharedSheetsByName.ContainsKey(sheetName))
				{
					_sharedSheetsByName[sheetName] = forkedEvaluationSheet;
				}
				else
				{
					_sharedSheetsByName.Add(sheetName, forkedEvaluationSheet);
				}
			}
			return forkedEvaluationSheet;
		}

		public void CopyUpdatedCells(IWorkbook workbook)
		{
			string[] array = new string[_sharedSheetsByName.Count];
			_sharedSheetsByName.Keys.CopyTo(array, 0);
			OrderedSheet[] array2 = new OrderedSheet[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string sheetName = array[i];
				array2[i] = new OrderedSheet(sheetName, _masterBook.GetSheetIndex(sheetName));
			}
			for (int j = 0; j < array2.Length; j++)
			{
				string sheetName2 = array2[j].GetSheetName();
				ForkedEvaluationSheet forkedEvaluationSheet = _sharedSheetsByName[sheetName2];
				forkedEvaluationSheet.CopyUpdatedCells(workbook.GetSheet(sheetName2));
			}
		}

		public int ConvertFromExternSheetIndex(int externSheetIndex)
		{
			return _masterBook.ConvertFromExternSheetIndex(externSheetIndex);
		}

		public ExternalSheet GetExternalSheet(int externSheetIndex)
		{
			return _masterBook.GetExternalSheet(externSheetIndex);
		}

		public Ptg[] GetFormulaTokens(IEvaluationCell cell)
		{
			if (cell is ForkedEvaluationCell)
			{
				throw new Exception("Updated formulas not supported yet");
			}
			return _masterBook.GetFormulaTokens(cell);
		}

		public IEvaluationName GetName(NamePtg namePtg)
		{
			return _masterBook.GetName(namePtg);
		}

		public IEvaluationName GetName(string name, int sheetIndex)
		{
			return _masterBook.GetName(name, sheetIndex);
		}

		public IEvaluationSheet GetSheet(int sheetIndex)
		{
			return GetSharedSheet(GetSheetName(sheetIndex));
		}

		public ExternalName GetExternalName(int externSheetIndex, int externNameIndex)
		{
			return _masterBook.GetExternalName(externSheetIndex, externNameIndex);
		}

		public int GetSheetIndex(IEvaluationSheet sheet)
		{
			if (sheet is ForkedEvaluationSheet)
			{
				ForkedEvaluationSheet forkedEvaluationSheet = (ForkedEvaluationSheet)sheet;
				return forkedEvaluationSheet.GetSheetIndex(_masterBook);
			}
			return _masterBook.GetSheetIndex(sheet);
		}

		public int GetSheetIndex(string sheetName)
		{
			return _masterBook.GetSheetIndex(sheetName);
		}

		public string GetSheetName(int sheetIndex)
		{
			return _masterBook.GetSheetName(sheetIndex);
		}

		public string ResolveNameXText(NameXPtg ptg)
		{
			return _masterBook.ResolveNameXText(ptg);
		}

		public UDFFinder GetUDFFinder()
		{
			return _masterBook.GetUDFFinder();
		}
	}
}
