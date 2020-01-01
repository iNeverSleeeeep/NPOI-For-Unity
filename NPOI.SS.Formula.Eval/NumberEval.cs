using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using System;
using System.Globalization;
using System.Text;

namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class NumberEval : NumericValueEval, StringValueEval, ValueEval
	{
		public static readonly NumberEval ZERO = new NumberEval(0.0);

		private double _value;

		private string _stringValue;

		public double NumberValue => _value;

		public string StringValue
		{
			get
			{
				if (_stringValue == null)
				{
					_stringValue = NumberToTextConverter.ToText(_value);
				}
				return _stringValue;
			}
		}

		public NumberEval(Ptg ptg)
		{
			if (ptg is IntPtg)
			{
				_value = (double)((IntPtg)ptg).Value;
			}
			else if (ptg is NumberPtg)
			{
				_value = ((NumberPtg)ptg).Value;
			}
		}

		public NumberEval(double value)
		{
			_value = value;
		}

		protected void MakeString()
		{
			if (!double.IsNaN(_value))
			{
				double num = Math.Round(_value);
				if (num == _value)
				{
					_stringValue = num.ToString(CultureInfo.CurrentCulture);
				}
				else
				{
					_stringValue = _value.ToString(CultureInfo.CurrentCulture);
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(StringValue);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
