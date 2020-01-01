using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the Excel function TIME
	///
	/// @author Steven Butler (sebutler @ gmail dot com)
	///
	/// Based on POI {@link DateFunc}
	public class TimeFunc : Fixed3ArgFunction
	{
		private const int SECONDS_PER_MINUTE = 60;

		private const int SECONDS_PER_HOUR = 3600;

		private const int HOURS_PER_DAY = 24;

		private const int SECONDS_PER_DAY = 86400;

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0, ValueEval arg1, ValueEval arg2)
		{
			double value;
			try
			{
				value = Evaluate(EvalArg(arg0, srcRowIndex, srcColumnIndex), EvalArg(arg1, srcRowIndex, srcColumnIndex), EvalArg(arg2, srcRowIndex, srcColumnIndex));
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return new NumberEval(value);
		}

		private int EvalArg(ValueEval arg, int srcRowIndex, int srcColumnIndex)
		{
			if (arg == MissingArgEval.instance)
			{
				return 0;
			}
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, srcRowIndex, srcColumnIndex);
			return OperandResolver.CoerceValueToInt(singleValue);
		}

		/// Converts the supplied hours, minutes and seconds to an Excel time value.
		///
		///
		/// @param ds array of 3 doubles Containing hours, minutes and seconds.
		/// Non-integer inputs are tRuncated to an integer before further calculation
		/// of the time value.
		/// @return An Excel representation of a time of day.
		/// If the time value represents more than a day, the days are Removed from
		/// the result, leaving only the time of day component.
		/// @throws NPOI.SS.Formula.Eval.EvaluationException
		/// If any of the arguments are greater than 32767 or the hours
		/// minutes and seconds when combined form a time value less than 0, the function
		/// Evaluates to an error.
		private double Evaluate(int hours, int minutes, int seconds)
		{
			if (hours > 32767 || minutes > 32767 || seconds > 32767)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			int num = hours * 3600 + minutes * 60 + seconds;
			if (num < 0)
			{
				throw new EvaluationException(ErrorEval.VALUE_INVALID);
			}
			return (double)(num % 86400) / 86400.0;
		}
	}
}
