using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Drawing;

namespace NPOI.HSSF.UserModel
{
	/// Translates Graphics calls into escher calls.  The translation Is lossy so
	/// many features are not supported and some just aren't implemented yet.  If
	/// in doubt test the specific calls you wish to make. Graphics calls are
	/// always performed into an EscherGroup so one will need to be Created.
	///
	/// <b>Important:</b>
	/// <blockquote>
	/// One important concept worth considering Is that of font size.  One of the
	/// difficulties in Converting Graphics calls into escher Drawing calls Is that
	/// Excel does not have the concept of absolute pixel positions.  It measures
	/// it's cell widths in 'Chars' and the cell heights in points.
	/// Unfortunately it's not defined exactly what a type of Char it's
	/// measuring.  Presumably this Is due to the fact that the Excel will be
	/// using different fonts on different platforms or even within the same
	/// platform.
	///
	/// Because of this constraint we've had to calculate the
	/// verticalPointsPerPixel.  This the amount the font should be scaled by when
	/// you Issue commands such as DrawString().  A good way to calculate this
	/// Is to use the follow formula:
	///
	/// <pre>
	///      multipler = GroupHeightInPoints / heightOfGroup
	/// </pre>
	///
	/// The height of the Group Is calculated fairly simply by calculating the
	/// difference between the y coordinates of the bounding box of the shape.  The
	/// height of the Group can be calculated by using a convenience called
	/// <c>HSSFClientAnchor.GetAnchorHeightInPoints()</c>.
	/// </blockquote>
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	public class EscherGraphics : IDisposable
	{
		private HSSFShapeGroup escherGroup;

		private HSSFWorkbook workbook;

		private float verticalPointsPerPixel = 1f;

		private float verticalPixelsPerPoint;

		private Color foreground;

		private Color background = Color.White;

		private Font font;

		private static POILogger Logger = POILogFactory.GetLogger(typeof(EscherGraphics));

		public Rectangle Clip => ClipBounds;

		public Rectangle ClipBounds => Rectangle.Empty;

		public Color Color => foreground;

		public Font Font => font;

		public Color Background
		{
			get
			{
				return background;
			}
			set
			{
				background = value;
			}
		}

		/// Construct an escher graphics object.
		///
		/// @param escherGroup           The escher Group to Write the graphics calls into.
		/// @param workbook              The workbook we are using.
		/// @param forecolor             The foreground color to use as default.
		/// @param verticalPointsPerPixel    The font multiplier.  (See class description for information on how this works.).
		public EscherGraphics(HSSFShapeGroup escherGroup, HSSFWorkbook workbook, Color forecolor, float verticalPointsPerPixel)
		{
			this.escherGroup = escherGroup;
			this.workbook = workbook;
			this.verticalPointsPerPixel = verticalPointsPerPixel;
			verticalPixelsPerPoint = 1f / verticalPointsPerPixel;
			font = new Font("Arial", 10f);
			foreground = forecolor;
		}

		/// Constructs an escher graphics object.
		///
		/// @param escherGroup           The escher Group to Write the graphics calls into.
		/// @param workbook              The workbook we are using.
		/// @param foreground            The foreground color to use as default.
		/// @param verticalPointsPerPixel    The font multiplier.  (See class description for information on how this works.).
		/// @param font                  The font to use.
		private EscherGraphics(HSSFShapeGroup escherGroup, HSSFWorkbook workbook, Color foreground, Font font, float verticalPointsPerPixel)
		{
			this.escherGroup = escherGroup;
			this.workbook = workbook;
			this.foreground = foreground;
			this.font = font;
			this.verticalPointsPerPixel = verticalPointsPerPixel;
			verticalPixelsPerPoint = 1f / verticalPointsPerPixel;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && font != null)
			{
				font.Dispose();
				font = null;
			}
		}

		public void ClearRect(int x, int y, int width, int height)
		{
			Color color = foreground;
			SetColor(background);
			FillRect(x, y, width, height);
			SetColor(color);
		}

		public void ClipRect(int x, int y, int width, int height)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "clipRect not supported");
			}
		}

		public void CopyArea(int x, int y, int width, int height, int dx, int dy)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "copyArea not supported");
			}
		}

		public EscherGraphics Create()
		{
			return new EscherGraphics(escherGroup, workbook, foreground, font, verticalPointsPerPixel);
		}

		public void DrawArc(int x, int y, int width, int height, int startAngle, int arcAngle)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "DrawArc not supported");
			}
		}

		public bool DrawImage(Image img, int dx1, int dy1, int dx2, int dy2, int sx1, int sy1, int sx2, int sy2, Color bgcolor)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "DrawImage not supported");
			}
			throw new NotImplementedException();
		}

		public bool DrawImage(Image img, int dx1, int dy1, int dx2, int dy2, int sx1, int sy1, int sx2, int sy2)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "DrawImage not supported");
			}
			throw new NotImplementedException();
		}

		public bool DrawImage(Image image, int i, int j, int k, int l, Color color)
		{
			return DrawImage(image, i, j, i + k, j + l, 0, 0, image.Width, image.Height, color);
		}

		public bool DrawImage(Image image, int i, int j, int k, int l)
		{
			return DrawImage(image, i, j, i + k, j + l, 0, 0, image.Width, image.Height);
		}

		public bool DrawImage(Image image, int i, int j, Color color)
		{
			return DrawImage(image, i, j, image.Width, image.Height, color);
		}

		public bool DrawImage(Image image, int i, int j)
		{
			return DrawImage(image, i, j, image.Width, image.Height);
		}

		public void DrawLine(int x1, int y1, int x2, int y2)
		{
			DrawLine(x1, y1, x2, y2, 0);
		}

		public void DrawLine(int x1, int y1, int x2, int y2, int width)
		{
			HSSFSimpleShape hSSFSimpleShape = escherGroup.CreateShape(new HSSFChildAnchor(x1, y1, x2, y2));
			hSSFSimpleShape.ShapeType = 20;
			hSSFSimpleShape.LineWidth = width;
			hSSFSimpleShape.SetLineStyleColor(foreground.R, foreground.G, foreground.B);
		}

		public void DrawOval(int x, int y, int width, int height)
		{
			HSSFSimpleShape hSSFSimpleShape = escherGroup.CreateShape(new HSSFChildAnchor(x, y, x + width, y + height));
			hSSFSimpleShape.ShapeType = 3;
			hSSFSimpleShape.LineWidth = 0;
			hSSFSimpleShape.SetLineStyleColor(foreground.R, foreground.G, foreground.B);
			hSSFSimpleShape.IsNoFill = true;
		}

		public void DrawPolygon(int[] xPoints, int[] yPoints, int nPoints)
		{
			int num = FindBiggest(xPoints);
			int num2 = FindBiggest(yPoints);
			int num3 = FindSmallest(xPoints);
			int num4 = FindSmallest(yPoints);
			HSSFPolygon hSSFPolygon = escherGroup.CreatePolygon(new HSSFChildAnchor(num3, num4, num, num2));
			hSSFPolygon.SetPolygonDrawArea(num - num3, num2 - num4);
			hSSFPolygon.SetPoints(AddToAll(xPoints, -num3), AddToAll(yPoints, -num4));
			hSSFPolygon.SetLineStyleColor(foreground.R, foreground.G, foreground.B);
			hSSFPolygon.LineWidth = 0;
			hSSFPolygon.IsNoFill = true;
		}

		private int[] AddToAll(int[] values, int amount)
		{
			int[] array = new int[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = values[i] + amount;
			}
			return array;
		}

		public void DrawPolyline(int[] xPoints, int[] yPoints, int nPoints)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "DrawPolyline not supported");
			}
		}

		public void DrawRect(int x, int y, int width, int height)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "DrawRect not supported");
			}
		}

		public void DrawRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "DrawRoundRect not supported");
			}
		}

		public void DrawString(string str, int x, int y)
		{
			if (!string.IsNullOrEmpty(str))
			{
				using (Font font = new Font(this.font.Name.Equals("SansSerif") ? "Arial" : this.font.Name, (float)(int)(this.font.Size / verticalPixelsPerPoint), this.font.Style))
				{
					FontDetails fontDetails = StaticFontMetrics.GetFontDetails(font);
					int num = fontDetails.GetStringWidth(str) * 8 + 12;
					int num2 = (int)(this.font.Size / verticalPixelsPerPoint + 6f) * 2;
					y -= Convert.ToInt32(this.font.Size / verticalPixelsPerPoint + 2f * verticalPixelsPerPoint);
					HSSFTextbox hSSFTextbox = escherGroup.CreateTextbox(new HSSFChildAnchor(x, y, x + num, y + num2));
					hSSFTextbox.IsNoFill = true;
					hSSFTextbox.LineStyle = LineStyle.None;
					HSSFRichTextString hSSFRichTextString = new HSSFRichTextString(str);
					HSSFFont hSSFFont = MatchFont(font);
					hSSFRichTextString.ApplyFont(hSSFFont);
					hSSFTextbox.String = hSSFRichTextString;
				}
			}
		}

		private HSSFFont MatchFont(Font font)
		{
			HSSFColor hSSFColor = workbook.GetCustomPalette().FindColor(foreground.R, foreground.G, foreground.B);
			if (hSSFColor == null)
			{
				hSSFColor = workbook.GetCustomPalette().FindSimilarColor(foreground.R, foreground.G, foreground.B);
			}
			bool bold = font.Bold;
			bool italic = font.Italic;
			HSSFFont hSSFFont = (HSSFFont)workbook.FindFont((short)(bold ? 700 : 400), hSSFColor.Indexed, (short)(font.Size * 20f), font.Name, italic, strikeout: false, FontSuperScript.None, FontUnderlineType.None);
			if (hSSFFont == null)
			{
				hSSFFont = (HSSFFont)workbook.CreateFont();
				hSSFFont.Boldweight = (short)(bold ? 700 : 0);
				hSSFFont.Color = hSSFColor.Indexed;
				hSSFFont.FontHeight = (double)(short)(font.Size * 20f);
				hSSFFont.FontName = font.Name;
				hSSFFont.IsItalic = italic;
				hSSFFont.IsStrikeout = false;
				hSSFFont.TypeOffset = FontSuperScript.None;
				hSSFFont.Underline = FontUnderlineType.None;
			}
			return hSSFFont;
		}

		public void FillArc(int x, int y, int width, int height, int startAngle, int arcAngle)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "FillArc not supported");
			}
		}

		public void FillOval(int x, int y, int width, int height)
		{
			HSSFSimpleShape hSSFSimpleShape = escherGroup.CreateShape(new HSSFChildAnchor(x, y, x + width, y + height));
			hSSFSimpleShape.ShapeType = 3;
			hSSFSimpleShape.LineStyle = LineStyle.None;
			hSSFSimpleShape.SetFillColor(foreground.R, foreground.G, foreground.B);
			hSSFSimpleShape.SetLineStyleColor(foreground.R, foreground.G, foreground.B);
			hSSFSimpleShape.IsNoFill = false;
		}

		/// Fills a (closed) polygon, as defined by a pair of arrays, which
		///  hold the <i>x</i> and <i>y</i> coordinates.
		///
		/// This Draws the polygon, with <c>nPoint</c> line segments.
		/// The first <c>nPoint - 1</c> line segments are
		///  Drawn between sequential points 
		///  (<c>xPoints[i],yPoints[i],xPoints[i+1],yPoints[i+1]</c>).
		/// The line segment Is a closing one, from the last point to 
		///  the first (assuming they are different).
		///
		/// The area inside of the polygon Is defined by using an
		///  even-odd Fill rule (also known as the alternating rule), and 
		///  the area inside of it Is Filled.
		/// @param xPoints array of the <c>x</c> coordinates.
		/// @param yPoints array of the <c>y</c> coordinates.
		/// @param nPoints the total number of points in the polygon.
		/// @see   java.awt.Graphics#DrawPolygon(int[], int[], int)
		public void FillPolygon(int[] xPoints, int[] yPoints, int nPoints)
		{
			int num = FindBiggest(xPoints);
			int num2 = FindBiggest(yPoints);
			int num3 = FindSmallest(xPoints);
			int num4 = FindSmallest(yPoints);
			HSSFPolygon hSSFPolygon = escherGroup.CreatePolygon(new HSSFChildAnchor(num3, num4, num, num2));
			hSSFPolygon.SetPolygonDrawArea(num - num3, num2 - num4);
			hSSFPolygon.SetPoints(AddToAll(xPoints, -num3), AddToAll(yPoints, -num4));
			hSSFPolygon.SetLineStyleColor(foreground.R, foreground.G, foreground.B);
			hSSFPolygon.SetFillColor(foreground.R, foreground.G, foreground.B);
		}

		private int FindBiggest(int[] values)
		{
			int num = -2147483648;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] > num)
				{
					num = values[i];
				}
			}
			return num;
		}

		private int FindSmallest(int[] values)
		{
			int num = 2147483647;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] < num)
				{
					num = values[i];
				}
			}
			return num;
		}

		public void FillRect(int x, int y, int width, int height)
		{
			HSSFSimpleShape hSSFSimpleShape = escherGroup.CreateShape(new HSSFChildAnchor(x, y, x + width, y + height));
			hSSFSimpleShape.ShapeType = 1;
			hSSFSimpleShape.LineStyle = LineStyle.None;
			hSSFSimpleShape.SetFillColor(foreground.R, foreground.G, foreground.B);
			hSSFSimpleShape.SetLineStyleColor(foreground.R, foreground.G, foreground.B);
		}

		public void FillRoundRect(int x, int y, int width, int height, int arcWidth, int arcHeight)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "FillRoundRect not supported");
			}
		}

		public Rectangle GetClipRect()
		{
			return ClipBounds;
		}

		public void SetClip(int x, int y, int width, int height)
		{
			throw new NotImplementedException();
		}

		public void SetColor(Color color)
		{
			foreground = color;
		}

		public void SetFont(Font f)
		{
			font = f;
		}

		public void SetPaintMode()
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "SetPaintMode not supported");
			}
			throw new NotImplementedException();
		}

		public void SetXORMode(Color color)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "SetXORMode not supported");
			}
			throw new NotImplementedException();
		}

		public void Translate(int x, int y)
		{
			if (Logger.Check(5))
			{
				Logger.Log(5, "translate not supported");
			}
			throw new NotImplementedException();
		}

		private HSSFShapeGroup GetEscherGraphics()
		{
			return escherGroup;
		}
	}
}
