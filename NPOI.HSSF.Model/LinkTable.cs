using NPOI.HSSF.Record;
using NPOI.SS.Formula.PTG;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.Model
{
	/// Link Table (OOO pdf reference: 4.10.3 ) <p />
	///
	/// The main data of all types of references is stored in the Link Table inside the Workbook Globals
	/// Substream (4.2.5). The Link Table itself is optional and occurs only, if  there are any
	/// references in the document.
	///  <p />
	///
	///  In BIFF8 the Link Table consists of
	///  <ul>
	///  <li>zero or more EXTERNALBOOK Blocks<p />
	///  	each consisting of
	///  	<ul>
	///  	<li>exactly one EXTERNALBOOK (0x01AE) record</li>
	///  	<li>zero or more EXTERNALNAME (0x0023) records</li>
	///  	<li>zero or more CRN Blocks<p />
	///             			each consisting of
	///  		<ul>
	///  		<li>exactly one XCT (0x0059)record</li>
	///  		<li>zero or more CRN (0x005A) records (documentation says one or more)</li>
	///  		</ul>
	///  	</li>
	///  	</ul>
	///  </li>
	///  <li>zero or one EXTERNSHEET (0x0017) record</li>
	///  <li>zero or more DEFINEDNAME (0x0018) records</li>
	///  </ul>
	///
	///
	/// @author Josh Micich
	public class LinkTable
	{
		private class CRNBlock
		{
			private CRNCountRecord _countRecord;

			private CRNRecord[] _crns;

			public CRNBlock(RecordStream rs)
			{
				_countRecord = (CRNCountRecord)rs.GetNext();
				int numberOfCRNs = _countRecord.NumberOfCRNs;
				CRNRecord[] array = new CRNRecord[numberOfCRNs];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (CRNRecord)rs.GetNext();
				}
				_crns = array;
			}

			public CRNRecord[] GetCrns()
			{
				return (CRNRecord[])_crns.Clone();
			}
		}

		private class ExternalBookBlock
		{
			private SupBookRecord _externalBookRecord;

			private ExternalNameRecord[] _externalNameRecords;

			private CRNBlock[] _crnBlocks;

			public int NumberOfNames => _externalNameRecords.Length;

			/// Create a new block for registering add-in functions
			///
			/// @see org.apache.poi.hssf.model.LinkTable#addNameXPtg(String)
			public ExternalBookBlock()
			{
				_externalBookRecord = SupBookRecord.CreateAddInFunctions();
				_externalNameRecords = new ExternalNameRecord[0];
				_crnBlocks = new CRNBlock[0];
			}

			public ExternalBookBlock(RecordStream rs)
			{
				_externalBookRecord = (SupBookRecord)rs.GetNext();
				ArrayList arrayList = new ArrayList();
				while (rs.PeekNextClass() == typeof(ExternalNameRecord))
				{
					arrayList.Add(rs.GetNext());
				}
				_externalNameRecords = (ExternalNameRecord[])arrayList.ToArray(typeof(ExternalNameRecord));
				arrayList.Clear();
				while (rs.PeekNextClass() == typeof(CRNCountRecord))
				{
					arrayList.Add(new CRNBlock(rs));
				}
				_crnBlocks = (CRNBlock[])arrayList.ToArray(typeof(CRNBlock));
			}

			public int AddExternalName(ExternalNameRecord rec)
			{
				ExternalNameRecord[] array = new ExternalNameRecord[_externalNameRecords.Length + 1];
				Array.Copy(_externalNameRecords, 0, array, 0, _externalNameRecords.Length);
				array[array.Length - 1] = rec;
				_externalNameRecords = array;
				return _externalNameRecords.Length - 1;
			}

			/// Create a new block for internal references. It is called when constructing a new LinkTable.
			///
			/// @see org.apache.poi.hssf.model.LinkTable#LinkTable(int, WorkbookRecordList)
			public ExternalBookBlock(int numberOfSheets)
			{
				_externalBookRecord = SupBookRecord.CreateInternalReferences((short)numberOfSheets);
				_externalNameRecords = new ExternalNameRecord[0];
				_crnBlocks = new CRNBlock[0];
			}

			public SupBookRecord GetExternalBookRecord()
			{
				return _externalBookRecord;
			}

			public string GetNameText(int definedNameIndex)
			{
				return _externalNameRecords[definedNameIndex].Text;
			}

			/// Performs case-insensitive search
			/// @return -1 if not found
			public int GetIndexOfName(string name)
			{
				for (int i = 0; i < _externalNameRecords.Length; i++)
				{
					if (_externalNameRecords[i].Text.Equals(name, StringComparison.OrdinalIgnoreCase))
					{
						return i;
					}
				}
				return -1;
			}

			public int GetNameIx(int definedNameIndex)
			{
				return _externalNameRecords[definedNameIndex].Ix;
			}
		}

		private ExternalBookBlock[] _externalBookBlocks;

		private ExternSheetRecord _externSheetRecord;

		private List<NameRecord> _definedNames;

		private int _recordCount;

		private WorkbookRecordList _workbookRecordList;

		/// TODO - would not be required if calling code used RecordStream or similar
		public int RecordCount => _recordCount;

		public int NumNames => _definedNames.Count;

		private static ExternSheetRecord ReadExtSheetRecord(RecordStream rs)
		{
			List<ExternSheetRecord> list = new List<ExternSheetRecord>(2);
			while (rs.PeekNextClass() == typeof(ExternSheetRecord))
			{
				list.Add((ExternSheetRecord)rs.GetNext());
			}
			int count = list.Count;
			if (count < 1)
			{
				throw new Exception("Expected an EXTERNSHEET record but got (" + rs.PeekNextClass().Name + ")");
			}
			if (count == 1)
			{
				return list[0];
			}
			ExternSheetRecord[] array = new ExternSheetRecord[count];
			array = list.ToArray();
			return ExternSheetRecord.Combine(array);
		}

		public LinkTable(List<NPOI.HSSF.Record.Record> inputList, int startIndex, WorkbookRecordList workbookRecordList, Dictionary<string, NameCommentRecord> commentRecords)
		{
			_workbookRecordList = workbookRecordList;
			RecordStream recordStream = new RecordStream(inputList, startIndex);
			ArrayList arrayList = new ArrayList();
			while (recordStream.PeekNextClass() == typeof(SupBookRecord))
			{
				arrayList.Add(new ExternalBookBlock(recordStream));
			}
			_externalBookBlocks = (ExternalBookBlock[])arrayList.ToArray(typeof(ExternalBookBlock));
			arrayList.Clear();
			if (_externalBookBlocks.Length > 0)
			{
				if (recordStream.PeekNextClass() != typeof(ExternSheetRecord))
				{
					_externSheetRecord = null;
				}
				else
				{
					_externSheetRecord = ReadExtSheetRecord(recordStream);
				}
			}
			else
			{
				_externSheetRecord = null;
			}
			_definedNames = new List<NameRecord>();
			while (true)
			{
				Type left = recordStream.PeekNextClass();
				if (left == typeof(NameRecord))
				{
					NameRecord item = (NameRecord)recordStream.GetNext();
					_definedNames.Add(item);
				}
				else
				{
					if (!(left == typeof(NameCommentRecord)))
					{
						break;
					}
					NameCommentRecord nameCommentRecord = (NameCommentRecord)recordStream.GetNext();
					commentRecords[nameCommentRecord.NameText] = nameCommentRecord;
				}
			}
			_recordCount = recordStream.GetCountRead();
			for (int i = startIndex; i < startIndex + _recordCount; i++)
			{
				_workbookRecordList.Records.Add(inputList[i]);
			}
		}

		public LinkTable(int numberOfSheets, WorkbookRecordList workbookRecordList)
		{
			_workbookRecordList = workbookRecordList;
			_definedNames = new List<NameRecord>();
			_externalBookBlocks = new ExternalBookBlock[1]
			{
				new ExternalBookBlock(numberOfSheets)
			};
			_externSheetRecord = new ExternSheetRecord();
			_recordCount = 2;
			SupBookRecord externalBookRecord = _externalBookBlocks[0].GetExternalBookRecord();
			int num = FindFirstRecordLocBySid(140);
			if (num < 0)
			{
				throw new Exception("CountryRecord not found");
			}
			_workbookRecordList.Add(num + 1, _externSheetRecord);
			_workbookRecordList.Add(num + 1, externalBookRecord);
		}

		public NameRecord GetSpecificBuiltinRecord(byte builtInCode, int sheetNumber)
		{
			IEnumerator enumerator = _definedNames.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NameRecord nameRecord = (NameRecord)enumerator.Current;
				if (nameRecord.BuiltInName == builtInCode && nameRecord.SheetNumber == sheetNumber)
				{
					return nameRecord;
				}
			}
			return null;
		}

		public void RemoveBuiltinRecord(byte name, int sheetIndex)
		{
			NameRecord specificBuiltinRecord = GetSpecificBuiltinRecord(name, sheetIndex);
			if (specificBuiltinRecord != null)
			{
				_definedNames.Remove(specificBuiltinRecord);
			}
		}

		/// @param extRefIndex as from a {@link Ref3DPtg} or {@link Area3DPtg}
		/// @return -1 if the reference is to an external book
		public int GetIndexToInternalSheet(int extRefIndex)
		{
			return _externSheetRecord.GetFirstSheetIndexFromRefIndex(extRefIndex);
		}

		private int FindRefIndexFromExtBookIndex(int extBookIndex)
		{
			return _externSheetRecord.FindRefIndexFromExtBookIndex(extBookIndex);
		}

		public NameXPtg GetNameXPtg(string name)
		{
			for (int i = 0; i < _externalBookBlocks.Length; i++)
			{
				int indexOfName = _externalBookBlocks[i].GetIndexOfName(name);
				if (indexOfName >= 0)
				{
					int num = FindRefIndexFromExtBookIndex(i);
					if (num >= 0)
					{
						return new NameXPtg(num, indexOfName);
					}
				}
			}
			return null;
		}

		public NameRecord GetNameRecord(int index)
		{
			return _definedNames[index];
		}

		public void AddName(NameRecord name)
		{
			_definedNames.Add(name);
			int num = FindFirstRecordLocBySid(23);
			if (num == -1)
			{
				num = FindFirstRecordLocBySid(430);
			}
			if (num == -1)
			{
				num = FindFirstRecordLocBySid(140);
			}
			int count = _definedNames.Count;
			_workbookRecordList.Add(num + count, name);
		}

		/// Register an external name in this workbook
		///
		/// @param name  the name to register
		/// @return a NameXPtg describing this name 
		public NameXPtg AddNameXPtg(string name)
		{
			int externalBookIndex = -1;
			ExternalBookBlock externalBookBlock = null;
			for (int i = 0; i < _externalBookBlocks.Length; i++)
			{
				SupBookRecord externalBookRecord = _externalBookBlocks[i].GetExternalBookRecord();
				if (externalBookRecord.IsAddInFunctions)
				{
					externalBookBlock = _externalBookBlocks[i];
					externalBookIndex = i;
					break;
				}
			}
			if (externalBookBlock == null)
			{
				externalBookBlock = new ExternalBookBlock();
				ExternalBookBlock[] array = new ExternalBookBlock[_externalBookBlocks.Length + 1];
				Array.Copy(_externalBookBlocks, 0, array, 0, _externalBookBlocks.Length);
				array[array.Length - 1] = externalBookBlock;
				_externalBookBlocks = array;
				externalBookIndex = _externalBookBlocks.Length - 1;
				int pos = FindFirstRecordLocBySid(23);
				_workbookRecordList.Add(pos, externalBookBlock.GetExternalBookRecord());
				_externSheetRecord.AddRef(_externalBookBlocks.Length - 1, -2, -2);
			}
			ExternalNameRecord externalNameRecord = new ExternalNameRecord();
			externalNameRecord.Text = name;
			externalNameRecord.SetParsedExpression(new Ptg[1]
			{
				ErrPtg.REF_INVALID
			});
			int nameIndex = externalBookBlock.AddExternalName(externalNameRecord);
			int num = 0;
			IEnumerator enumerator = _workbookRecordList.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)enumerator.Current;
				if (record is SupBookRecord && ((SupBookRecord)record).IsAddInFunctions)
				{
					break;
				}
				num++;
			}
			int numberOfNames = externalBookBlock.NumberOfNames;
			_workbookRecordList.Add(num + numberOfNames, externalNameRecord);
			int refIxForSheet = _externSheetRecord.GetRefIxForSheet(externalBookIndex, -2);
			return new NameXPtg(refIxForSheet, nameIndex);
		}

		public void RemoveName(int namenum)
		{
			_definedNames.RemoveAt(namenum);
		}

		public int GetSheetIndexFromExternSheetIndex(int extRefIndex)
		{
			if (extRefIndex >= _externSheetRecord.NumOfRefs)
			{
				return -1;
			}
			return _externSheetRecord.GetFirstSheetIndexFromRefIndex(extRefIndex);
		}

		private static int GetSheetIndex(string[] sheetNames, string sheetName)
		{
			for (int i = 0; i < sheetNames.Length; i++)
			{
				if (sheetNames[i].Equals(sheetName))
				{
					return i;
				}
			}
			throw new InvalidOperationException("External workbook does not contain sheet '" + sheetName + "'");
		}

		public int GetExternalSheetIndex(string workbookName, string sheetName)
		{
			SupBookRecord supBookRecord = null;
			int num = -1;
			for (int i = 0; i < _externalBookBlocks.Length; i++)
			{
				SupBookRecord externalBookRecord = _externalBookBlocks[i].GetExternalBookRecord();
				if (externalBookRecord.IsExternalReferences && workbookName.Equals(externalBookRecord.URL))
				{
					supBookRecord = externalBookRecord;
					num = i;
					break;
				}
			}
			if (supBookRecord == null)
			{
				throw new NullReferenceException("No external workbook with name '" + workbookName + "'");
			}
			int sheetIndex = GetSheetIndex(supBookRecord.SheetNames, sheetName);
			int refIxForSheet = _externSheetRecord.GetRefIxForSheet(num, sheetIndex);
			if (refIxForSheet < 0)
			{
				throw new InvalidOperationException("ExternSheetRecord does not contain combination (" + num + ", " + sheetIndex + ")");
			}
			return refIxForSheet;
		}

		public string[] GetExternalBookAndSheetName(int extRefIndex)
		{
			int extbookIndexFromRefIndex = _externSheetRecord.GetExtbookIndexFromRefIndex(extRefIndex);
			SupBookRecord externalBookRecord = _externalBookBlocks[extbookIndexFromRefIndex].GetExternalBookRecord();
			if (!externalBookRecord.IsExternalReferences)
			{
				return null;
			}
			int firstSheetIndexFromRefIndex = _externSheetRecord.GetFirstSheetIndexFromRefIndex(extRefIndex);
			string text = null;
			if (firstSheetIndexFromRefIndex >= 0)
			{
				text = (string)externalBookRecord.SheetNames.GetValue(firstSheetIndexFromRefIndex);
			}
			return new string[2]
			{
				externalBookRecord.URL,
				text
			};
		}

		public int CheckExternSheet(int sheetIndex)
		{
			int num = -1;
			for (int i = 0; i < _externalBookBlocks.Length; i++)
			{
				SupBookRecord externalBookRecord = _externalBookBlocks[i].GetExternalBookRecord();
				if (externalBookRecord.IsInternalReferences)
				{
					num = i;
					break;
				}
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Could not find 'internal references' EXTERNALBOOK");
			}
			int refIxForSheet = _externSheetRecord.GetRefIxForSheet(num, sheetIndex);
			if (refIxForSheet >= 0)
			{
				return refIxForSheet;
			}
			return _externSheetRecord.AddRef(num, sheetIndex, sheetIndex);
		}

		/// copied from Workbook
		private int FindFirstRecordLocBySid(short sid)
		{
			int num = 0;
			IEnumerator enumerator = _workbookRecordList.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)enumerator.Current;
				if (record.Sid == sid)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		public string ResolveNameXText(int refIndex, int definedNameIndex)
		{
			int extbookIndexFromRefIndex = _externSheetRecord.GetExtbookIndexFromRefIndex(refIndex);
			return _externalBookBlocks[extbookIndexFromRefIndex].GetNameText(definedNameIndex);
		}

		public int ResolveNameXIx(int refIndex, int definedNameIndex)
		{
			int extbookIndexFromRefIndex = _externSheetRecord.GetExtbookIndexFromRefIndex(refIndex);
			return _externalBookBlocks[extbookIndexFromRefIndex].GetNameIx(definedNameIndex);
		}

		/// Changes an external referenced file to another file.
		/// A formular in Excel which refers a cell in another file is saved in two parts: 
		/// The referenced file is stored in an reference table. the row/cell information is saved separate.
		/// This method invokation will only change the reference in the lookup-table itself.
		/// @param oldUrl The old URL to search for and which is to be replaced
		/// @param newUrl The URL replacement
		/// @return true if the oldUrl was found and replaced with newUrl. Otherwise false
		public bool ChangeExternalReference(string oldUrl, string newUrl)
		{
			ExternalBookBlock[] externalBookBlocks = _externalBookBlocks;
			foreach (ExternalBookBlock externalBookBlock in externalBookBlocks)
			{
				SupBookRecord externalBookRecord = externalBookBlock.GetExternalBookRecord();
				if (externalBookRecord.IsExternalReferences && externalBookRecord.URL.Equals(oldUrl))
				{
					externalBookRecord.URL = newUrl;
					return true;
				}
			}
			return false;
		}
	}
}
