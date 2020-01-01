using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using System;
using System.Text;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// High Level Represantion of Named Range
	/// </summary>
	/// <remarks>@author Libin Roman (Vista Portal LDT. Developer)</remarks>
	public class HSSFName : IName
	{
		private HSSFWorkbook book;

		private NameRecord _definedNameRec;

		private NameCommentRecord _commentRec;

		/// <summary>
		/// Gets or sets the sheets name which this named range is referenced to
		/// </summary>
		/// <value>sheet name, which this named range refered to</value>
		public string SheetName
		{
			get
			{
				int externSheetNumber = _definedNameRec.ExternSheetNumber;
				return book.Workbook.FindSheetNameFromExternSheet(externSheetNumber);
			}
		}

		/// <summary>
		/// Gets or sets the name of the named range
		/// </summary>
		/// <value>named range name</value>
		public string NameName
		{
			get
			{
				return _definedNameRec.NameText;
			}
			set
			{
				ValidateName(value);
				_definedNameRec.NameText = value;
				InternalWorkbook workbook = book.Workbook;
				int sheetNumber = _definedNameRec.SheetNumber;
				for (int num = workbook.NumNames - 1; num >= 0; num--)
				{
					NameRecord nameRecord = workbook.GetNameRecord(num);
					if (nameRecord != _definedNameRec && nameRecord.NameText.Equals(NameName, StringComparison.OrdinalIgnoreCase) && sheetNumber == nameRecord.SheetNumber)
					{
						string message = "The " + ((sheetNumber == 0) ? "workbook" : "sheet") + " already contains this name: " + value;
						_definedNameRec.NameText = value + "(2)";
						throw new ArgumentException(message);
					}
				}
				if (_commentRec != null)
				{
					string nameText = _commentRec.NameText;
					_commentRec.NameText = value;
					book.Workbook.UpdateNameCommentRecordCache(_commentRec);
				}
			}
		}

		public string RefersToFormula
		{
			get
			{
				if (_definedNameRec.IsFunctionName)
				{
					throw new InvalidOperationException("Only applicable to named ranges");
				}
				Ptg[] nameDefinition = _definedNameRec.NameDefinition;
				if (nameDefinition.Length < 1)
				{
					return null;
				}
				return HSSFFormulaParser.ToFormulaString(book, nameDefinition);
			}
			set
			{
				Ptg[] nameDefinition = HSSFFormulaParser.Parse(value, book, FormulaType.NamedRange, SheetIndex);
				_definedNameRec.NameDefinition = nameDefinition;
			}
		}

		/// Returns the sheet index this name applies to.
		///
		/// @return the sheet index this name applies to, -1 if this name applies to the entire workbook
		public int SheetIndex
		{
			get
			{
				return _definedNameRec.SheetNumber - 1;
			}
			set
			{
				int num = book.NumberOfSheets - 1;
				if (value < -1 || value > num)
				{
					throw new ArgumentException("Sheet index (" + value + ") is out of range" + ((num == -1) ? "" : (" (0.." + num + ")")));
				}
				_definedNameRec.SheetNumber = value + 1;
			}
		}

		public string Comment
		{
			get
			{
				if (_commentRec != null && _commentRec.CommentText != null && _commentRec.CommentText.Length > 0)
				{
					return _commentRec.CommentText;
				}
				return _definedNameRec.DescriptionText;
			}
			set
			{
				_definedNameRec.DescriptionText = value;
			}
		}

		/// <summary>
		/// Tests if this name points to a cell that no longer exists
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the name refers to a deleted cell; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted
		{
			get
			{
				Ptg[] nameDefinition = _definedNameRec.NameDefinition;
				return Ptg.DoesFormulaReferToDeletedCell(nameDefinition);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is function name.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is function name; otherwise, <c>false</c>.
		/// </value>
		public bool IsFunctionName => _definedNameRec.IsFunctionName;

		internal HSSFName(HSSFWorkbook book, NameRecord name)
			: this(book, name, null)
		{
		}

		/// <summary>
		/// Creates new HSSFName   - called by HSSFWorkbook to Create a sheet from
		/// scratch.
		/// </summary>
		/// <param name="book">lowlevel Workbook object associated with the sheet.</param>
		/// <param name="name">the Name Record</param>
		/// <param name="comment"></param>
		internal HSSFName(HSSFWorkbook book, NameRecord name, NameCommentRecord comment)
		{
			this.book = book;
			_definedNameRec = name;
			_commentRec = comment;
		}

		private void ValidateName(string name)
		{
			if (name.Length == 0)
			{
				throw new ArgumentException("Name cannot be blank");
			}
			char c = name[0];
			if ((c != '_' && !char.IsLetter(c)) || name.IndexOf(' ') != -1)
			{
				throw new ArgumentException("Invalid name: '" + name + "'; Names must begin with a letter or underscore and not contain spaces");
			}
		}

		/// <summary>
		/// Sets the NameParsedFormula structure that specifies the formula for the defined name.
		/// </summary>
		/// <param name="ptgs">the sequence of {@link Ptg}s for the formula.</param>
		public void SetNameDefinition(Ptg[] ptgs)
		{
			_definedNameRec.NameDefinition = ptgs;
		}

		/// Indicates that the defined name refers to a user-defined function.
		/// This attribute is used when there is an add-in or other code project associated with the file.
		///
		/// @param value <c>true</c> indicates the name refers to a function.
		public void SetFunction(bool value)
		{
			_definedNameRec.SetFunction(value);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(_definedNameRec.NameText);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
