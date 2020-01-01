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
	public class CT_StrRef
	{
		private string fField;

		private CT_StrData strCacheField;

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
		public CT_StrData strCache
		{
			get
			{
				return strCacheField;
			}
			set
			{
				strCacheField = value;
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

		public static CT_StrRef Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_StrRef cT_StrRef = new CT_StrRef();
			cT_StrRef.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "f")
				{
					cT_StrRef.f = childNode.InnerText;
				}
				else if (childNode.LocalName == "strCache")
				{
					cT_StrRef.strCache = CT_StrData.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_StrRef.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_StrRef;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (f != null)
			{
				sw.Write(string.Format("<c:f>{0}</c:f>", f));
			}
			if (strCache != null)
			{
				strCache.Write(sw, "strCache");
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

		public CT_StrData AddNewStrCache()
		{
			strCache = new CT_StrData();
			return strCache;
		}
	}
}
