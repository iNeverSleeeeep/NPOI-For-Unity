using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Spreadsheet;

namespace NPOI.XSSF.UserModel
{
	/// A connection shape Drawing element. A connection shape is a line, etc.
	/// that connects two other shapes in this Drawing.
	///
	/// @author Yegor Kozlov
	public class XSSFConnector : XSSFShape
	{
		private static CT_Connector prototype;

		private CT_Connector ctShape;

		/// Gets the shape type, one of the constants defined in {@link NPOI.ss.usermodel.ShapeTypes}.
		///
		/// @return the shape type
		/// @see NPOI.ss.usermodel.ShapeTypes
		public ST_ShapeType ShapeType
		{
			get
			{
				return ctShape.spPr.prstGeom.prst;
			}
			set
			{
				ctShape.spPr.prstGeom.prst = value;
			}
		}

		/// Construct a new XSSFConnector object.
		///
		/// @param Drawing the XSSFDrawing that owns this shape
		/// @param ctShape the shape bean that holds all the shape properties
		public XSSFConnector(XSSFDrawing drawing, CT_Connector ctShape)
		{
			base.drawing = drawing;
			this.ctShape = ctShape;
		}

		/// Initialize default structure of a new auto-shape
		public static CT_Connector Prototype()
		{
			CT_Connector cT_Connector = new CT_Connector();
			CT_ConnectorNonVisual cT_ConnectorNonVisual = cT_Connector.AddNewNvCxnSpPr();
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_NonVisualDrawingProps cT_NonVisualDrawingProps = cT_ConnectorNonVisual.AddNewCNvPr();
			cT_NonVisualDrawingProps.id = 1u;
			cT_NonVisualDrawingProps.name = "Shape 1";
			cT_ConnectorNonVisual.AddNewCNvCxnSpPr();
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties cT_ShapeProperties = cT_Connector.AddNewSpPr();
			NPOI.OpenXmlFormats.Dml.CT_Transform2D cT_Transform2D = cT_ShapeProperties.AddNewXfrm();
			CT_PositiveSize2D cT_PositiveSize2D = cT_Transform2D.AddNewExt();
			cT_PositiveSize2D.cx = 0L;
			cT_PositiveSize2D.cy = 0L;
			CT_Point2D cT_Point2D = cT_Transform2D.AddNewOff();
			cT_Point2D.x = 0L;
			cT_Point2D.y = 0L;
			CT_PresetGeometry2D cT_PresetGeometry2D = cT_ShapeProperties.AddNewPrstGeom();
			cT_PresetGeometry2D.prst = ST_ShapeType.line;
			cT_PresetGeometry2D.AddNewAvLst();
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeStyle cT_ShapeStyle = cT_Connector.AddNewStyle();
			CT_SchemeColor cT_SchemeColor = cT_ShapeStyle.AddNewLnRef().AddNewSchemeClr();
			cT_SchemeColor.val = ST_SchemeColorVal.accent1;
			cT_ShapeStyle.lnRef.idx = 1u;
			CT_StyleMatrixReference cT_StyleMatrixReference = cT_ShapeStyle.AddNewFillRef();
			cT_StyleMatrixReference.idx = 0u;
			cT_StyleMatrixReference.AddNewSchemeClr().val = ST_SchemeColorVal.accent1;
			CT_StyleMatrixReference cT_StyleMatrixReference2 = cT_ShapeStyle.AddNewEffectRef();
			cT_StyleMatrixReference2.idx = 0u;
			cT_StyleMatrixReference2.AddNewSchemeClr().val = ST_SchemeColorVal.accent1;
			CT_FontReference cT_FontReference = cT_ShapeStyle.AddNewFontRef();
			cT_FontReference.idx = ST_FontCollectionIndex.minor;
			cT_FontReference.AddNewSchemeClr().val = ST_SchemeColorVal.tx1;
			prototype = cT_Connector;
			return prototype;
		}

		public CT_Connector GetCTConnector()
		{
			return ctShape;
		}

		protected internal override NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties GetShapeProperties()
		{
			return ctShape.spPr;
		}
	}
}
