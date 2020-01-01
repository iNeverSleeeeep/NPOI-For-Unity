using NPOI.SS.Formula.PTG;
using System.Text;

namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class BoolEval : NumericValueEval, StringValueEval, ValueEval
	{
		private bool value;

		public static readonly BoolEval FALSE = new BoolEval(value: false);

		public static readonly BoolEval TRUE = new BoolEval(value: true);

		public bool BooleanValue => value;

		public double NumberValue => (double)(value ? 1 : 0);

		public string StringValue
		{
			get
			{
				if (!value)
				{
					return "FALSE";
				}
				return "TRUE";
			}
		}

		/// Convenience method for the following:<br />
		/// <c>(b ? BoolEval.TRUE : BoolEval.FALSE)</c>
		/// @return a <c>BoolEval</c> instance representing <c>b</c>.
		public static BoolEval ValueOf(bool b)
		{
			if (!b)
			{
				return FALSE;
			}
			return TRUE;
		}

		public BoolEval(Ptg ptg)
		{
			value = ((BoolPtg)ptg).Value;
		}

		private BoolEval(bool value)
		{
			this.value = value;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(StringValue);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
