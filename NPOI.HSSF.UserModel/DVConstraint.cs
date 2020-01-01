using NPOI.HSSF.Model;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Globalization;
using System.Text;

namespace NPOI.HSSF.UserModel
{
	/// @author Josh Micich
	public class DVConstraint : IDataValidationConstraint
	{
		public class FormulaPair
		{
			private Ptg[] _formula1;

			private Ptg[] _formula2;

			public Ptg[] Formula1 => _formula1;

			public Ptg[] Formula2 => _formula2;

			public FormulaPair(Ptg[] formula1, Ptg[] formula2)
			{
				_formula1 = formula1;
				_formula2 = formula2;
			}
		}

		private int _validationType;

		private int _operator;

		private string[] _explicitListValues;

		private string _formula1;

		private string _formula2;

		private double _value1;

		private double _value2;

		/// Convenience method
		/// @return <c>true</c> if this constraint is a 'list' validation
		public bool IsListValidationType => _validationType == 3;

		/// Convenience method
		/// @return <c>true</c> if this constraint is a 'list' validation with explicit values
		public bool IsExplicitList
		{
			get
			{
				if (_validationType == 3)
				{
					return _explicitListValues != null;
				}
				return false;
			}
		}

		public int Operator
		{
			get
			{
				return _operator;
			}
			set
			{
				_operator = value;
			}
		}

		public string[] ExplicitListValues
		{
			get
			{
				return _explicitListValues;
			}
			set
			{
				if (_validationType != 3)
				{
					throw new InvalidOperationException("Cannot setExplicitListValues on non-list constraint");
				}
				_formula1 = null;
				_explicitListValues = value;
			}
		}

		public string Formula1
		{
			get
			{
				return _formula1;
			}
			set
			{
				_value1 = double.NaN;
				_explicitListValues = null;
				_formula1 = value;
			}
		}

		public string Formula2
		{
			get
			{
				return _formula2;
			}
			set
			{
				_value2 = double.NaN;
				_formula2 = value;
			}
		}

		/// @return the numeric value for expression 1. May be <c>null</c>
		public double Value1
		{
			get
			{
				return _value1;
			}
			set
			{
				_formula1 = null;
				_value1 = value;
			}
		}

		/// @return the numeric value for expression 2. May be <c>null</c>
		public double Value2
		{
			get
			{
				return _value2;
			}
			set
			{
				_formula2 = null;
				_value2 = value;
			}
		}

		private DVConstraint(int validationType, int comparisonOperator, string formulaA, string formulaB, double value1, double value2, string[] excplicitListValues)
		{
			_validationType = validationType;
			_operator = comparisonOperator;
			_formula1 = formulaA;
			_formula2 = formulaB;
			_value1 = value1;
			_value2 = value2;
			_explicitListValues = excplicitListValues;
		}

		/// Creates a list constraint
		private DVConstraint(string listFormula, string[] excplicitListValues)
			: this(3, 0, listFormula, null, double.NaN, double.NaN, excplicitListValues)
		{
		}

		/// Creates a number based data validation constraint. The text values entered for expr1 and expr2
		/// can be either standard Excel formulas or formatted number values. If the expression starts 
		/// with '=' it is Parsed as a formula, otherwise it is Parsed as a formatted number. 
		///
		/// @param validationType one of {@link NPOI.SS.UserModel.DataValidationConstraint.ValidationType#ANY},
		/// {@link NPOI.SS.UserModel.DataValidationConstraint.ValidationType#DECIMAL},
		/// {@link NPOI.SS.UserModel.DataValidationConstraint.ValidationType#INTEGER},
		/// {@link NPOI.SS.UserModel.DataValidationConstraint.ValidationType#TEXT_LENGTH}
		/// @param comparisonOperator any constant from {@link NPOI.SS.UserModel.DataValidationConstraint.OperatorType} enum
		/// @param expr1 date formula (when first char is '=') or formatted number value
		/// @param expr2 date formula (when first char is '=') or formatted number value
		public static DVConstraint CreateNumericConstraint(int validationType, int comparisonOperator, string expr1, string expr2)
		{
			switch (validationType)
			{
			case 0:
				if (expr1 != null || expr2 != null)
				{
					throw new ArgumentException("expr1 and expr2 must be null for validation type 'any'");
				}
				break;
			case 1:
			case 2:
			case 6:
				if (expr1 == null)
				{
					throw new ArgumentException("expr1 must be supplied");
				}
				OperatorType.ValidateSecondArg(comparisonOperator, expr2);
				break;
			default:
				throw new ArgumentException("Validation Type (" + validationType + ") not supported with this method");
			}
			string formulaFromTextExpression = GetFormulaFromTextExpression(expr1);
			double value = (formulaFromTextExpression == null) ? ConvertNumber(expr1) : double.NaN;
			string formulaFromTextExpression2 = GetFormulaFromTextExpression(expr2);
			double value2 = (formulaFromTextExpression2 == null) ? ConvertNumber(expr2) : double.NaN;
			return new DVConstraint(validationType, comparisonOperator, formulaFromTextExpression, formulaFromTextExpression2, value, value2, null);
		}

		public static DVConstraint CreateFormulaListConstraint(string listFormula)
		{
			return new DVConstraint(listFormula, null);
		}

		public static DVConstraint CreateExplicitListConstraint(string[] explicitListValues)
		{
			return new DVConstraint(null, explicitListValues);
		}

		/// Creates a time based data validation constraint. The text values entered for expr1 and expr2
		/// can be either standard Excel formulas or formatted time values. If the expression starts 
		/// with '=' it is Parsed as a formula, otherwise it is Parsed as a formatted time.  To parse 
		/// formatted times, two formats are supported:  "HH:MM" or "HH:MM:SS".  This is contrary to 
		/// Excel which uses the default time format from the OS.
		///
		/// @param comparisonOperator constant from {@link NPOI.SS.UserModel.DataValidationConstraint.OperatorType} enum
		/// @param expr1 date formula (when first char is '=') or formatted time value
		/// @param expr2 date formula (when first char is '=') or formatted time value
		public static DVConstraint CreateTimeConstraint(int comparisonOperator, string expr1, string expr2)
		{
			if (expr1 == null)
			{
				throw new ArgumentException("expr1 must be supplied");
			}
			OperatorType.ValidateSecondArg(comparisonOperator, expr1);
			string formulaFromTextExpression = GetFormulaFromTextExpression(expr1);
			double value = (formulaFromTextExpression == null) ? ConvertTime(expr1) : double.NaN;
			string formulaFromTextExpression2 = GetFormulaFromTextExpression(expr2);
			double value2 = (formulaFromTextExpression2 == null) ? ConvertTime(expr2) : double.NaN;
			return new DVConstraint(5, comparisonOperator, formulaFromTextExpression, formulaFromTextExpression2, value, value2, null);
		}

		/// Creates a date based data validation constraint. The text values entered for expr1 and expr2
		/// can be either standard Excel formulas or formatted date values. If the expression starts 
		/// with '=' it is Parsed as a formula, otherwise it is Parsed as a formatted date (Excel uses 
		/// the same convention).  To parse formatted dates, a date format needs to be specified.  This
		/// is contrary to Excel which uses the default short date format from the OS.
		///
		/// @param comparisonOperator constant from {@link NPOI.SS.UserModel.DataValidationConstraint.OperatorType} enum
		/// @param expr1 date formula (when first char is '=') or formatted date value
		/// @param expr2 date formula (when first char is '=') or formatted date value
		/// @param dateFormat ignored if both expr1 and expr2 are formulas.  Default value is "YYYY/MM/DD"
		/// otherwise any other valid argument for <c>SimpleDateFormat</c> can be used
		/// @see <a href="http://java.sun.com/j2se/1.5.0/docs/api/java/text/DateFormat.html">SimpleDateFormat</a>
		public static DVConstraint CreateDateConstraint(int comparisonOperator, string expr1, string expr2, string dateFormat)
		{
			if (expr1 == null)
			{
				throw new ArgumentException("expr1 must be supplied");
			}
			OperatorType.ValidateSecondArg(comparisonOperator, expr2);
			SimpleDateFormat dateFormat2 = (dateFormat == null) ? null : new SimpleDateFormat(dateFormat);
			string formulaFromTextExpression = GetFormulaFromTextExpression(expr1);
			double value = (formulaFromTextExpression == null) ? ConvertDate(expr1, dateFormat2) : double.NaN;
			string formulaFromTextExpression2 = GetFormulaFromTextExpression(expr2);
			double value2 = (formulaFromTextExpression2 == null) ? ConvertDate(expr2, dateFormat2) : double.NaN;
			return new DVConstraint(4, comparisonOperator, formulaFromTextExpression, formulaFromTextExpression2, value, value2, null);
		}

		/// Distinguishes formula expressions from simple value expressions.  This logic is only 
		/// required by a few factory methods in this class that create data validation constraints
		/// from more or less the same parameters that would have been entered in the Excel UI.  The
		/// data validation dialog box uses the convention that formulas begin with '='.  Other methods
		/// in this class follow the POI convention (formulas and values are distinct), so the '=' 
		/// convention is not used there.
		///
		/// @param textExpr a formula or value expression
		/// @return all text After '=' if textExpr begins with '='. Otherwise <code>null</code> if textExpr does not begin with '='
		private static string GetFormulaFromTextExpression(string textExpr)
		{
			if (textExpr == null)
			{
				return null;
			}
			if (textExpr.Length < 1)
			{
				throw new ArgumentException("Empty string is not a valid formula/value expression");
			}
			if (textExpr[0] == '=')
			{
				return textExpr.Substring(1);
			}
			return null;
		}

		/// @return <code>null</code> if numberStr is <code>null</code>
		private static double ConvertNumber(string numberStr)
		{
			if (numberStr != null)
			{
				try
				{
					return double.Parse(numberStr, CultureInfo.CurrentCulture);
				}
				catch (FormatException)
				{
					throw new InvalidOperationException("The supplied text '" + numberStr + "' could not be parsed as a number");
				}
			}
			return double.NaN;
		}

		/// @return <code>null</code> if timeStr is <code>null</code>
		private static double ConvertTime(string timeStr)
		{
			if (timeStr == null)
			{
				return double.NaN;
			}
			return DateUtil.ConvertTime(timeStr);
		}

		/// @param dateFormat pass <code>null</code> for default YYYYMMDD
		/// @return <code>null</code> if timeStr is <code>null</code>
		private static double ConvertDate(string dateStr, SimpleDateFormat dateFormat)
		{
			if (dateStr == null)
			{
				return double.NaN;
			}
			DateTime date;
			if (dateFormat != null)
			{
				try
				{
					date = DateTime.Parse(dateStr, CultureInfo.CurrentCulture);
				}
				catch (FormatException innerException)
				{
					throw new InvalidOperationException("Failed to parse date '" + dateStr + "' using specified format '" + dateFormat + "'", innerException);
				}
			}
			else
			{
				date = DateUtil.ParseYYYYMMDDDate(dateStr);
			}
			return DateUtil.GetExcelDate(date);
		}

		public static DVConstraint CreateCustomFormulaConstraint(string formula)
		{
			if (formula == null)
			{
				throw new ArgumentException("formula must be supplied");
			}
			return new DVConstraint(7, 0, formula, null, double.NaN, double.NaN, null);
		}

		public int GetValidationType()
		{
			return _validationType;
		}

		/// @return both Parsed formulas (for expression 1 and 2). 
		public FormulaPair CreateFormulas(HSSFSheet sheet)
		{
			Ptg[] formula;
			Ptg[] formula2;
			if (IsListValidationType)
			{
				formula = CreateListFormula(sheet);
				formula2 = Ptg.EMPTY_PTG_ARRAY;
			}
			else
			{
				formula = ConvertDoubleFormula(_formula1, _value1, sheet);
				formula2 = ConvertDoubleFormula(_formula2, _value2, sheet);
			}
			return new FormulaPair(formula, formula2);
		}

		private Ptg[] CreateListFormula(HSSFSheet sheet)
		{
			if (_explicitListValues == null)
			{
				IWorkbook workbook = sheet.Workbook;
				return HSSFFormulaParser.Parse(_formula1, (HSSFWorkbook)workbook, FormulaType.DataValidationList, workbook.GetSheetIndex(sheet));
			}
			StringBuilder stringBuilder = new StringBuilder(_explicitListValues.Length * 16);
			for (int i = 0; i < _explicitListValues.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append('\0');
				}
				stringBuilder.Append(_explicitListValues[i]);
			}
			return new Ptg[1]
			{
				new StringPtg(stringBuilder.ToString())
			};
		}

		/// @return The Parsed token array representing the formula or value specified. 
		/// Empty array if both formula and value are <code>null</code>
		private static Ptg[] ConvertDoubleFormula(string formula, double value, HSSFSheet sheet)
		{
			if (formula == null)
			{
				if (double.IsNaN(value))
				{
					return Ptg.EMPTY_PTG_ARRAY;
				}
				return new Ptg[1]
				{
					new NumberPtg(value)
				};
			}
			if (!double.IsNaN(value))
			{
				throw new InvalidOperationException("Both formula and value cannot be present");
			}
			IWorkbook workbook = sheet.Workbook;
			return HSSFFormulaParser.Parse(formula, (HSSFWorkbook)workbook, FormulaType.Cell, workbook.GetSheetIndex(sheet));
		}
	}
}
