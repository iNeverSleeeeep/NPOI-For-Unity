using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Spreadsheet;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.UserModel
{
	/// Represents a picture shape in a SpreadsheetML Drawing.
	///
	/// @author Yegor Kozlov
	public class XSSFPicture : XSSFShape, IPicture
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(XSSFPicture));

		/// Column width measured as the number of characters of the maximum digit width of the
		/// numbers 0, 1, 2, ..., 9 as rendered in the normal style's font. There are 4 pixels of margin
		/// pAdding (two on each side), plus 1 pixel pAdding for the gridlines.
		///
		/// This value is the same for default font in Office 2007 (Calibry) and Office 2003 and earlier (Arial)
		private static float DEFAULT_COLUMN_WIDTH = 9.140625f;

		/// A default instance of CTShape used for creating new shapes.
		private static CT_Picture prototype = null;

		/// This object specifies a picture object and all its properties
		private CT_Picture ctPicture;

		public new int CountOfAllChildren
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public new int FillColor
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public new LineStyle LineStyle
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				base.LineStyle = value;
			}
		}

		public new int LineStyleColor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public new int LineWidth
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				base.LineWidth = (double)value;
			}
		}

		public IPictureData PictureData
		{
			get
			{
				string embed = ctPicture.blipFill.blip.embed;
				return (XSSFPictureData)GetDrawing().GetRelationById(embed);
			}
		}

		/// Construct a new XSSFPicture object. This constructor is called from
		///  {@link XSSFDrawing#CreatePicture(XSSFClientAnchor, int)}
		///
		/// @param Drawing the XSSFDrawing that owns this picture
		public XSSFPicture(XSSFDrawing drawing, CT_Picture ctPicture)
		{
			base.drawing = drawing;
			this.ctPicture = ctPicture;
		}

		/// Returns a prototype that is used to construct new shapes
		///
		/// @return a prototype that is used to construct new shapes
		public XSSFPicture(XSSFDrawing drawing, XmlNode ctPicture)
		{
			base.drawing = drawing;
			this.ctPicture = CT_Picture.Parse(ctPicture, POIXMLDocumentPart.NamespaceManager);
		}

		internal static CT_Picture Prototype()
		{
			CT_Picture cT_Picture = new CT_Picture();
			CT_PictureNonVisual cT_PictureNonVisual = cT_Picture.AddNewNvPicPr();
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_NonVisualDrawingProps cT_NonVisualDrawingProps = cT_PictureNonVisual.AddNewCNvPr();
			cT_NonVisualDrawingProps.id = 1u;
			cT_NonVisualDrawingProps.name = "Picture 1";
			cT_NonVisualDrawingProps.descr = "Picture";
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_NonVisualPictureProperties cT_NonVisualPictureProperties = cT_PictureNonVisual.AddNewCNvPicPr();
			cT_NonVisualPictureProperties.AddNewPicLocks().noChangeAspect = true;
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_BlipFillProperties cT_BlipFillProperties = cT_Picture.AddNewBlipFill();
			cT_BlipFillProperties.AddNewBlip().embed = "";
			cT_BlipFillProperties.AddNewStretch().AddNewFillRect();
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties cT_ShapeProperties = cT_Picture.AddNewSpPr();
			NPOI.OpenXmlFormats.Dml.CT_Transform2D cT_Transform2D = cT_ShapeProperties.AddNewXfrm();
			CT_PositiveSize2D cT_PositiveSize2D = cT_Transform2D.AddNewExt();
			cT_PositiveSize2D.cx = 0L;
			cT_PositiveSize2D.cy = 0L;
			CT_Point2D cT_Point2D = cT_Transform2D.AddNewOff();
			cT_Point2D.x = 0L;
			cT_Point2D.y = 0L;
			CT_PresetGeometry2D cT_PresetGeometry2D = cT_ShapeProperties.AddNewPrstGeom();
			cT_PresetGeometry2D.prst = ST_ShapeType.rect;
			cT_PresetGeometry2D.AddNewAvLst();
			prototype = cT_Picture;
			return prototype;
		}

		/// Link this shape with the picture data
		///
		/// @param rel relationship referring the picture data
		internal void SetPictureReference(PackageRelationship rel)
		{
			ctPicture.blipFill.blip.embed = rel.Id;
		}

		/// Return the underlying CT_Picture bean that holds all properties for this picture
		///
		/// @return the underlying CT_Picture bean
		internal CT_Picture GetCTPicture()
		{
			return ctPicture;
		}

		/// Reset the image to the original size.
		///
		/// <p>
		/// Please note, that this method works correctly only for workbooks
		/// with the default font size (Calibri 11pt for .xlsx).
		/// If the default font is Changed the resized image can be streched vertically or horizontally.
		/// </p>
		public void Resize()
		{
			Resize(1.0);
		}

		/// Reset the image to the original size.
		/// <p>
		/// Please note, that this method works correctly only for workbooks
		/// with the default font size (Calibri 11pt for .xlsx).
		/// If the default font is Changed the resized image can be streched vertically or horizontally.
		/// </p>
		///
		/// @param scale the amount by which image dimensions are multiplied relative to the original size.
		/// <code>resize(1.0)</code> Sets the original size, <code>resize(0.5)</code> resize to 50% of the original,
		/// <code>resize(2.0)</code> resizes to 200% of the original.
		public void Resize(double scale)
		{
			IClientAnchor clientAnchor = (XSSFClientAnchor)GetAnchor();
			IClientAnchor preferredSize = GetPreferredSize(scale);
			int row = clientAnchor.Row1 + (preferredSize.Row2 - preferredSize.Row1);
			int num2 = clientAnchor.Col2 = clientAnchor.Col1 + (preferredSize.Col2 - preferredSize.Col1);
			clientAnchor.Dx1 = 0;
			clientAnchor.Dx2 = preferredSize.Dx2;
			clientAnchor.Row2 = row;
			clientAnchor.Dy1 = 0;
			clientAnchor.Dy2 = preferredSize.Dy2;
		}

		/// Calculate the preferred size for this picture.
		///
		/// @return XSSFClientAnchor with the preferred size for this image
		public IClientAnchor GetPreferredSize()
		{
			return GetPreferredSize(1.0);
		}

		/// Calculate the preferred size for this picture.
		///
		/// @param scale the amount by which image dimensions are multiplied relative to the original size.
		/// @return XSSFClientAnchor with the preferred size for this image
		public IClientAnchor GetPreferredSize(double scale)
		{
			XSSFClientAnchor xSSFClientAnchor = (XSSFClientAnchor)GetAnchor();
			XSSFPictureData xSSFPictureData = (XSSFPictureData)PictureData;
			Size imageDimension = GetImageDimension(xSSFPictureData.GetPackagePart(), xSSFPictureData.GetPictureType());
			double num = (double)imageDimension.Width * scale;
			double num2 = (double)imageDimension.Height * scale;
			float num3 = 0f;
			int num4 = xSSFClientAnchor.Col1;
			int dx = 0;
			while (true)
			{
				num3 += GetColumnWidthInPixels(num4);
				if ((double)num3 > num)
				{
					break;
				}
				num4++;
			}
			if ((double)num3 > num)
			{
				double num5 = (double)GetColumnWidthInPixels(num4);
				double num6 = (double)num3 - num;
				dx = (int)((double)XSSFShape.EMU_PER_PIXEL * (num5 - num6));
			}
			xSSFClientAnchor.Col2 = num4;
			xSSFClientAnchor.Dx2 = dx;
			double num7 = 0.0;
			int num8 = xSSFClientAnchor.Row1;
			int dy = 0;
			while (true)
			{
				num7 += (double)GetRowHeightInPixels(num8);
				if (num7 > num2)
				{
					break;
				}
				num8++;
			}
			if (num7 > num2)
			{
				double num9 = (double)GetRowHeightInPixels(num8);
				double num10 = num7 - num2;
				dy = (int)((double)XSSFShape.EMU_PER_PIXEL * (num9 - num10));
			}
			xSSFClientAnchor.Row2 = num8;
			xSSFClientAnchor.Dy2 = dy;
			CT_PositiveSize2D ext = ctPicture.spPr.xfrm.ext;
			ext.cx = (long)(num * (double)XSSFShape.EMU_PER_PIXEL);
			ext.cy = (long)(num2 * (double)XSSFShape.EMU_PER_PIXEL);
			return xSSFClientAnchor;
		}

		private float GetColumnWidthInPixels(int columnIndex)
		{
			XSSFSheet xSSFSheet = (XSSFSheet)GetDrawing().GetParent();
			CT_Col column = xSSFSheet.GetColumnHelper().GetColumn(columnIndex, false);
			double num = (column == null || !column.IsSetWidth()) ? ((double)DEFAULT_COLUMN_WIDTH) : column.width;
			return (float)num * XSSFWorkbook.DEFAULT_CHARACTER_WIDTH;
		}

		private float GetRowHeightInPixels(int rowIndex)
		{
			XSSFSheet xSSFSheet = (XSSFSheet)GetDrawing().GetParent();
			IRow row = xSSFSheet.GetRow(rowIndex);
			float num = (row != null) ? row.HeightInPoints : xSSFSheet.DefaultRowHeightInPoints;
			return num * (float)XSSFShape.PIXEL_DPI / (float)XSSFShape.POINT_DPI;
		}

		/// Return the dimension of this image
		///
		/// @param part the namespace part holding raw picture data
		/// @param type type of the picture: {@link Workbook#PICTURE_TYPE_JPEG},
		/// {@link Workbook#PICTURE_TYPE_PNG} or {@link Workbook#PICTURE_TYPE_DIB}
		///
		/// @return image dimension in pixels
		protected static Size GetImageDimension(PackagePart part, int type)
		{
			try
			{
				return Image.FromStream(part.GetInputStream()).Size;
			}
			catch (IOException exception)
			{
				logger.Log(5, exception);
				return default(Size);
			}
		}

		protected internal override NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties GetShapeProperties()
		{
			return ctPicture.spPr;
		}

		public new void SetLineStyleColor(int lineStyleColor)
		{
			throw new NotImplementedException();
		}
	}
}
