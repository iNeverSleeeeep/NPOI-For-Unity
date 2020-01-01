using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Dml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_GradientFill
	{
		private List<CT_GradientStop> stopField;

		private ST_GradientType typeField = ST_GradientType.linear;

		private double degreeField;

		private double leftField;

		private double rightField;

		private double topField;

		private double bottomField;

		[XmlElement]
		public List<CT_GradientStop> stop
		{
			get
			{
				return stopField;
			}
			set
			{
				stopField = value;
			}
		}

		[DefaultValue(ST_GradientType.linear)]
		[XmlAttribute]
		public ST_GradientType type
		{
			get
			{
				if (typeField != 0)
				{
					return typeField;
				}
				return ST_GradientType.linear;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0.0)]
		public double degree
		{
			get
			{
				return degreeField;
			}
			set
			{
				degreeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0.0)]
		public double left
		{
			get
			{
				return leftField;
			}
			set
			{
				leftField = value;
			}
		}

		[DefaultValue(0.0)]
		[XmlAttribute]
		public double right
		{
			get
			{
				return rightField;
			}
			set
			{
				rightField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0.0)]
		public double top
		{
			get
			{
				return topField;
			}
			set
			{
				topField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0.0)]
		public double bottom
		{
			get
			{
				return bottomField;
			}
			set
			{
				bottomField = value;
			}
		}

		public static CT_GradientFill Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GradientFill cT_GradientFill = new CT_GradientFill();
			if (node.Attributes["type"] != null)
			{
				cT_GradientFill.type = (ST_GradientType)Enum.Parse(typeof(ST_GradientType), node.Attributes["type"].Value);
			}
			cT_GradientFill.degree = XmlHelper.ReadDouble(node.Attributes["degree"]);
			cT_GradientFill.left = XmlHelper.ReadDouble(node.Attributes["left"]);
			cT_GradientFill.right = XmlHelper.ReadDouble(node.Attributes["right"]);
			cT_GradientFill.top = XmlHelper.ReadDouble(node.Attributes["top"]);
			cT_GradientFill.bottom = XmlHelper.ReadDouble(node.Attributes["bottom"]);
			cT_GradientFill.stop = new List<CT_GradientStop>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "stop")
				{
					cT_GradientFill.stop.Add(CT_GradientStop.Parse(childNode, namespaceManager));
				}
			}
			return cT_GradientFill;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "type", type.ToString());
			XmlHelper.WriteAttribute(sw, "degree", degree);
			XmlHelper.WriteAttribute(sw, "left", left);
			XmlHelper.WriteAttribute(sw, "right", right);
			XmlHelper.WriteAttribute(sw, "top", top);
			XmlHelper.WriteAttribute(sw, "bottom", bottom);
			sw.Write(">");
			if (stop != null)
			{
				foreach (CT_GradientStop item in stop)
				{
					item.Write(sw, "stop");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
