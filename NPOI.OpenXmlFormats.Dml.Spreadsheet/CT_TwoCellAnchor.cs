using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_TwoCellAnchor : IEG_Anchor
	{
		private CT_Marker fromField = new CT_Marker();

		private CT_Marker toField = new CT_Marker();

		private CT_Shape shapeField;

		private CT_GroupShape groupShapeField;

		private CT_GraphicalObjectFrame graphicalObjectField;

		private CT_Connector connectorField;

		private CT_Picture pictureField;

		private CT_AnchorClientData clientDataField;

		private ST_EditAs editAsField;

		private bool editAsSpecifiedField;

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

		[XmlAttribute]
		public ST_EditAs editAs
		{
			get
			{
				return editAsField;
			}
			set
			{
				editAsField = value;
			}
		}

		[XmlIgnore]
		public bool editAsSpecified
		{
			get
			{
				return editAsSpecifiedField;
			}
			set
			{
				editAsSpecifiedField = value;
			}
		}

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
		public CT_Marker to
		{
			get
			{
				return toField;
			}
			set
			{
				toField = value;
			}
		}

		[XmlElement]
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

		[XmlElement]
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

		[XmlElement]
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

		[XmlElement]
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

		[XmlElement("pic")]
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

		public CT_TwoCellAnchor()
		{
			clientDataField = new CT_AnchorClientData();
		}

		public CT_Shape AddNewSp()
		{
			shapeField = new CT_Shape();
			return shapeField;
		}

		public CT_GroupShape AddNewGrpSp()
		{
			groupShapeField = new CT_GroupShape();
			return groupShapeField;
		}

		public CT_GraphicalObjectFrame AddNewGraphicFrame()
		{
			graphicalObjectField = new CT_GraphicalObjectFrame();
			return graphicalObjectField;
		}

		public CT_Connector AddNewCxnSp()
		{
			connectorField = new CT_Connector();
			return connectorField;
		}

		public CT_Picture AddNewPic()
		{
			pictureField = new CT_Picture();
			return pictureField;
		}

		public CT_AnchorClientData AddNewClientData()
		{
			clientDataField = new CT_AnchorClientData();
			return clientDataField;
		}

		public void Write(StreamWriter sw)
		{
			sw.Write("<xdr:twoCellAnchor");
			if (editAsField != 0)
			{
				sw.Write(string.Format(" editAs=\"{0}\"", editAsField.ToString()));
			}
			sw.Write(">");
			from.Write(sw, "xdr:from");
			to.Write(sw, "xdr:to");
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
			sw.Write("</xdr:twoCellAnchor>");
		}

		internal static CT_TwoCellAnchor Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			CT_TwoCellAnchor cT_TwoCellAnchor = new CT_TwoCellAnchor();
			if (node.Attributes["editAs"] != null)
			{
				cT_TwoCellAnchor.editAs = (ST_EditAs)Enum.Parse(typeof(ST_EditAs), node.Attributes["editAs"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "from")
				{
					cT_TwoCellAnchor.from = CT_Marker.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "to")
				{
					cT_TwoCellAnchor.to = CT_Marker.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sp")
				{
					cT_TwoCellAnchor.sp = CT_Shape.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pic")
				{
					cT_TwoCellAnchor.picture = CT_Picture.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cxnSp")
				{
					cT_TwoCellAnchor.connector = CT_Connector.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grpSp")
				{
					cT_TwoCellAnchor.groupShape = CT_GroupShape.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "graphicFrame")
				{
					cT_TwoCellAnchor.graphicFrame = CT_GraphicalObjectFrame.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "clientData")
				{
					cT_TwoCellAnchor.clientData = CT_AnchorClientData.Parse(childNode, namespaceManager);
				}
			}
			return cT_TwoCellAnchor;
		}
	}
}
