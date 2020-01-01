using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_NumData
	{
		private string formatCodeField;

		private CT_UnsignedInt ptCountField;

		private List<CT_NumVal> ptField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public string formatCode
		{
			get
			{
				return formatCodeField;
			}
			set
			{
				formatCodeField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_UnsignedInt ptCount
		{
			get
			{
				return ptCountField;
			}
			set
			{
				ptCountField = value;
			}
		}

		[XmlElement("pt", Order = 2)]
		public List<CT_NumVal> pt
		{
			get
			{
				return ptField;
			}
			set
			{
				ptField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		public static CT_NumData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumData cT_NumData = new CT_NumData();
			cT_NumData.pt = new List<CT_NumVal>();
			cT_NumData.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "formatCode")
				{
					cT_NumData.formatCode = childNode.InnerText;
				}
				else if (childNode.LocalName == "ptCount")
				{
					cT_NumData.ptCount = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pt")
				{
					cT_NumData.pt.Add(CT_NumVal.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_NumData.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_NumData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (formatCode != null)
			{
				sw.Write(string.Format("<c:formatCode>{0}</c:formatCode>", formatCode));
			}
			if (ptCount != null)
			{
				ptCount.Write(sw, "ptCount");
			}
			if (pt != null)
			{
				foreach (CT_NumVal item in pt)
				{
					item.Write(sw, "pt");
				}
			}
			if (extLst != null)
			{
				foreach (CT_Extension item2 in extLst)
				{
					item2.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_NumVal AddNewPt()
		{
			if (ptField == null)
			{
				ptField = new List<CT_NumVal>();
			}
			CT_NumVal cT_NumVal = new CT_NumVal();
			ptField.Add(cT_NumVal);
			return cT_NumVal;
		}

		public CT_UnsignedInt AddNewPtCount()
		{
			ptCountField = new CT_UnsignedInt();
			return ptCountField;
		}
	}
}
