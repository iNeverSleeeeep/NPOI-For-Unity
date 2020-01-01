using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_PresetTextShape
	{
		private List<CT_GeomGuide> avLstField;

		private ST_TextShapeType prstField;

		[XmlArray(Order = 0)]
		[XmlArrayItem("gd", IsNullable = false)]
		public List<CT_GeomGuide> avLst
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
		public ST_TextShapeType prst
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

		public static CT_PresetTextShape Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PresetTextShape cT_PresetTextShape = new CT_PresetTextShape();
			if (node.Attributes["prst"] != null)
			{
				cT_PresetTextShape.prst = (ST_TextShapeType)Enum.Parse(typeof(ST_TextShapeType), node.Attributes["prst"].Value);
			}
			cT_PresetTextShape.avLst = new List<CT_GeomGuide>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "avLst")
				{
					cT_PresetTextShape.avLst.Add(CT_GeomGuide.Parse(childNode, namespaceManager));
				}
			}
			return cT_PresetTextShape;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "prst", prst.ToString());
			sw.Write(">");
			if (avLst != null)
			{
				foreach (CT_GeomGuide item in avLst)
				{
					item.Write(sw, "avLst");
				}
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
