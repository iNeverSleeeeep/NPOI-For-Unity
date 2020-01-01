using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_NonVisualConnectorProperties
	{
		private CT_ConnectorLocking cxnSpLocksField;

		private CT_Connection stCxnField;

		private CT_Connection endCxnField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_ConnectorLocking cxnSpLocks
		{
			get
			{
				return cxnSpLocksField;
			}
			set
			{
				cxnSpLocksField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Connection stCxn
		{
			get
			{
				return stCxnField;
			}
			set
			{
				stCxnField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Connection endCxn
		{
			get
			{
				return endCxnField;
			}
			set
			{
				endCxnField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		public static CT_NonVisualConnectorProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NonVisualConnectorProperties cT_NonVisualConnectorProperties = new CT_NonVisualConnectorProperties();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cxnSpLocks")
				{
					cT_NonVisualConnectorProperties.cxnSpLocks = CT_ConnectorLocking.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "stCxn")
				{
					cT_NonVisualConnectorProperties.stCxn = CT_Connection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "endCxn")
				{
					cT_NonVisualConnectorProperties.endCxn = CT_Connection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_NonVisualConnectorProperties.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_NonVisualConnectorProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (cxnSpLocks != null)
			{
				cxnSpLocks.Write(sw, "cxnSpLocks");
			}
			if (stCxn != null)
			{
				stCxn.Write(sw, "stCxn");
			}
			if (endCxn != null)
			{
				endCxn.Write(sw, "endCxn");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_NonVisualConnectorProperties()
		{
			extLstField = new CT_OfficeArtExtensionList();
			endCxnField = new CT_Connection();
			stCxnField = new CT_Connection();
			cxnSpLocksField = new CT_ConnectorLocking();
		}
	}
}
