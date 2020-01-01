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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_Path2D
	{
		private ItemsChoiceType[] itemsElementNameField;

		private long wField;

		private long hField;

		private ST_PathFillMode fillField;

		private bool strokeField;

		private bool extrusionOkField;

		[DefaultValue(typeof(long), "0")]
		[XmlAttribute]
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

		[XmlAttribute]
		[DefaultValue(typeof(long), "0")]
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

		[DefaultValue(ST_PathFillMode.norm)]
		[XmlAttribute]
		public ST_PathFillMode fill
		{
			get
			{
				return fillField;
			}
			set
			{
				fillField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool stroke
		{
			get
			{
				return strokeField;
			}
			set
			{
				strokeField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool extrusionOk
		{
			get
			{
				return extrusionOkField;
			}
			set
			{
				extrusionOkField = value;
			}
		}

		public static CT_Path2D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Path2D cT_Path2D = new CT_Path2D();
			cT_Path2D.w = XmlHelper.ReadLong(node.Attributes["w"]);
			cT_Path2D.h = XmlHelper.ReadLong(node.Attributes["h"]);
			if (node.Attributes["fill"] != null)
			{
				cT_Path2D.fill = (ST_PathFillMode)Enum.Parse(typeof(ST_PathFillMode), node.Attributes["fill"].Value);
			}
			cT_Path2D.stroke = XmlHelper.ReadBool(node.Attributes["stroke"]);
			cT_Path2D.extrusionOk = XmlHelper.ReadBool(node.Attributes["extrusionOk"]);
			return cT_Path2D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w", (double)w);
			XmlHelper.WriteAttribute(sw, "h", (double)h);
			XmlHelper.WriteAttribute(sw, "fill", fill.ToString());
			XmlHelper.WriteAttribute(sw, "stroke", stroke);
			XmlHelper.WriteAttribute(sw, "extrusionOk", extrusionOk);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_Path2D()
		{
			wField = 0L;
			hField = 0L;
			fillField = ST_PathFillMode.norm;
			strokeField = true;
			extrusionOkField = true;
		}
	}
}
