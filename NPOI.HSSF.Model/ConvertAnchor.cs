using NPOI.DDF;
using NPOI.HSSF.UserModel;
using System;

namespace NPOI.HSSF.Model
{
	public class ConvertAnchor
	{
		/// <summary>
		/// Creates the anchor.
		/// </summary>
		/// <param name="userAnchor">The user anchor.</param>
		/// <returns></returns>
		public static EscherRecord CreateAnchor(HSSFAnchor userAnchor)
		{
			if (userAnchor is HSSFClientAnchor)
			{
				HSSFClientAnchor hSSFClientAnchor = (HSSFClientAnchor)userAnchor;
				EscherClientAnchorRecord escherClientAnchorRecord = new EscherClientAnchorRecord();
				escherClientAnchorRecord.RecordId = -4080;
				escherClientAnchorRecord.Options = 0;
				escherClientAnchorRecord.Flag = (short)hSSFClientAnchor.AnchorType;
				escherClientAnchorRecord.Col1 = (short)Math.Min(hSSFClientAnchor.Col1, hSSFClientAnchor.Col2);
				escherClientAnchorRecord.Dx1 = (short)hSSFClientAnchor.Dx1;
				escherClientAnchorRecord.Row1 = (short)Math.Min(hSSFClientAnchor.Row1, hSSFClientAnchor.Row2);
				escherClientAnchorRecord.Dy1 = (short)hSSFClientAnchor.Dy1;
				escherClientAnchorRecord.Col2 = (short)Math.Max(hSSFClientAnchor.Col1, hSSFClientAnchor.Col2);
				escherClientAnchorRecord.Dx2 = (short)hSSFClientAnchor.Dx2;
				escherClientAnchorRecord.Row2 = (short)Math.Max(hSSFClientAnchor.Row1, hSSFClientAnchor.Row2);
				escherClientAnchorRecord.Dy2 = (short)hSSFClientAnchor.Dy2;
				return escherClientAnchorRecord;
			}
			HSSFChildAnchor hSSFChildAnchor = (HSSFChildAnchor)userAnchor;
			EscherChildAnchorRecord escherChildAnchorRecord = new EscherChildAnchorRecord();
			escherChildAnchorRecord.RecordId = -4081;
			escherChildAnchorRecord.Options = 0;
			escherChildAnchorRecord.Dx1 = (short)Math.Min(hSSFChildAnchor.Dx1, hSSFChildAnchor.Dx2);
			escherChildAnchorRecord.Dy1 = (short)Math.Min(hSSFChildAnchor.Dy1, hSSFChildAnchor.Dy2);
			escherChildAnchorRecord.Dx2 = (short)Math.Max(hSSFChildAnchor.Dx2, hSSFChildAnchor.Dx1);
			escherChildAnchorRecord.Dy2 = (short)Math.Max(hSSFChildAnchor.Dy2, hSSFChildAnchor.Dy1);
			return escherChildAnchorRecord;
		}
	}
}
