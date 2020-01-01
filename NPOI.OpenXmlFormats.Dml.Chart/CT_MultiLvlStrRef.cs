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
	public class CT_MultiLvlStrRef
	{
		private string fField;

		private CT_MultiLvlStrData multiLvlStrCacheField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public string f
		{
			get
			{
				return fField;
			}
			set
			{
				fField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_MultiLvlStrData multiLvlStrCache
		{
			get
			{
				return multiLvlStrCacheField;
			}
			set
			{
				multiLvlStrCacheField = value;
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

		public static CT_MultiLvlStrRef Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MultiLvlStrRef cT_MultiLvlStrRef = new CT_MultiLvlStrRef();
			cT_MultiLvlStrRef.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "f")
				{
					cT_MultiLvlStrRef.f = childNode.InnerText;
				}
				else if (childNode.LocalName == "multiLvlStrCache")
				{
					cT_MultiLvlStrRef.multiLvlStrCache = CT_MultiLvlStrData.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_MultiLvlStrRef.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_MultiLvlStrRef;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (f != null)
			{
				sw.Write(string.Format("<c:f>{0}</c:f>", f));
			}
			if (multiLvlStrCache != null)
			{
				multiLvlStrCache.Write(sw, "multiLvlStrCache");
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
