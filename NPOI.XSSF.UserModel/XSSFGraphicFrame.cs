using NPOI.OpenXmlFormats;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Spreadsheet;

namespace NPOI.XSSF.UserModel
{
	/// Represents DrawingML GraphicalObjectFrame.
	///
	/// @author Roman Kashitsyn
	public class XSSFGraphicFrame : XSSFShape
	{
		private static CT_GraphicalObjectFrame prototype;

		private CT_GraphicalObjectFrame graphicFrame;

		private new XSSFDrawing drawing;

		private new XSSFClientAnchor anchor;

		/// Returns the frame name.
		/// @return name of the frame
		public string Name
		{
			get
			{
				return GetNonVisualProperties().name;
			}
			set
			{
				GetNonVisualProperties().name = value;
			}
		}

		/// Returns the frame anchor.
		/// @return the anchor this frame is attached to
		public XSSFClientAnchor Anchor
		{
			get
			{
				return anchor;
			}
			set
			{
				anchor = value;
			}
		}

		/// Gets the frame id.
		public long Id
		{
			get
			{
				return graphicFrame.nvGraphicFramePr.cNvPr.id;
			}
			set
			{
				graphicFrame.nvGraphicFramePr.cNvPr.id = (uint)value;
			}
		}

		/// Construct a new XSSFGraphicFrame object.
		///
		/// @param Drawing the XSSFDrawing that owns this frame
		/// @param ctGraphicFrame the XML bean that stores this frame content
		public XSSFGraphicFrame(XSSFDrawing Drawing, CT_GraphicalObjectFrame ctGraphicFrame)
		{
			drawing = Drawing;
			graphicFrame = ctGraphicFrame;
		}

		internal CT_GraphicalObjectFrame GetCTGraphicalObjectFrame()
		{
			return graphicFrame;
		}

		/// Initialize default structure of a new graphic frame
		public static CT_GraphicalObjectFrame Prototype()
		{
			CT_GraphicalObjectFrame cT_GraphicalObjectFrame = new CT_GraphicalObjectFrame();
			CT_GraphicalObjectFrameNonVisual cT_GraphicalObjectFrameNonVisual = cT_GraphicalObjectFrame.AddNewNvGraphicFramePr();
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_NonVisualDrawingProps cT_NonVisualDrawingProps = cT_GraphicalObjectFrameNonVisual.AddNewCNvPr();
			cT_NonVisualDrawingProps.id = 0u;
			cT_NonVisualDrawingProps.name = "Diagramm 1";
			cT_GraphicalObjectFrameNonVisual.AddNewCNvGraphicFramePr();
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_Transform2D cT_Transform2D = cT_GraphicalObjectFrame.AddNewXfrm();
			CT_PositiveSize2D cT_PositiveSize2D = cT_Transform2D.AddNewExt();
			CT_Point2D cT_Point2D = cT_Transform2D.AddNewOff();
			cT_PositiveSize2D.cx = 0L;
			cT_PositiveSize2D.cy = 0L;
			cT_Point2D.x = 0L;
			cT_Point2D.y = 0L;
			cT_GraphicalObjectFrame.AddNewGraphic();
			prototype = cT_GraphicalObjectFrame;
			return prototype;
		}

		/// Sets the frame macro.
		public void SetMacro(string macro)
		{
			graphicFrame.macro = macro;
		}

		private NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_NonVisualDrawingProps GetNonVisualProperties()
		{
			CT_GraphicalObjectFrameNonVisual nvGraphicFramePr = graphicFrame.nvGraphicFramePr;
			return nvGraphicFramePr.cNvPr;
		}

		/// Assign a DrawingML chart to the graphic frame.
		internal void SetChart(XSSFChart chart, string relId)
		{
			CT_GraphicalObjectData data = graphicFrame.graphic.AddNewGraphicData();
			AppendChartElement(data, relId);
			chart.SetGraphicFrame(this);
		}

		private void AppendChartElement(CT_GraphicalObjectData data, string id)
		{
			string namespaceURI = ST_RelationshipId.NamespaceURI;
			string text = "http://schemas.openxmlformats.org/drawingml/2006/chart";
			string el = string.Format("<c:chart xmlns:c=\"{1}\" xmlns:r=\"{2}\" r:id=\"{0}\"/>", id, text, namespaceURI);
			data.AddChartElement(el);
			data.uri = text;
		}

		protected internal override NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_ShapeProperties GetShapeProperties()
		{
			return null;
		}
	}
}
