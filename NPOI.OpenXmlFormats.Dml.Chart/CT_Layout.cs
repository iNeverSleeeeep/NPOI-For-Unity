using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_Layout
	{
		private CT_ManualLayout manualLayoutField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_ManualLayout manualLayout
		{
			get
			{
				return manualLayoutField;
			}
			set
			{
				manualLayoutField = value;
			}
		}

		[XmlElement(Order = 1)]
		public List<CT_Extension> extLst
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

		public static CT_Layout Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Layout cT_Layout = new CT_Layout();
			cT_Layout.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "manualLayout")
				{
					cT_Layout.manualLayout = CT_ManualLayout.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Layout.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_Layout;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (manualLayout != null)
			{
				manualLayout.Write(sw, "manualLayout");
			}
			if (extLst != null)
			{
				foreach (CT_Extension item in extLst)
				{
					item.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public bool IsSetManualLayout()
		{
			return manualLayout != null;
		}

		public CT_ManualLayout AddNewManualLayout()
		{
			manualLayoutField = new CT_ManualLayout();
			return manualLayoutField;
		}
	}
}
