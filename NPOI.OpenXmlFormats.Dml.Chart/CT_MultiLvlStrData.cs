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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_MultiLvlStrData
	{
		private CT_UnsignedInt ptCountField;

		private List<CT_StrVal> lvlField;

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

		[XmlElement(Order = 1)]
		public List<CT_StrVal> lvl
		{
			get
			{
				return lvlField;
			}
			set
			{
				lvlField = value;
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

		public static CT_MultiLvlStrData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MultiLvlStrData cT_MultiLvlStrData = new CT_MultiLvlStrData();
			cT_MultiLvlStrData.lvl = new List<CT_StrVal>();
			cT_MultiLvlStrData.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ptCount")
				{
					cT_MultiLvlStrData.ptCount = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lvl")
				{
					cT_MultiLvlStrData.lvl.Add(CT_StrVal.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_MultiLvlStrData.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_MultiLvlStrData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (ptCount != null)
			{
				ptCount.Write(sw, "ptCount");
			}
			if (lvl != null)
			{
				foreach (CT_StrVal item in lvl)
				{
					item.Write(sw, "lvl");
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
	}
}
