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
	public class CT_NumRef
	{
		private string fField;

		private CT_NumData numCacheField;

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
		public CT_NumData numCache
		{
			get
			{
				return numCacheField;
			}
			set
			{
				numCacheField = value;
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

		public static CT_NumRef Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumRef cT_NumRef = new CT_NumRef();
			cT_NumRef.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "f")
				{
					cT_NumRef.f = childNode.InnerText;
				}
				else if (childNode.LocalName == "numCache")
				{
					cT_NumRef.numCache = CT_NumData.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_NumRef.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_NumRef;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (f != null)
			{
				sw.Write(string.Format("<c:f>{0}</c:f>", f));
			}
			if (numCache != null)
			{
				numCache.Write(sw, "numCache");
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

		public CT_NumData AddNewNumCache()
		{
			numCacheField = new CT_NumData();
			return numCacheField;
		}
	}
}
