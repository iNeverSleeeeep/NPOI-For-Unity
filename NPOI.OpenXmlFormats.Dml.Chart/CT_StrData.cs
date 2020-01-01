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
	public class CT_StrData
	{
		private CT_UnsignedInt ptCountField;

		private List<CT_StrVal> ptField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
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

		[XmlElement("pt", Order = 1)]
		public List<CT_StrVal> pt
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

		public static CT_StrData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_StrData cT_StrData = new CT_StrData();
			cT_StrData.pt = new List<CT_StrVal>();
			cT_StrData.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ptCount")
				{
					cT_StrData.ptCount = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pt")
				{
					cT_StrData.pt.Add(CT_StrVal.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_StrData.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_StrData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (ptCount != null)
			{
				ptCount.Write(sw, "ptCount");
			}
			if (pt != null)
			{
				foreach (CT_StrVal item in pt)
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

		public CT_UnsignedInt AddNewPtCount()
		{
			ptCount = new CT_UnsignedInt();
			return ptCount;
		}

		public CT_StrVal AddNewPt()
		{
			if (pt == null)
			{
				pt = new List<CT_StrVal>();
			}
			CT_StrVal cT_StrVal = new CT_StrVal();
			pt.Add(cT_StrVal);
			return cT_StrVal;
		}
	}
}
