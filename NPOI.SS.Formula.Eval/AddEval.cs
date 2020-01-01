namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	/// This is a documentation of the observed behaviour of 
	/// the '+' operator in Excel:
	///
	/// - 1+TRUE = 2
	/// - 1+FALSE = 1
	/// - 1+"true" = #VALUE!
	/// - 1+"1" = 2
	/// - 1+A1 = #VALUE if A1 Contains "1"
	/// - 1+A1 = 2 if A1 Contains ="1"
	/// - 1+A1 = 2 if A1 Contains TRUE or =TRUE
	/// - 1+A1 = #VALUE! if A1 Contains "TRUE" or ="TRUE"
	public class AddEval : TwoOperandNumericOperation
	{
		public override double Evaluate(double d0, double d1)
		{
			return d0 + d1;
		}
	}
}
