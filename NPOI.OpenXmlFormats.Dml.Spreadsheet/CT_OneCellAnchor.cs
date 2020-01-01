using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_OneCellAnchor : IEG_Anchor
	{
		private CT_Marker fromField = new CT_Marker();

		private CT_PositiveSize2D extField;

		private CT_AnchorClientData clientDataField = new CT_AnchorClientData();

		private CT_Shape shapeField;

		private CT_GroupShape groupShapeField;

		private CT_GraphicalObjectFrame graphicalObjectField;

		private CT_Connector connectorField;

		private CT_Picture pictureField;

		[XmlElement]
		public CT_Marker from
		{
			get
			{
				return fromField;
			}
			set
			{
				fromField = value;
			}
		}

		[XmlElement]
		public CT_PositiveSize2D ext
		{
			get
			{
				return extField;
			}
			set
			{
				extField = value;
			}
		}

		[XmlElement]
		public CT_AnchorClientData clientData
		{
			get
			{
				return clientDataField;
			}
			set
			{
				clientDataField = value;
			}
		}

		public CT_Shape sp
		{
			get
			{
				return shapeField;
			}
			set
			{
				shapeField = value;
			}
		}

		public CT_GroupShape groupShape
		{
			get
			{
				return groupShapeField;
			}
			set
			{
				groupShapeField = value;
			}
		}

		public CT_GraphicalObjectFrame graphicFrame
		{
			get
			{
				return graphicalObjectField;
			}
			set
			{
				graphicalObjectField = value;
			}
		}

		public CT_Connector connector
		{
			get
			{
				return connectorField;
			}
			set
			{
				connectorField = value;
			}
		}

		public CT_Picture picture
		{
			get
			{
				return pictureField;
			}
			set
			{
				pictureField = value;
			}
		}

		public CT_AnchorClientData AddNewClientData()
		{
			clientDataField = new CT_AnchorClientData();
			return clientDataField;
		}

		public void Write(StreamWriter sw)
		{
			sw.Write("<xdr:oneCellAnchor>");
			from.Write(sw, "xdr:from");
			ext.Write(sw, "xdr:ext");
			if (sp != null)
			{
				sp.Write(sw, "sp");
			}
			else if (connector != null)
			{
				connector.Write(sw, "cxnSp");
			}
			else if (groupShape != null)
			{
				groupShape.Write(sw, "grpSp");
			}
			else if (graphicalObjectField != null)
			{
				graphicalObjectField.Write(sw, "graphicFrame");
			}
			else if (pictureField != null)
			{
				picture.Write(sw, "pic");
			}
			if (clientData != null)
			{
				clientData.Write(sw, "clientData");
			}
			sw.Write("</xdr:oneCellAnchor>");
		}

		internal static CT_OneCellAnchor Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			CT_OneCellAnchor cT_OneCellAnchor = new CT_OneCellAnchor();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "from")
				{
					cT_OneCellAnchor.from = CT_Marker.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ext")
				{
					cT_OneCellAnchor.ext = CT_PositiveSize2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sp")
				{
					cT_OneCellAnchor.sp = CT_Shape.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pic")
				{
					cT_OneCellAnchor.picture = CT_Picture.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cxnSp")
				{
					cT_OneCellAnchor.connector = CT_Connector.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grpSp")
				{
					cT_OneCellAnchor.groupShape = CT_GroupShape.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "graphicFrame")
				{
					cT_OneCellAnchor.graphicFrame = CT_GraphicalObjectFrame.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "clientData")
				{
					cT_OneCellAnchor.clientData = CT_AnchorClientData.Parse(childNode, namespaceManager);
				}
			}
			return cT_OneCellAnchor;
		}
	}
}
