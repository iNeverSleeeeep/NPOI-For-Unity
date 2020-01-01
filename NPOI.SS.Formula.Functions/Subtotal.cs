using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the Excel function SUBTOTAL<p>
	///
	/// <b>Syntax :</b> <br />
	///  SUBTOTAL ( <b>functionCode</b>, <b>ref1</b>, ref2 ... ) <br />
	///    <table border="1" cellpadding="1" cellspacing="0" summary="Parameter descriptions">
	///      <tr><td><b>functionCode</b></td><td>(1-11) Selects the underlying aggregate function to be used (see table below)</td></tr>
	///      <tr><td><b>ref1</b>, ref2 ...</td><td>Arguments to be passed to the underlying aggregate function</td></tr>
	///    </table><br />
	/// </p>
	///
	///  <table border="1" cellpadding="1" cellspacing="0" summary="Parameter descriptions">
	///      <tr><th>functionCode</th><th>Aggregate Function</th></tr>
	///      <tr align="center"><td>1</td><td>AVERAGE</td></tr>
	///      <tr align="center"><td>2</td><td>COUNT</td></tr>
	///      <tr align="center"><td>3</td><td>COUNTA</td></tr>
	///      <tr align="center"><td>4</td><td>MAX</td></tr>
	///      <tr align="center"><td>5</td><td>MIN</td></tr>
	///      <tr align="center"><td>6</td><td>PRODUCT</td></tr>
	///      <tr align="center"><td>7</td><td>STDEV</td></tr>
	///      <tr align="center"><td>8</td><td>STDEVP *</td></tr>
	///      <tr align="center"><td>9</td><td>SUM</td></tr>
	///      <tr align="center"><td>10</td><td>VAR *</td></tr>
	///      <tr align="center"><td>11</td><td>VARP *</td></tr>
	///      <tr align="center"><td>101-111</td><td>*</td></tr>
	///  </table><br />
	/// * Not implemented in POI yet. Functions 101-111 are the same as functions 1-11 but with
	/// the option 'ignore hidden values'.
	/// <p />
	///
	/// @author Paul Tomlin &lt; pault at bulk sms dot com &gt;
	public class Subtotal : Function
	{
		private static Function FindFunction(int functionCode)
		{
			switch (functionCode)
			{
			case 1:
				return AggregateFunction.SubtotalInstance(AggregateFunction.AVERAGE);
			case 2:
				return Count.SubtotalInstance();
			case 3:
				return Counta.SubtotalInstance();
			case 4:
				return AggregateFunction.SubtotalInstance(AggregateFunction.MAX);
			case 5:
				return AggregateFunction.SubtotalInstance(AggregateFunction.MIN);
			case 6:
				return AggregateFunction.SubtotalInstance(AggregateFunction.PRODUCT);
			case 7:
				return AggregateFunction.SubtotalInstance(AggregateFunction.STDEV);
			case 8:
				throw new NotImplementedException("STDEVP");
			case 9:
				return AggregateFunction.SubtotalInstance(AggregateFunction.SUM);
			case 10:
				throw new NotImplementedException("VAR");
			case 11:
				throw new NotImplementedException("VARP");
			case 101:
			case 102:
			case 103:
			case 104:
			case 105:
			case 106:
			case 107:
			case 108:
			case 109:
			case 110:
			case 111:
				throw new NotImplementedException("SUBTOTAL - with 'exclude hidden values' option");
			default:
				throw EvaluationException.InvalidValue();
			}
		}

		public ValueEval Evaluate(ValueEval[] args, int srcRowIndex, int srcColumnIndex)
		{
			int num = args.Length - 1;
			if (num < 1)
			{
				return ErrorEval.VALUE_INVALID;
			}
			Function function;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(args[0], srcRowIndex, srcColumnIndex);
				int functionCode = OperandResolver.CoerceValueToInt(singleValue);
				function = FindFunction(functionCode);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			ValueEval[] array = new ValueEval[num];
			Array.Copy(args, 1, array, 0, num);
			return function.Evaluate(array, srcRowIndex, srcColumnIndex);
		}
	}
}
