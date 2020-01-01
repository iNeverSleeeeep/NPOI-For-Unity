using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using NPOI.Util;
using System;

namespace NPOI.HSSF.Record.Aggregates
{
	/// <summary>
	/// The formula record aggregate is used to join toGether the formula record and it's
	/// (optional) string record and (optional) Shared Formula Record (template Reads, excel optimization).
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	[Serializable]
	public class FormulaRecordAggregate : RecordAggregate, CellValueRecordInterface, IComparable, ICloneable
	{
		public const short sid = -2000;

		private FormulaRecord _formulaRecord;

		private SharedValueManager _sharedValueManager;

		/// caches the calculated result of the formula 
		private StringRecord _stringRecord;

		[NonSerialized]
		private SharedFormulaRecord _sharedFormulaRecord;

		public bool IsPartOfArrayFormula
		{
			get
			{
				if (_sharedFormulaRecord != null)
				{
					return false;
				}
				CellReference expReference = _formulaRecord.Formula.ExpReference;
				ArrayRecord arrayRecord = (expReference == null) ? null : _sharedValueManager.GetArrayRecord(expReference.Row, expReference.Col);
				return arrayRecord != null;
			}
		}

		/// <summary>
		/// Get the current Serialized size of the record. Should include the sid and recLength (4 bytes).
		/// </summary>
		/// <value>The size of the record.</value>
		public override int RecordSize => _formulaRecord.RecordSize + ((_stringRecord != null) ? _stringRecord.RecordSize : 0);

		/// <summary>
		/// return the non static version of the id for this record.
		/// </summary>
		/// <value>The sid.</value>
		public override short Sid => -2000;

		/// <summary>
		/// Gets or sets the formula record.
		/// </summary>
		/// <value>The formula record.</value>
		public FormulaRecord FormulaRecord
		{
			get
			{
				return _formulaRecord;
			}
			set
			{
				_formulaRecord = value;
			}
		}

		/// <summary>
		/// Gets or sets the string record.
		/// </summary>
		/// <value>The string record.</value>
		public StringRecord StringRecord
		{
			get
			{
				return _stringRecord;
			}
			set
			{
				_stringRecord = value;
			}
		}

		public short XFIndex
		{
			get
			{
				return _formulaRecord.XFIndex;
			}
			set
			{
				_formulaRecord.XFIndex = value;
			}
		}

		public int Column
		{
			get
			{
				return _formulaRecord.Column;
			}
			set
			{
				_formulaRecord.Column = value;
			}
		}

		public int Row
		{
			get
			{
				return _formulaRecord.Row;
			}
			set
			{
				_formulaRecord.Row = value;
			}
		}

		/// <summary>
		/// Gets the string value.
		/// </summary>
		/// <value>The string value.</value>
		public string StringValue
		{
			get
			{
				if (_stringRecord == null)
				{
					return null;
				}
				return _stringRecord.String;
			}
		}

		public Ptg[] FormulaTokens
		{
			get
			{
				if (_sharedFormulaRecord != null)
				{
					return _sharedFormulaRecord.GetFormulaTokens(_formulaRecord);
				}
				CellReference expReference = _formulaRecord.Formula.ExpReference;
				if (expReference != null)
				{
					ArrayRecord arrayRecord = _sharedValueManager.GetArrayRecord(expReference.Row, expReference.Col);
					return arrayRecord.FormulaTokens;
				}
				return _formulaRecord.ParsedExpression;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.Record.Aggregates.FormulaRecordAggregate" /> class.
		/// </summary>
		/// <param name="formulaRec">The formula rec.</param>
		/// <param name="stringRec">The string rec.</param>
		/// <param name="svm">The SVM.</param>
		public FormulaRecordAggregate(FormulaRecord formulaRec, StringRecord stringRec, SharedValueManager svm)
		{
			if (svm == null)
			{
				throw new ArgumentException("sfm must not be null");
			}
			if (formulaRec.HasCachedResultString)
			{
				if (stringRec == null)
				{
					throw new RecordFormatException("Formula record flag is set but String record was not found");
				}
				_stringRecord = stringRec;
			}
			else
			{
				_stringRecord = null;
			}
			_formulaRecord = formulaRec;
			_sharedValueManager = svm;
			if (formulaRec.IsSharedFormula)
			{
				CellReference expReference = formulaRec.Formula.ExpReference;
				if (expReference == null)
				{
					HandleMissingSharedFormulaRecord(formulaRec);
				}
				else
				{
					_sharedFormulaRecord = svm.LinkSharedFormulaRecord(expReference, this);
				}
			}
		}

		/// Should be called by any code which is either deleting this formula cell, or changing
		/// its type.  This method gives the aggregate a chance to unlink any shared formula
		/// that may be involved with this cell formula.
		public void NotifyFormulaChanging()
		{
			if (_sharedFormulaRecord != null)
			{
				_sharedValueManager.Unlink(_sharedFormulaRecord);
			}
		}

		/// <summary>
		/// called by the class that is responsible for writing this sucker.
		/// Subclasses should implement this so that their data is passed back in a
		/// byte array.
		/// </summary>
		/// <param name="offset">offset to begin writing at</param>
		/// <param name="data">byte array containing instance data.</param>
		/// <returns>number of bytes written</returns>
		public override int Serialize(int offset, byte[] data)
		{
			int num = offset + _formulaRecord.Serialize(offset, data);
			if (_stringRecord != null)
			{
				num += _stringRecord.Serialize(num, data);
			}
			return num - offset;
		}

		/// <summary>
		/// Visit each of the atomic BIFF records contained in this {@link RecordAggregate} in the order
		/// that they should be written to file.  Implementors may or may not return the actual
		/// {@link Record}s being used to manage POI's internal implementation.  Callers should not
		/// assume either way, and therefore only attempt to modify those {@link Record}s after cloning
		/// </summary>
		/// <param name="rv"></param>
		public override void VisitContainedRecords(RecordVisitor rv)
		{
			rv.VisitRecord(_formulaRecord);
			Record recordForFirstCell = _sharedValueManager.GetRecordForFirstCell(this);
			if (recordForFirstCell != null)
			{
				rv.VisitRecord(recordForFirstCell);
			}
			if (_formulaRecord.HasCachedResultString && _stringRecord != null)
			{
				rv.VisitRecord(_stringRecord);
			}
		}

		/// <summary>
		/// Sometimes the shared formula flag "seems" to be erroneously set (because the corresponding
		/// SharedFormulaRecord does not exist). Normally this would leave no way of determining
		/// the Ptg tokens for the formula.  However as it turns out in these
		/// cases, Excel encodes the unshared Ptg tokens in the right place (inside the FormulaRecord). 
		/// So the the only thing that needs to be done is to ignore the erroneous
		/// shared formula flag.
		///
		/// This method may also be used for setting breakpoints to help diagnose issues regarding the
		/// abnormally-set 'shared formula' flags.
		/// </summary>
		/// <param name="formula">The formula.</param>
		private static void HandleMissingSharedFormulaRecord(FormulaRecord formula)
		{
			Ptg ptg = formula.ParsedExpression[0];
			if (ptg is ExpPtg)
			{
				throw new RecordFormatException("SharedFormulaRecord not found for FormulaRecord with (isSharedFormula=true)");
			}
			formula.IsSharedFormula = false;
		}

		public int CompareTo(object o)
		{
			return _formulaRecord.CompareTo(o);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj" /> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			return _formulaRecord.Equals(obj);
		}

		public override int GetHashCode()
		{
			return _formulaRecord.GetHashCode();
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			return _formulaRecord.ToString();
		}

		public void SetCachedDoubleResult(double value)
		{
			_stringRecord = null;
			_formulaRecord.Value = value;
		}

		/// <summary>
		/// Sets the cached string result.
		/// </summary>
		/// <param name="value">The value.</param>
		public void SetCachedStringResult(string value)
		{
			if (_stringRecord == null)
			{
				_stringRecord = new StringRecord();
			}
			_stringRecord.String = value;
			if (value.Length < 1)
			{
				_formulaRecord.SetCachedResultTypeEmptyString();
			}
			else
			{
				_formulaRecord.SetCachedResultTypeString();
			}
		}

		/// <summary>
		/// Sets the cached boolean result.
		/// </summary>
		/// <param name="value">if set to <c>true</c> [value].</param>
		public void SetCachedBooleanResult(bool value)
		{
			_stringRecord = null;
			_formulaRecord.SetCachedResultBoolean(value);
		}

		/// <summary>
		/// Sets the cached error result.
		/// </summary>
		/// <param name="errorCode">The error code.</param>
		public void SetCachedErrorResult(int errorCode)
		{
			_stringRecord = null;
			_formulaRecord.SetCachedResultErrorCode(errorCode);
		}

		public object Clone()
		{
			return this;
		}

		/// Also checks for a related shared formula and unlinks it if found
		public void SetParsedExpression(Ptg[] ptgs)
		{
			NotifyFormulaChanging();
			_formulaRecord.ParsedExpression = ptgs;
		}

		public void UnlinkSharedFormula()
		{
			SharedFormulaRecord sharedFormulaRecord = _sharedFormulaRecord;
			if (sharedFormulaRecord == null)
			{
				throw new InvalidOperationException("Formula not linked to shared formula");
			}
			Ptg[] formulaTokens = sharedFormulaRecord.GetFormulaTokens(_formulaRecord);
			_formulaRecord.SetParsedExpression(formulaTokens);
			_formulaRecord.SetSharedFormula(flag: false);
			_sharedFormulaRecord = null;
		}

		public CellRangeAddress GetArrayFormulaRange()
		{
			if (_sharedFormulaRecord != null)
			{
				throw new InvalidOperationException("not an array formula cell.");
			}
			CellReference expReference = _formulaRecord.Formula.ExpReference;
			if (expReference == null)
			{
				throw new InvalidOperationException("not an array formula cell.");
			}
			ArrayRecord arrayRecord = _sharedValueManager.GetArrayRecord(expReference.Row, expReference.Col);
			if (arrayRecord == null)
			{
				throw new InvalidOperationException("ArrayRecord was not found for the locator " + expReference.FormatAsString());
			}
			CellRangeAddress8Bit range = arrayRecord.Range;
			return new CellRangeAddress(range.FirstRow, range.LastRow, range.FirstColumn, range.LastColumn);
		}

		public void SetArrayFormula(CellRangeAddress r, Ptg[] ptgs)
		{
			ArrayRecord ar = new ArrayRecord(Formula.Create(ptgs), new CellRangeAddress8Bit(r.FirstRow, r.LastRow, r.FirstColumn, r.LastColumn));
			_sharedValueManager.AddArrayRecord(ar);
		}

		/// Removes an array formula
		/// @return the range of the array formula containing the specified cell. Never <code>null</code>
		public CellRangeAddress RemoveArrayFormula(int rowIndex, int columnIndex)
		{
			CellRangeAddress8Bit cellRangeAddress8Bit = _sharedValueManager.RemoveArrayFormula(rowIndex, columnIndex);
			_formulaRecord.ParsedExpression = null;
			return new CellRangeAddress(cellRangeAddress8Bit.FirstRow, cellRangeAddress8Bit.LastRow, cellRangeAddress8Bit.FirstColumn, cellRangeAddress8Bit.LastColumn);
		}
	}
}
