namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	/// Support for hyperbolic trig functions was Added as a part of
	/// Java distribution only in JDK1.5. This class uses custom
	/// naive implementation based on formulas at:
	/// http://www.math2.org/math/trig/hyperbolics.htm
	/// These formulas seem to agree with excel's implementation.
	public class Atanh : OneArg
	{
		public override double Evaluate(double d)
		{
			return MathX.atanh(d);
		}
	}
}
