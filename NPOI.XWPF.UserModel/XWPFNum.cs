using NPOI.OpenXmlFormats.Wordprocessing;

namespace NPOI.XWPF.UserModel
{
	/// @author Philipp Epp
	public class XWPFNum
	{
		private CT_Num ctNum;

		protected XWPFNumbering numbering;

		public XWPFNum()
		{
			ctNum = null;
			numbering = null;
		}

		public XWPFNum(CT_Num ctNum)
		{
			this.ctNum = ctNum;
			numbering = null;
		}

		public XWPFNum(XWPFNumbering numbering)
		{
			ctNum = null;
			this.numbering = numbering;
		}

		public XWPFNum(CT_Num ctNum, XWPFNumbering numbering)
		{
			this.ctNum = ctNum;
			this.numbering = numbering;
		}

		public XWPFNumbering GetNumbering()
		{
			return numbering;
		}

		public CT_Num GetCTNum()
		{
			return ctNum;
		}

		public void SetNumbering(XWPFNumbering numbering)
		{
			this.numbering = numbering;
		}

		public void SetCTNum(CT_Num ctNum)
		{
			this.ctNum = ctNum;
		}
	}
}
