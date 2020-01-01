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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	public class CT_Bevel
	{
		private long wField;

		private long hField;

		private ST_BevelPresetType prstField;

		[XmlAttribute]
		[DefaultValue(typeof(long), "76200")]
		public long w
		{
			get
			{
				return wField;
			}
			set
			{
				wField = value;
			}
		}

		[DefaultValue(typeof(long), "76200")]
		[XmlAttribute]
		public long h
		{
			get
			{
				return hField;
			}
			set
			{
				hField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_BevelPresetType.circle)]
		public ST_BevelPresetType prst
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

		public static CT_Bevel Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Bevel cT_Bevel = new CT_Bevel();
			cT_Bevel.w = XmlHelper.ReadLong(node.Attributes["w"]);
			cT_Bevel.h = XmlHelper.ReadLong(node.Attributes["h"]);
			if (node.Attributes["prst"] != null)
			{
				cT_Bevel.prst = (ST_BevelPresetType)Enum.Parse(typeof(ST_BevelPresetType), node.Attributes["prst"].Value);
			}
			return cT_Bevel;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w", (double)w);
			XmlHelper.WriteAttribute(sw, "h", (double)h);
			XmlHelper.WriteAttribute(sw, "prst", prst.ToString());
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_Bevel()
		{
			wField = 76200L;
			hField = 76200L;
			prstField = ST_BevelPresetType.circle;
		}
	}
}
