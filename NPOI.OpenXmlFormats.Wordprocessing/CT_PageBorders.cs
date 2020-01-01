using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_PageBorders
	{
		private CT_Border topField;

		private CT_Border leftField;

		private CT_Border bottomField;

		private CT_Border rightField;

		private ST_PageBorderZOrder zOrderField;

		private bool zOrderFieldSpecified;

		private ST_PageBorderDisplay displayField;

		private bool displayFieldSpecified;

		private ST_PageBorderOffset offsetFromField;

		private bool offsetFromFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_Border top
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

		[XmlElement(Order = 1)]
		public CT_Border left
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

		[XmlElement(Order = 2)]
		public CT_Border bottom
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

		[XmlElement(Order = 3)]
		public CT_Border right
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_PageBorderZOrder zOrder
		{
			get
			{
				return zOrderField;
			}
			set
			{
				zOrderField = value;
			}
		}

		[XmlIgnore]
		public bool zOrderSpecified
		{
			get
			{
				return zOrderFieldSpecified;
			}
			set
			{
				zOrderFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_PageBorderDisplay display
		{
			get
			{
				return displayField;
			}
			set
			{
				displayField = value;
			}
		}

		[XmlIgnore]
		public bool displaySpecified
		{
			get
			{
				return displayFieldSpecified;
			}
			set
			{
				displayFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_PageBorderOffset offsetFrom
		{
			get
			{
				return offsetFromField;
			}
			set
			{
				offsetFromField = value;
			}
		}

		[XmlIgnore]
		public bool offsetFromSpecified
		{
			get
			{
				return offsetFromFieldSpecified;
			}
			set
			{
				offsetFromFieldSpecified = value;
			}
		}

		public static CT_PageBorders Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PageBorders cT_PageBorders = new CT_PageBorders();
			if (node.Attributes["w:zOrder"] != null)
			{
				cT_PageBorders.zOrder = (ST_PageBorderZOrder)Enum.Parse(typeof(ST_PageBorderZOrder), node.Attributes["w:zOrder"].Value);
			}
			if (node.Attributes["w:display"] != null)
			{
				cT_PageBorders.display = (ST_PageBorderDisplay)Enum.Parse(typeof(ST_PageBorderDisplay), node.Attributes["w:display"].Value);
			}
			if (node.Attributes["w:offsetFrom"] != null)
			{
				cT_PageBorders.offsetFrom = (ST_PageBorderOffset)Enum.Parse(typeof(ST_PageBorderOffset), node.Attributes["w:offsetFrom"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "top")
				{
					cT_PageBorders.top = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "left")
				{
					cT_PageBorders.left = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bottom")
				{
					cT_PageBorders.bottom = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "right")
				{
					cT_PageBorders.right = CT_Border.Parse(childNode, namespaceManager);
				}
			}
			return cT_PageBorders;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:zOrder", zOrder.ToString());
			XmlHelper.WriteAttribute(sw, "w:display", display.ToString());
			XmlHelper.WriteAttribute(sw, "w:offsetFrom", offsetFrom.ToString());
			sw.Write(">");
			if (top != null)
			{
				top.Write(sw, "top");
			}
			if (left != null)
			{
				left.Write(sw, "left");
			}
			if (bottom != null)
			{
				bottom.Write(sw, "bottom");
			}
			if (right != null)
			{
				right.Write(sw, "right");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
