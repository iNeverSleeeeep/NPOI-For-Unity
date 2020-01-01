using NPOI.OpenXmlFormats.Dml;
using System;

namespace NPOI.XSSF.UserModel
{
	/// @author Yegor Kozlov
	public class XSSFChildAnchor : XSSFAnchor
	{
		private CT_Transform2D t2d;

		public override int Dx1
		{
			get
			{
				return (int)t2d.off.x;
			}
			set
			{
				t2d.off.y = value;
			}
		}

		public override int Dy1
		{
			get
			{
				return (int)t2d.off.y;
			}
			set
			{
				t2d.off.y = value;
			}
		}

		public override int Dy2
		{
			get
			{
				return (int)(Dy1 + t2d.ext.cy);
			}
			set
			{
				t2d.ext.cy = value - Dy1;
			}
		}

		public override int Dx2
		{
			get
			{
				return (int)(Dx1 + t2d.ext.cx);
			}
			set
			{
				t2d.ext.cx = value - Dx1;
			}
		}

		public XSSFChildAnchor(int x, int y, int cx, int cy)
		{
			t2d = new CT_Transform2D();
			CT_Point2D cT_Point2D = t2d.AddNewOff();
			CT_PositiveSize2D cT_PositiveSize2D = t2d.AddNewExt();
			cT_Point2D.x = x;
			cT_Point2D.y = y;
			cT_PositiveSize2D.cx = Math.Abs(cx - x);
			cT_PositiveSize2D.cy = Math.Abs(cy - y);
			if (x > cx)
			{
				t2d.flipH = true;
			}
			if (y > cy)
			{
				t2d.flipV = true;
			}
		}

		public XSSFChildAnchor(CT_Transform2D t2d)
		{
			this.t2d = t2d;
		}

		public CT_Transform2D GetCTTransform2D()
		{
			return t2d;
		}
	}
}
