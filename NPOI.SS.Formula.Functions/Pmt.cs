namespace NPOI.SS.Formula.Functions
{
	/// Implementation for the PMT() Excel function.<p />
	///
	/// <b>Syntax:</b><br />
	/// <b>PMT</b>(<b>rate</b>, <b>nper</b>, <b>pv</b>, fv, type)<p />
	///
	/// Returns the constant repayment amount required for a loan assuming a constant interest rate.<p />
	///
	/// <b>rate</b> the loan interest rate.<br />
	/// <b>nper</b> the number of loan repayments.<br />
	/// <b>pv</b> the present value of the future payments (or principle).<br />
	/// <b>fv</b> the future value (default zero) surplus cash at the end of the loan lifetime.<br />
	/// <b>type</b> whether payments are due at the beginning(1) or end(0 - default) of each payment period.<br />
	public class Pmt : FinanceFunction
	{
		public override double Evaluate(double rate, double arg1, double arg2, double arg3, bool type)
		{
			return FinanceLib.pmt(rate, arg1, arg2, arg3, type);
		}
	}
}
