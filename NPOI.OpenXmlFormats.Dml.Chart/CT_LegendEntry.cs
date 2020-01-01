using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_LegendEntry
	{
		private CT_UnsignedInt idxField;

		private object itemField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_UnsignedInt idx
		{
			get
			{
				return idxField;
			}
			set
			{
				idxField = value;
			}
		}

		[XmlElement("delete", typeof(CT_Boolean), Order = 1)]
		[XmlElement("txPr", typeof(CT_TextBody), Order = 1)]
		public object Item
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
			}
		}

		[XmlArrayItem("ext", IsNullable = false)]
		[XmlArray(Order = 2)]
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

		public static CT_LegendEntry Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LegendEntry cT_LegendEntry = new CT_LegendEntry();
			cT_LegendEntry.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "idx")
				{
					cT_LegendEntry.idx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "Item")
				{
					cT_LegendEntry.Item = new object();
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_LegendEntry.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_LegendEntry;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (idx != null)
			{
				idx.Write(sw, "idx");
			}
			if (Item != null)
			{
				sw.Write("<Item/>");
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
