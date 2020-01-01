using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TblGridChange : CT_Markup
	{
		private List<CT_TblGridCol> tblGridField;

		[XmlArrayItem("gridCol", IsNullable = false)]
		[XmlArray(Order = 0)]
		public List<CT_TblGridCol> tblGrid
		{
			get
			{
				return tblGridField;
			}
			set
			{
				tblGridField = value;
			}
		}

		public new static CT_TblGridChange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblGridChange cT_TblGridChange = new CT_TblGridChange();
			cT_TblGridChange.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			cT_TblGridChange.tblGrid = new List<CT_TblGridCol>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tblGrid")
				{
					cT_TblGridChange.tblGrid.Add(CT_TblGridCol.Parse(childNode, namespaceManager));
				}
			}
			return cT_TblGridChange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r:id", base.id);
			sw.Write(">");
			if (tblGrid != null)
			{
				foreach (CT_TblGridCol item in tblGrid)
				{
					item.Write(sw, "tblGrid");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
