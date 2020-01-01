using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the Excel function WEEKDAY
	///
	/// @author Thies Wellpott
	public class WeekdayFunc : Function
	{
		public static Function instance = new WeekdayFunc();

		private WeekdayFunc()
		{
		}

		/// * Perform WEEKDAY(date, returnOption) function.
		/// * Note: Parameter texts are from German EXCEL-2010 help.
		/// * Parameters in args[]:
		/// *  args[0] serialDate
		/// * EXCEL-date value
		/// * Standardmaessig ist der 1. Januar 1900 die fortlaufende Zahl 1 und
		/// * der 1. Januar 2008 die fortlaufende Zahl 39.448, da dieser Tag nach 39.448 Tagen
		/// * auf den 01.01.1900 folgt.
		/// * @return Option (optional)
		/// * Bestimmt den Rueckgabewert:
		///    1	oder nicht angegeben Zahl 1 (Sonntag) bis 7 (Samstag). Verhaelt sich wie fruehere Microsoft Excel-Versionen.
		///    2	Zahl 1 (Montag) bis 7 (Sonntag).
		///    3	Zahl 0 (Montag) bis 6 (Sonntag).
		///    11	Die Zahlen 1 (Montag) bis 7 (Sonntag)
		///    12	Die Zahlen 1 (Dienstag) bis 7 (Montag)
		///    13	Die Zahlen 1 (Mittwoch) bis 7 (Dienstag)
		///    14	Die Zahlen 1 (Donnerstag) bis 7 (Mittwoch)
		///    15	Die Zahlen 1 (Freitag) bis 7 (Donnerstag)
		///    16	Die Zahlen 1 (Samstag) bis 7 (Freitag)
		///    17	Die Zahlen 1 (Sonntag) bis 7 (Samstag)
		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			try
			{
				if (args.Length < 1 || args.Length > 2)
				{
					return ErrorEval.VALUE_INVALID;
				}
				ValueEval singleValue = OperandResolver.GetSingleValue(args[0], srcRowIndex, srcColumnIndex);
				double num = OperandResolver.CoerceValueToDouble(singleValue);
				if (!DateUtil.IsValidExcelDate(num))
				{
					return ErrorEval.NUM_ERROR;
				}
				int dayOfWeek = (int)DateUtil.GetJavaCalendar(num, use1904windowing: false).DayOfWeek;
				int num2 = 1;
				if (args.Length == 2)
				{
					ValueEval singleValue2 = OperandResolver.GetSingleValue(args[1], srcRowIndex, srcColumnIndex);
					if (singleValue2 == MissingArgEval.instance || singleValue2 == BlankEval.instance)
					{
						return ErrorEval.NUM_ERROR;
					}
					num2 = OperandResolver.CoerceValueToInt(singleValue2);
					if (num2 == 2)
					{
						num2 = 11;
					}
				}
				double value;
				if (num2 == 1)
				{
					value = (double)dayOfWeek;
				}
				else if (num2 == 3)
				{
					value = (double)((dayOfWeek + 6 - 1) % 7);
				}
				else
				{
					if (num2 < 11 || num2 > 17)
					{
						return ErrorEval.NUM_ERROR;
					}
					value = (double)((dayOfWeek + 6 - (num2 - 10)) % 7 + 1);
				}
				return new NumberEval(value);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
		}
	}
}
