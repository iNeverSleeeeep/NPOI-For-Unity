using NPOI.HSSF.Util;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Spreadsheet;
using NPOI.OpenXmlFormats.Spreadsheet;
using System.Xml;

namespace NPOI.XSSF.UserModel
{
	/// Represents a shape with a predefined geometry in a SpreadsheetML Drawing.
	/// Possible shape types are defined in {@link NPOI.ss.usermodel.ShapeTypes}
	///
	/// @author Yegor Kozlov
	public class XSSFSimpleShape : XSSFShape
	{
		/// A default instance of CT_Shape used for creating new shapes.
		private static CT_Shape prototype;

		/// Xml bean that stores properties of this shape
		private CT_Shape ctShape;

		/// Gets the shape type, one of the constants defined in {@link NPOI.ss.usermodel.ShapeTypes}.
		///
		/// @return the shape type
		/// @see NPOI.ss.usermodel.ShapeTypes
		public int ShapeType
		{
			get
			{
				return (int)ctShape.spPr.prstGeom.prst;
			}
			set
			{
				ctShape.spPr.prstGeom.prst = (ST_ShapeType)value;
			}
		}

		public XSSFSimpleShape(XSSFDrawing drawing, CT_Shape ctShape)
		{
			base.drawing = drawing;
			this.ctShape = ctShape;
		}

		public XSSFSimpleShape(XSSFDrawing drawing, XmlNode spNode)
		{
			base.drawing = drawing;
			ctShape = CT_Shape.Parse(spNode, null);
		}

		/// Prototype with the default structure of a new auto-shape.
		internal static CT_Shape Prototype()
		{
			CT_Shape cT_Shape = new CT_Shape();
			CT_ShapeNonVisual cT_ShapeNonVisual = cT_Shape.AddNewNvSpPr();
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_NonVisualDrawingProps cT_NonVisualDrawingProps = cT_ShapeNonVisual.AddNewCNvPr();
			cT_NonVisualDrawingProps.id = 1u;
			cT_NonVisualDrawingProps.name = "Shape 1";
			cT_ShapeNonVisual.AddNewCNvSpPr();
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties cT_ShapeProperties = cT_Shape.AddNewSpPr();
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
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeStyle cT_ShapeStyle = cT_Shape.AddNewStyle();
			CT_SchemeColor cT_SchemeColor = cT_ShapeStyle.AddNewLnRef().AddNewSchemeClr();
			cT_SchemeColor.val = ST_SchemeColorVal.accent1;
			cT_SchemeColor.AddNewShade().val = 50000;
			cT_ShapeStyle.lnRef.idx = 2u;
			CT_StyleMatrixReference cT_StyleMatrixReference = cT_ShapeStyle.AddNewFillRef();
			cT_StyleMatrixReference.idx = 1u;
			cT_StyleMatrixReference.AddNewSchemeClr().val = ST_SchemeColorVal.accent1;
			CT_StyleMatrixReference cT_StyleMatrixReference2 = cT_ShapeStyle.AddNewEffectRef();
			cT_StyleMatrixReference2.idx = 0u;
			cT_StyleMatrixReference2.AddNewSchemeClr().val = ST_SchemeColorVal.accent1;
			CT_FontReference cT_FontReference = cT_ShapeStyle.AddNewFontRef();
			cT_FontReference.idx = ST_FontCollectionIndex.minor;
			cT_FontReference.AddNewSchemeClr().val = ST_SchemeColorVal.lt1;
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_TextBody cT_TextBody = cT_Shape.AddNewTxBody();
			CT_TextBodyProperties cT_TextBodyProperties = cT_TextBody.AddNewBodyPr();
			cT_TextBodyProperties.anchor = ST_TextAnchoringType.ctr;
			cT_TextBodyProperties.rtlCol = false;
			CT_TextParagraph cT_TextParagraph = cT_TextBody.AddNewP();
			cT_TextParagraph.AddNewPPr().algn = ST_TextAlignType.ctr;
			CT_TextCharacterProperties cT_TextCharacterProperties = cT_TextParagraph.AddNewEndParaRPr();
			cT_TextCharacterProperties.lang = "en-US";
			cT_TextCharacterProperties.sz = 1100;
			cT_TextBody.AddNewLstStyle();
			prototype = cT_Shape;
			return prototype;
		}

		public CT_Shape GetCTShape()
		{
			return ctShape;
		}

		protected internal override NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties GetShapeProperties()
		{
			return ctShape.spPr;
		}

		public void SetText(XSSFRichTextString str)
		{
			XSSFWorkbook xSSFWorkbook = (XSSFWorkbook)GetDrawing().GetParent().GetParent();
			str.SetStylesTableReference(xSSFWorkbook.GetStylesSource());
			CT_TextParagraph cT_TextParagraph = new CT_TextParagraph();
			if (str.NumFormattingRuns == 0)
			{
				CT_RegularTextRun cT_RegularTextRun = cT_TextParagraph.AddNewR();
				CT_TextCharacterProperties cT_TextCharacterProperties = cT_RegularTextRun.AddNewRPr();
				cT_TextCharacterProperties.lang = "en-US";
				cT_TextCharacterProperties.sz = 1100;
				cT_RegularTextRun.t = str.String;
			}
			else
			{
				for (int i = 0; i < str.GetCTRst().sizeOfRArray(); i++)
				{
					CT_RElt rArray = str.GetCTRst().GetRArray(i);
					CT_RPrElt cT_RPrElt = rArray.rPr;
					if (cT_RPrElt == null)
					{
						cT_RPrElt = rArray.AddNewRPr();
					}
					CT_RegularTextRun cT_RegularTextRun2 = cT_TextParagraph.AddNewR();
					CT_TextCharacterProperties cT_TextCharacterProperties2 = cT_RegularTextRun2.AddNewRPr();
					cT_TextCharacterProperties2.lang = "en-US";
					ApplyAttributes(cT_RPrElt, cT_TextCharacterProperties2);
					cT_RegularTextRun2.t = rArray.t;
				}
			}
			ctShape.txBody.SetPArray(new CT_TextParagraph[1]
			{
				cT_TextParagraph
			});
		}

		/// org.openxmlformats.schemas.spreadsheetml.x2006.main.CTRPrElt to
		/// org.openxmlformats.schemas.drawingml.x2006.main.CTFont adapter
		private static void ApplyAttributes(CT_RPrElt pr, CT_TextCharacterProperties rPr)
		{
			if (pr.sizeOfBArray() > 0)
			{
				rPr.b = pr.GetBArray(0).val;
			}
			if (pr.sizeOfUArray() > 0)
			{
				switch (pr.GetUArray(0).val)
				{
				case ST_UnderlineValues.single:
					rPr.u = ST_TextUnderlineType.sng;
					break;
				case ST_UnderlineValues.@double:
					rPr.u = ST_TextUnderlineType.dbl;
					break;
				case ST_UnderlineValues.none:
					rPr.u = ST_TextUnderlineType.none;
					break;
				}
			}
			if (pr.sizeOfIArray() > 0)
			{
				rPr.i = pr.GetIArray(0).val;
			}
			if (pr.sizeOfFamilyArray() > 0)
			{
				CT_TextFont cT_TextFont = rPr.AddNewLatin();
				cT_TextFont.typeface = pr.GetRFontArray(0).val;
			}
			if (pr.sizeOfSzArray() > 0)
			{
				int num2 = rPr.sz = (int)(pr.GetSzArray(0).val * 100.0);
			}
			if (pr.sizeOfColorArray() > 0)
			{
				CT_SolidColorFillProperties cT_SolidColorFillProperties = rPr.IsSetSolidFill() ? rPr.solidFill : rPr.AddNewSolidFill();
				NPOI.OpenXmlFormats.Spreadsheet.CT_Color colorArray = pr.GetColorArray(0);
				if (colorArray.IsSetRgb())
				{
					CT_SRgbColor cT_SRgbColor = cT_SolidColorFillProperties.IsSetSrgbClr() ? cT_SolidColorFillProperties.srgbClr : cT_SolidColorFillProperties.AddNewSrgbClr();
					cT_SRgbColor.val = colorArray.rgb;
				}
				else if (colorArray.IsSetIndexed())
				{
					HSSFColor hSSFColor = HSSFColor.GetIndexHash()[(int)colorArray.indexed] as HSSFColor;
					if (hSSFColor != null)
					{
						byte[] rGB = hSSFColor.RGB;
						CT_SRgbColor cT_SRgbColor2 = cT_SolidColorFillProperties.IsSetSrgbClr() ? cT_SolidColorFillProperties.srgbClr : cT_SolidColorFillProperties.AddNewSrgbClr();
						cT_SRgbColor2.val = rGB;
					}
				}
			}
		}
	}
}
