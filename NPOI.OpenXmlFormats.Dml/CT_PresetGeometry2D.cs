using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_PresetGeometry2D
	{
		private CT_GeomGuideList avLstField;

		private ST_ShapeType prstField;

		[XmlElement(Order = 0)]
		public CT_GeomGuideList avLst
		{
			get
			{
				return avLstField;
			}
			set
			{
				avLstField = value;
			}
		}

		[XmlAttribute]
		public ST_ShapeType prst
		{
			get
			{
				return prstField;
			}
			set
			{
				prstField = value;
			}
		}

		public CT_GeomGuideList AddNewAvLst()
		{
			avLstField = new CT_GeomGuideList();
			return avLstField;
		}

		public static CT_PresetGeometry2D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PresetGeometry2D cT_PresetGeometry2D = new CT_PresetGeometry2D();
			if (node.Attributes["prst"] != null)
			{
				cT_PresetGeometry2D.prst = (ST_ShapeType)Enum.Parse(typeof(ST_ShapeType), node.Attributes["prst"].Value);
			}
			if (node.ChildNodes != null)
			{
				foreach (XmlNode childNode in node.ChildNodes)
				{
					if (childNode.LocalName == "avLst")
					{
						cT_PresetGeometry2D.avLstField = CT_GeomGuideList.Parse(childNode, namespaceManager);
					}
				}
				return cT_PresetGeometry2D;
			}
			return cT_PresetGeometry2D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "prst", prst.ToString());
			sw.Write(">");
			if (avLst != null)
			{
				avLst.Write(sw, "avLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
