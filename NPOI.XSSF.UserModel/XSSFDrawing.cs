using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Dml.Spreadsheet;
using NPOI.OpenXmlFormats.Vml;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.UserModel
{
	/// Represents a SpreadsheetML Drawing
	///
	/// @author Yegor Kozlov
	public class XSSFDrawing : POIXMLDocumentPart, IDrawing
	{
		public const string NAMESPACE_A = "http://schemas.openxmlformats.org/drawingml/2006/main";

		public const string NAMESPACE_C = "http://schemas.openxmlformats.org/drawingml/2006/chart";

		/// Root element of the SpreadsheetML Drawing part
		private CT_Drawing drawing = NewDrawing();

		private long numOfGraphicFrames;

		/// Create a new SpreadsheetML Drawing
		///
		/// @see NPOI.xssf.usermodel.XSSFSheet#CreateDrawingPatriarch()
		public XSSFDrawing()
		{
			drawing = NewDrawing();
		}

		/// Construct a SpreadsheetML Drawing from a namespace part
		///
		/// @param part the namespace part holding the Drawing data,
		/// the content type must be <code>application/vnd.openxmlformats-officedocument.Drawing+xml</code>
		/// @param rel  the namespace relationship holding this Drawing,
		/// the relationship type must be http://schemas.openxmlformats.org/officeDocument/2006/relationships/drawing
		internal XSSFDrawing(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			XmlDocument xmldoc = POIXMLDocumentPart.ConvertStreamToXml(part.GetInputStream());
			drawing = CT_Drawing.Parse(xmldoc, POIXMLDocumentPart.NamespaceManager);
		}

		/// Construct a new CT_Drawing bean. By default, it's just an empty placeholder for Drawing objects
		///
		/// @return a new CT_Drawing bean
		private static CT_Drawing NewDrawing()
		{
			return new CT_Drawing();
		}

		/// Return the underlying CT_Drawing bean, the root element of the SpreadsheetML Drawing part.
		///
		/// @return the underlying CT_Drawing bean
		public CT_Drawing GetCTDrawing()
		{
			return drawing;
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			Stream outputStream = packagePart.GetOutputStream();
			drawing.Save(outputStream);
			outputStream.Close();
		}

		public IClientAnchor CreateAnchor(int dx1, int dy1, int dx2, int dy2, int col1, int row1, int col2, int row2)
		{
			return new XSSFClientAnchor(dx1, dy1, dx2, dy2, col1, row1, col2, row2);
		}

		/// Constructs a textbox under the Drawing.
		///
		/// @param anchor    the client anchor describes how this group is attached
		///                  to the sheet.
		/// @return      the newly Created textbox.
		public ITextbox CreateTextbox(IClientAnchor anchor)
		{
			long num = newShapeId();
			CT_TwoCellAnchor cT_TwoCellAnchor = CreateTwoCellAnchor(anchor);
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_Shape cT_Shape = cT_TwoCellAnchor.AddNewSp();
			cT_Shape.Set(XSSFSimpleShape.Prototype());
			cT_Shape.nvSpPr.cNvPr.id = (uint)num;
			XSSFTextBox xSSFTextBox = new XSSFTextBox(this, cT_Shape);
			xSSFTextBox.anchor = (XSSFClientAnchor)anchor;
			return xSSFTextBox;
		}

		/// Creates a picture.
		///
		/// @param anchor    the client anchor describes how this picture is attached to the sheet.
		/// @param pictureIndex the index of the picture in the workbook collection of pictures,
		///   {@link NPOI.xssf.usermodel.XSSFWorkbook#getAllPictures()} .
		///
		/// @return  the newly Created picture shape.
		public IPicture CreatePicture(XSSFClientAnchor anchor, int pictureIndex)
		{
			PackageRelationship pictureReference = AddPictureReference(pictureIndex);
			long num = newShapeId();
			CT_TwoCellAnchor cT_TwoCellAnchor = CreateTwoCellAnchor(anchor);
			CT_Picture cT_Picture = cT_TwoCellAnchor.AddNewPic();
			cT_Picture.Set(XSSFPicture.Prototype());
			cT_Picture.nvPicPr.cNvPr.id = (uint)num;
			cT_Picture.nvPicPr.cNvPr.name = "Picture " + num;
			XSSFPicture xSSFPicture = new XSSFPicture(this, cT_Picture);
			xSSFPicture.anchor = anchor;
			xSSFPicture.SetPictureReference(pictureReference);
			return xSSFPicture;
		}

		public IPicture CreatePicture(IClientAnchor anchor, int pictureIndex)
		{
			return CreatePicture((XSSFClientAnchor)anchor, pictureIndex);
		}

		/// <summary>
		/// Creates a chart.
		/// </summary>
		/// <param name="anchor">the client anchor describes how this chart is attached to</param>
		/// <returns>the newly created chart</returns>
		public IChart CreateChart(IClientAnchor anchor)
		{
			int idx = GetPackagePart().Package.GetPartsByContentType(XSSFRelation.CHART.ContentType).Count + 1;
			XSSFChart xSSFChart = (XSSFChart)CreateRelationship(XSSFRelation.CHART, XSSFFactory.GetInstance(), idx);
			string id = xSSFChart.GetPackageRelationship().Id;
			XSSFGraphicFrame xSSFGraphicFrame = CreateGraphicFrame((XSSFClientAnchor)anchor);
			xSSFGraphicFrame.SetChart(xSSFChart, id);
			return xSSFChart;
		}

		/// Add the indexed picture to this Drawing relations
		///
		/// @param pictureIndex the index of the picture in the workbook collection of pictures,
		///   {@link NPOI.xssf.usermodel.XSSFWorkbook#getAllPictures()} .
		internal PackageRelationship AddPictureReference(int pictureIndex)
		{
			XSSFWorkbook xSSFWorkbook = (XSSFWorkbook)GetParent().GetParent();
			XSSFPictureData xSSFPictureData = (XSSFPictureData)xSSFWorkbook.GetAllPictures()[pictureIndex];
			PackagePartName partName = xSSFPictureData.GetPackagePart().PartName;
			PackageRelationship packageRelationship = GetPackagePart().AddRelationship(partName, TargetMode.Internal, XSSFRelation.IMAGES.Relation);
			AddRelation(packageRelationship.Id, new XSSFPictureData(xSSFPictureData.GetPackagePart(), packageRelationship));
			return packageRelationship;
		}

		/// Creates a simple shape.  This includes such shapes as lines, rectangles,
		/// and ovals.
		///
		/// @param anchor    the client anchor describes how this group is attached
		///                  to the sheet.
		/// @return  the newly Created shape.
		public XSSFSimpleShape CreateSimpleShape(XSSFClientAnchor anchor)
		{
			long num = newShapeId();
			CT_TwoCellAnchor cT_TwoCellAnchor = CreateTwoCellAnchor(anchor);
			NPOI.OpenXmlFormats.Dml.Spreadsheet.CT_Shape cT_Shape = cT_TwoCellAnchor.AddNewSp();
			cT_Shape.Set(XSSFSimpleShape.Prototype());
			cT_Shape.nvSpPr.cNvPr.id = (uint)num;
			XSSFSimpleShape xSSFSimpleShape = new XSSFSimpleShape(this, cT_Shape);
			xSSFSimpleShape.anchor = anchor;
			return xSSFSimpleShape;
		}

		/// Creates a simple shape.  This includes such shapes as lines, rectangles,
		/// and ovals.
		///
		/// @param anchor    the client anchor describes how this group is attached
		///                  to the sheet.
		/// @return  the newly Created shape.
		public XSSFConnector CreateConnector(XSSFClientAnchor anchor)
		{
			CT_TwoCellAnchor cT_TwoCellAnchor = CreateTwoCellAnchor(anchor);
			CT_Connector cT_Connector = cT_TwoCellAnchor.AddNewCxnSp();
			cT_Connector.Set(XSSFConnector.Prototype());
			XSSFConnector xSSFConnector = new XSSFConnector(this, cT_Connector);
			xSSFConnector.anchor = anchor;
			return xSSFConnector;
		}

		/// Creates a simple shape.  This includes such shapes as lines, rectangles,
		/// and ovals.
		///
		/// @param anchor    the client anchor describes how this group is attached
		///                  to the sheet.
		/// @return  the newly Created shape.
		public XSSFShapeGroup CreateGroup(XSSFClientAnchor anchor)
		{
			CT_TwoCellAnchor cT_TwoCellAnchor = CreateTwoCellAnchor(anchor);
			CT_GroupShape cT_GroupShape = cT_TwoCellAnchor.AddNewGrpSp();
			cT_GroupShape.Set(XSSFShapeGroup.Prototype());
			XSSFShapeGroup xSSFShapeGroup = new XSSFShapeGroup(this, cT_GroupShape);
			xSSFShapeGroup.anchor = anchor;
			return xSSFShapeGroup;
		}

		/// Creates a comment.
		/// @param anchor the client anchor describes how this comment is attached
		///               to the sheet.
		/// @return the newly Created comment.
		public IComment CreateCellComment(IClientAnchor anchor)
		{
			XSSFClientAnchor xSSFClientAnchor = (XSSFClientAnchor)anchor;
			XSSFSheet xSSFSheet = (XSSFSheet)GetParent();
			CommentsTable commentsTable = xSSFSheet.GetCommentsTable(true);
			XSSFVMLDrawing vMLDrawing = xSSFSheet.GetVMLDrawing(true);
			NPOI.OpenXmlFormats.Vml.CT_Shape cT_Shape = vMLDrawing.newCommentShape();
			if (xSSFClientAnchor.IsSet())
			{
				string value = xSSFClientAnchor.Col1 + ", 0, " + xSSFClientAnchor.Row1 + ", 0, " + xSSFClientAnchor.Col2 + ", 0, " + xSSFClientAnchor.Row2 + ", 0";
				cT_Shape.GetClientDataArray(0).SetAnchorArray(0, value);
			}
			XSSFComment xSSFComment = new XSSFComment(commentsTable, commentsTable.CreateComment(), cT_Shape);
			xSSFComment.Column = xSSFClientAnchor.Col1;
			xSSFComment.Row = xSSFClientAnchor.Row1;
			return xSSFComment;
		}

		/// Creates a new graphic frame.
		///
		/// @param anchor    the client anchor describes how this frame is attached
		///                  to the sheet
		/// @return  the newly Created graphic frame
		private XSSFGraphicFrame CreateGraphicFrame(XSSFClientAnchor anchor)
		{
			CT_TwoCellAnchor cT_TwoCellAnchor = CreateTwoCellAnchor(anchor);
			CT_GraphicalObjectFrame cT_GraphicalObjectFrame = cT_TwoCellAnchor.AddNewGraphicFrame();
			cT_GraphicalObjectFrame.Set(XSSFGraphicFrame.Prototype());
			long num = numOfGraphicFrames++;
			XSSFGraphicFrame xSSFGraphicFrame = new XSSFGraphicFrame(this, cT_GraphicalObjectFrame);
			xSSFGraphicFrame.Anchor = anchor;
			xSSFGraphicFrame.Id = num;
			xSSFGraphicFrame.Name = "Diagramm" + num;
			return xSSFGraphicFrame;
		}

		/// Returns all charts in this Drawing.
		public List<XSSFChart> GetCharts()
		{
			List<XSSFChart> list = new List<XSSFChart>();
			foreach (POIXMLDocumentPart relation in GetRelations())
			{
				if (relation is XSSFChart)
				{
					list.Add((XSSFChart)relation);
				}
			}
			return list;
		}

		/// Create and Initialize a CT_TwoCellAnchor that anchors a shape against top-left and bottom-right cells.
		///
		/// @return a new CT_TwoCellAnchor
		private CT_TwoCellAnchor CreateTwoCellAnchor(IClientAnchor anchor)
		{
			CT_TwoCellAnchor cT_TwoCellAnchor = drawing.AddNewTwoCellAnchor();
			XSSFClientAnchor xSSFClientAnchor = (XSSFClientAnchor)anchor;
			cT_TwoCellAnchor.from = xSSFClientAnchor.From;
			cT_TwoCellAnchor.to = xSSFClientAnchor.To;
			cT_TwoCellAnchor.AddNewClientData();
			xSSFClientAnchor.To = cT_TwoCellAnchor.to;
			xSSFClientAnchor.From = cT_TwoCellAnchor.from;
			NPOI.OpenXmlFormats.Dml.Spreadsheet.ST_EditAs editAs;
			switch (anchor.AnchorType)
			{
			case 3:
				editAs = NPOI.OpenXmlFormats.Dml.Spreadsheet.ST_EditAs.absolute;
				break;
			case 0:
				editAs = NPOI.OpenXmlFormats.Dml.Spreadsheet.ST_EditAs.twoCell;
				break;
			case 2:
				editAs = NPOI.OpenXmlFormats.Dml.Spreadsheet.ST_EditAs.oneCell;
				break;
			default:
				editAs = NPOI.OpenXmlFormats.Dml.Spreadsheet.ST_EditAs.oneCell;
				break;
			}
			cT_TwoCellAnchor.editAs = editAs;
			cT_TwoCellAnchor.editAsSpecified = true;
			return cT_TwoCellAnchor;
		}

		private long newShapeId()
		{
			return drawing.SizeOfTwoCellAnchorArray() + 1;
		}

		public bool ContainsChart()
		{
			throw new NotImplementedException();
		}

		/// @return list of shapes in this drawing
		public List<XSSFShape> GetShapes()
		{
			List<XSSFShape> list = new List<XSSFShape>();
			foreach (IEG_Anchor cellAnchor in drawing.CellAnchors)
			{
				XSSFShape xSSFShape = null;
				if (cellAnchor.picture != null)
				{
					xSSFShape = new XSSFPicture(this, cellAnchor.picture);
				}
				else if (cellAnchor.connector != null)
				{
					xSSFShape = new XSSFConnector(this, cellAnchor.connector);
				}
				else if (cellAnchor.groupShape != null)
				{
					xSSFShape = new XSSFShapeGroup(this, cellAnchor.groupShape);
				}
				else if (cellAnchor.graphicFrame != null)
				{
					xSSFShape = new XSSFGraphicFrame(this, cellAnchor.graphicFrame);
				}
				else if (cellAnchor.sp != null)
				{
					xSSFShape = new XSSFSimpleShape(this, cellAnchor.sp);
				}
				if (xSSFShape != null)
				{
					xSSFShape.anchor = GetAnchorFromIEGAnchor(cellAnchor);
					list.Add(xSSFShape);
				}
			}
			return list;
		}

		private XSSFAnchor GetAnchorFromIEGAnchor(IEG_Anchor ctAnchor)
		{
			CT_Marker cell = null;
			CT_Marker cell2 = null;
			if (ctAnchor is CT_TwoCellAnchor)
			{
				cell = ((CT_TwoCellAnchor)ctAnchor).from;
				cell2 = ((CT_TwoCellAnchor)ctAnchor).to;
			}
			else if (ctAnchor is CT_OneCellAnchor)
			{
				cell = ((CT_OneCellAnchor)ctAnchor).from;
			}
			return new XSSFClientAnchor(cell, cell2);
		}

		private XSSFAnchor GetAnchorFromParent(XmlNode obj)
		{
			XSSFAnchor xSSFAnchor = null;
			XmlNode parentNode = obj.ParentNode;
			XmlNode xmlNode = parentNode.SelectSingleNode("xdr:from", POIXMLDocumentPart.NamespaceManager);
			if (xmlNode == null)
			{
				throw new InvalidDataException("xdr:from node is missing");
			}
			CT_Marker cell = CT_Marker.Parse(xmlNode, POIXMLDocumentPart.NamespaceManager);
			XmlNode xmlNode2 = parentNode.SelectSingleNode("xdr:to", POIXMLDocumentPart.NamespaceManager);
			CT_Marker cell2 = null;
			if (xmlNode2 != null)
			{
				cell2 = CT_Marker.Parse(xmlNode2, POIXMLDocumentPart.NamespaceManager);
			}
			return new XSSFClientAnchor(cell, cell2);
		}
	}
}
