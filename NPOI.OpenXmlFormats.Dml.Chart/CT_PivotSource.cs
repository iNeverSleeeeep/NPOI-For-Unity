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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_PivotSource
	{
		private string nameField;

		private CT_UnsignedInt fmtIdField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_UnsignedInt fmtId
		{
			get
			{
				return fmtIdField;
			}
			set
			{
				fmtIdField = value;
			}
		}

		[XmlElement(Order = 2)]
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

		public static CT_PivotSource Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PivotSource cT_PivotSource = new CT_PivotSource();
			cT_PivotSource.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "name")
				{
					cT_PivotSource.name = childNode.InnerText;
				}
				else if (childNode.LocalName == "fmtId")
				{
					cT_PivotSource.fmtId = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_PivotSource.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_PivotSource;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (name != null)
			{
				sw.Write(string.Format("<name>{0}</name>", name));
			}
			if (fmtId != null)
			{
				fmtId.Write(sw, "fmtId");
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
	}
}
