using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Spreadsheet;
using NPOI.SS.UserModel;
using System;

namespace NPOI.XSSF.UserModel
{
	/// Represents a shape in a SpreadsheetML Drawing.
	///
	/// @author Yegor Kozlov
	public abstract class XSSFShape : IShape
	{
		public static int EMU_PER_PIXEL = 9525;

		public static int EMU_PER_POINT = 12700;

		public static int POINT_DPI = 72;

		public static int PIXEL_DPI = 96;

		/// Parent Drawing
		protected XSSFDrawing drawing;

		/// The parent shape, always not-null for shapes in groups
		public XSSFShapeGroup parent;

		/// anchor that is used by this shape
		internal XSSFAnchor anchor;

		/// Gets the parent shape.
		public IShape Parent
		{
			get
			{
				return parent;
			}
		}

		/// Whether this shape is not Filled with a color
		///
		/// @return true if this shape is not Filled with a color.
		public bool IsNoFill
		{
			get
			{
				return GetShapeProperties().noFill != null;
			}
			set
			{
				NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties shapeProperties = GetShapeProperties();
				if (shapeProperties.IsSetPattFill())
				{
					shapeProperties.unsetPattFill();
				}
				if (shapeProperties.IsSetSolidFill())
				{
					shapeProperties.unsetSolidFill();
				}
				shapeProperties.noFill = new CT_NoFillProperties();
			}
		}

		public int CountOfAllChildren
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int FillColor
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

		public virtual LineStyle LineStyle
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties shapeProperties = GetShapeProperties();
				CT_LineProperties cT_LineProperties = shapeProperties.IsSetLn() ? shapeProperties.ln : shapeProperties.AddNewLn();
				CT_PresetLineDashProperties cT_PresetLineDashProperties = new CT_PresetLineDashProperties();
				cT_PresetLineDashProperties.val = (ST_PresetLineDashVal)(value + 1);
				cT_LineProperties.prstDash = cT_PresetLineDashProperties;
			}
		}

		public virtual int LineStyleColor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public virtual double LineWidth
		{
			get
			{
				NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties shapeProperties = GetShapeProperties();
				if (shapeProperties.IsSetLn())
				{
					return (double)shapeProperties.ln.w * 1.0 / (double)EMU_PER_POINT;
				}
				return 0.0;
			}
			set
			{
				NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties shapeProperties = GetShapeProperties();
				CT_LineProperties cT_LineProperties = shapeProperties.IsSetLn() ? shapeProperties.ln : shapeProperties.AddNewLn();
				cT_LineProperties.w = (int)(value * (double)EMU_PER_POINT);
			}
		}

		/// Return the Drawing that owns this shape
		///
		/// @return the parent Drawing that owns this shape
		public XSSFDrawing GetDrawing()
		{
			return drawing;
		}

		/// @return  the anchor that is used by this shape.
		public XSSFAnchor GetAnchor()
		{
			return anchor;
		}

		/// Returns xml bean with shape properties.
		///
		/// @return xml bean with shape properties.
		protected internal abstract NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties GetShapeProperties();

		/// Sets the color used to fill this shape using the solid fill pattern.
		public void SetFillColor(int red, int green, int blue)
		{
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties shapeProperties = GetShapeProperties();
			CT_SolidColorFillProperties cT_SolidColorFillProperties = shapeProperties.IsSetSolidFill() ? shapeProperties.solidFill : shapeProperties.AddNewSolidFill();
			CT_SRgbColor cT_SRgbColor = new CT_SRgbColor();
			cT_SRgbColor.val = new byte[3]
			{
				(byte)red,
				(byte)green,
				(byte)blue
			};
			cT_SolidColorFillProperties.srgbClr = cT_SRgbColor;
		}

		/// The color applied to the lines of this shape.
		public void SetLineStyleColor(int red, int green, int blue)
		{
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties shapeProperties = GetShapeProperties();
			CT_LineProperties cT_LineProperties = shapeProperties.IsSetLn() ? shapeProperties.ln : shapeProperties.AddNewLn();
			CT_SolidColorFillProperties cT_SolidColorFillProperties = cT_LineProperties.IsSetSolidFill() ? cT_LineProperties.solidFill : cT_LineProperties.AddNewSolidFill();
			CT_SRgbColor cT_SRgbColor = new CT_SRgbColor();
			cT_SRgbColor.val = new byte[3]
			{
				(byte)red,
				(byte)green,
				(byte)blue
			};
			cT_SolidColorFillProperties.srgbClr = cT_SRgbColor;
		}

		public void SetLineStyleColor(int lineStyleColor)
		{
			throw new NotImplementedException();
		}
	}
}
