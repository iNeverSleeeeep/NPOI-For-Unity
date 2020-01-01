using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_Transform2D
	{
		private CT_Point2D offField;

		private CT_PositiveSize2D extField;

		private int? rotField = null;

		private bool? flipHField = null;

		private bool? flipVField = null;

		[XmlElement(Order = 0)]
		public CT_Point2D off
		{
			get
			{
				return offField;
			}
			set
			{
				offField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_PositiveSize2D ext
		{
			get
			{
				return extField;
			}
			set
			{
				extField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int rot
		{
			get
			{
				if (rotField.HasValue)
				{
					return rotField.Value;
				}
				return 0;
			}
			set
			{
				rotField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool flipH
		{
			get
			{
				if (flipHField.HasValue)
				{
					return flipHField.Value;
				}
				return false;
			}
			set
			{
				flipHField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool flipV
		{
			get
			{
				if (flipVField.HasValue)
				{
					return flipVField.Value;
				}
				return false;
			}
			set
			{
				flipVField = value;
			}
		}

		public static CT_Transform2D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Transform2D cT_Transform2D = new CT_Transform2D();
			cT_Transform2D.rot = XmlHelper.ReadInt(node.Attributes["rot"]);
			cT_Transform2D.flipH = XmlHelper.ReadBool(node.Attributes["flipH"]);
			cT_Transform2D.flipV = XmlHelper.ReadBool(node.Attributes["flipV"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "off")
				{
					cT_Transform2D.off = CT_Point2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ext")
				{
					cT_Transform2D.ext = CT_PositiveSize2D.Parse(childNode, namespaceManager);
				}
			}
			return cT_Transform2D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "rot", rot);
			XmlHelper.WriteAttribute(sw, "flipH", flipH, false);
			XmlHelper.WriteAttribute(sw, "flipV", flipV, false);
			sw.Write(">");
			if (off != null)
			{
				off.Write(sw, "off");
			}
			if (ext != null)
			{
				ext.Write(sw, "a:ext");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_PositiveSize2D AddNewExt()
		{
			extField = new CT_PositiveSize2D();
			return extField;
		}

		public CT_Point2D AddNewOff()
		{
			offField = new CT_Point2D();
			return offField;
		}
	}
}
