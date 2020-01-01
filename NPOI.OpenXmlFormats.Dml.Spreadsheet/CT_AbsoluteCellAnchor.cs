using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	public class CT_AbsoluteCellAnchor : IEG_Anchor
	{
		private CT_Point2D posField;

		private CT_PositiveSize2D extField;

		private CT_AnchorClientData clientDataField = new CT_AnchorClientData();

		private CT_Shape shapeField;

		private CT_GroupShape groupShapeField;

		private CT_GraphicalObjectFrame graphicalObjectField;

		private CT_Connector connectorField;

		private CT_Picture pictureField;

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

		public CT_Point2D pos
		{
			get
			{
				return posField;
			}
			set
			{
				posField = value;
			}
		}

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

		public static CT_AbsoluteCellAnchor Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			CT_AbsoluteCellAnchor cT_AbsoluteCellAnchor = new CT_AbsoluteCellAnchor();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pos")
				{
					cT_AbsoluteCellAnchor.pos = CT_Point2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ext")
				{
					cT_AbsoluteCellAnchor.ext = CT_PositiveSize2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sp")
				{
					cT_AbsoluteCellAnchor.sp = CT_Shape.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pic")
				{
					cT_AbsoluteCellAnchor.picture = CT_Picture.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cxnSp")
				{
					cT_AbsoluteCellAnchor.connector = CT_Connector.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grpSp")
				{
					cT_AbsoluteCellAnchor.groupShape = CT_GroupShape.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "graphicFrame")
				{
					cT_AbsoluteCellAnchor.graphicFrame = CT_GraphicalObjectFrame.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "clientData")
				{
					cT_AbsoluteCellAnchor.clientData = CT_AnchorClientData.Parse(childNode, namespaceManager);
				}
			}
			return cT_AbsoluteCellAnchor;
		}

		public CT_Point2D AddNewOff()
		{
			posField = new CT_Point2D();
			return posField;
		}

		public CT_AnchorClientData AddNewClientData()
		{
			clientDataField = new CT_AnchorClientData();
			return clientDataField;
		}

		public void Write(StreamWriter sw)
		{
			sw.Write("<xdr:absCellAnchor>");
			if (pos != null)
			{
				pos.Write(sw, "pos");
			}
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
			sw.Write("</xdr:oneCellAnchor>");
			if (clientData != null)
			{
				clientData.Write(sw, "clientData");
			}
			sw.Write("</xdr:absCellAnchor");
		}
	}
}
