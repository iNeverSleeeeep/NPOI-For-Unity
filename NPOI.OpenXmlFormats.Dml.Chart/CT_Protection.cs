using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_Protection
	{
		private CT_Boolean chartObjectField;

		private CT_Boolean dataField;

		private CT_Boolean formattingField;

		private CT_Boolean selectionField;

		private CT_Boolean userInterfaceField;

		[XmlElement(Order = 0)]
		public CT_Boolean chartObject
		{
			get
			{
				return chartObjectField;
			}
			set
			{
				chartObjectField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Boolean data
		{
			get
			{
				return dataField;
			}
			set
			{
				dataField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Boolean formatting
		{
			get
			{
				return formattingField;
			}
			set
			{
				formattingField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Boolean selection
		{
			get
			{
				return selectionField;
			}
			set
			{
				selectionField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Boolean userInterface
		{
			get
			{
				return userInterfaceField;
			}
			set
			{
				userInterfaceField = value;
			}
		}

		public static CT_Protection Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Protection cT_Protection = new CT_Protection();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "chartObject")
				{
					cT_Protection.chartObject = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "data")
				{
					cT_Protection.data = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "formatting")
				{
					cT_Protection.formatting = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "selection")
				{
					cT_Protection.selection = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "userInterface")
				{
					cT_Protection.userInterface = CT_Boolean.Parse(childNode, namespaceManager);
				}
			}
			return cT_Protection;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (chartObject != null)
			{
				chartObject.Write(sw, "chartObject");
			}
			if (data != null)
			{
				data.Write(sw, "data");
			}
			if (formatting != null)
			{
				formatting.Write(sw, "formatting");
			}
			if (selection != null)
			{
				selection.Write(sw, "selection");
			}
			if (userInterface != null)
			{
				userInterface.Write(sw, "userInterface");
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
