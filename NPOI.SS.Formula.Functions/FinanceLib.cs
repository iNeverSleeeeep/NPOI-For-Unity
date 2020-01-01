using System;

namespace NPOI.SS.Formula.Functions
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	///
	///
	/// This class Is a functon library for common fiscal functions.
	/// <b>Glossary of terms/abbreviations:</b>
	/// <br />
	/// <ul>
	/// <li><em>FV:</em> Future Value</li>
	/// <li><em>PV:</em> Present Value</li>
	/// <li><em>NPV:</em> Net Present Value</li>
	/// <li><em>PMT:</em> (Periodic) Payment</li>
	///
	/// </ul>
	/// For more info on the terms/abbreviations please use the references below 
	/// (hyperlinks are subject to Change):
	/// <br />Online References:
	/// <ol>
	/// <li>GNU Emacs Calc 2.02 Manual: http://theory.uwinnipeg.ca/gnu/calc/calc_203.html</li>
	/// <li>Yahoo Financial Glossary: http://biz.yahoo.com/f/g/nn.html#y</li>
	/// <li>MS Excel function reference: http://office.microsoft.com/en-us/assistance/CH062528251033.aspx</li>
	/// </ol>
	/// <h3>Implementation Notes:</h3>
	/// Symbols used in the formulae that follow:<br />
	/// <ul>
	/// <li>p: present value</li>
	/// <li>f: future value</li>
	/// <li>n: number of periods</li>
	/// <li>y: payment (in each period)</li>
	/// <li>r: rate</li>
	/// <li>^: the power operator (NOT the java bitwise XOR operator!)</li>
	/// </ul>
	/// [From MS Excel function reference] Following are some of the key formulas
	/// that are used in this implementation:
	/// <pre>
	/// p(1+r)^n + y(1+rt)((1+r)^n-1)/r + f=0   ...{when r!=0}
	/// ny + p + f=0                            ...{when r=0}
	/// </pre>
	public class FinanceLib
	{
		private FinanceLib()
		{
		}

		/// Future value of an amount given the number of payments, rate, amount
		/// of individual payment, present value and bool value indicating whether
		/// payments are due at the beginning of period 
		/// (false =&gt; payments are due at end of period) 
		/// @param r rate
		/// @param n num of periods
		/// @param y pmt per period
		/// @param p future value
		/// @param t type (true=pmt at end of period, false=pmt at begining of period)
		public static double fv(double r, double n, double y, double p, bool t)
		{
			double num = 0.0;
			if (r == 0.0)
			{
				return -1.0 * (p + n * y);
			}
			double num2 = r + 1.0;
			return (1.0 - Math.Pow(num2, n)) * (t ? num2 : 1.0) * y / r - p * Math.Pow(num2, n);
		}

		/// Present value of an amount given the number of future payments, rate, amount
		/// of individual payment, future value and bool value indicating whether
		/// payments are due at the beginning of period 
		/// (false =&gt; payments are due at end of period) 
		/// @param r
		/// @param n
		/// @param y
		/// @param f
		/// @param t
		public static double pv(double r, double n, double y, double f, bool t)
		{
			double num = 0.0;
			if (r == 0.0)
			{
				return -1.0 * (n * y + f);
			}
			double num2 = r + 1.0;
			return ((1.0 - Math.Pow(num2, n)) / r * (t ? num2 : 1.0) * y - f) / Math.Pow(num2, n);
		}

		/// calculates the Net Present Value of a principal amount
		/// given the disCount rate and a sequence of cash flows 
		/// (supplied as an array). If the amounts are income the value should 
		/// be positive, else if they are payments and not income, the 
		/// value should be negative.
		/// @param r
		/// @param cfs cashflow amounts
		public static double npv(double r, double[] cfs)
		{
			double num = 0.0;
			double num2 = r + 1.0;
			double num3 = num2;
			int i = 0;
			for (int num4 = cfs.Length; i < num4; i++)
			{
				num += cfs[i] / num3;
				num3 *= num2;
			}
			return num;
		}

		/// @param r
		/// @param n
		/// @param p
		/// @param f
		/// @param t
		public static double pmt(double r, double n, double p, double f, bool t)
		{
			double num = 0.0;
			if (r == 0.0)
			{
				return -1.0 * (f + p) / n;
			}
			double num2 = r + 1.0;
			return (f + p * Math.Pow(num2, n)) * r / ((t ? num2 : 1.0) * (1.0 - Math.Pow(num2, n)));
		}

		/// @param r
		/// @param y
		/// @param p
		/// @param f
		/// @param t
		public static double nper(double r, double y, double p, double f, bool t)
		{
			double num = 0.0;
			if (r == 0.0)
			{
				return -1.0 * (f + p) / y;
			}
			double num2 = r + 1.0;
			double num3 = (t ? num2 : 1.0) * y / r;
			double num4 = (num3 - f < 0.0) ? Math.Log(f - num3) : Math.Log(num3 - f);
			double num5 = (num3 - f < 0.0) ? Math.Log(0.0 - p - num3) : Math.Log(p + num3);
			double num6 = Math.Log(num2);
			return (num4 - num5) / num6;
		}
	}
}
