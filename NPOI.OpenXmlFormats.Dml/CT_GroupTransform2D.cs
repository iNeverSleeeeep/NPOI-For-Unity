using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_GroupTransform2D
	{
		private CT_Point2D offField;

		private CT_PositiveSize2D extField;

		private CT_Point2D chOffField;

		private CT_PositiveSize2D chExtField;

		private int rotField;

		private bool flipHField;

		private bool flipVField;

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

		[XmlElement(Order = 2)]
		public CT_Point2D chOff
		{
			get
			{
				return chOffField;
			}
			set
			{
				chOffField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_PositiveSize2D chExt
		{
			get
			{
				return chExtField;
			}
			set
			{
				chExtField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int rot
		{
			get
			{
				return rotField;
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
				return flipHField;
			}
			set
			{
				flipHField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool flipV
		{
			get
			{
				return flipVField;
			}
			set
			{
				flipVField = value;
			}
		}

		public CT_GroupTransform2D()
		{
			rotField = 0;
			flipHField = false;
			flipVField = false;
		}

		public static CT_GroupTransform2D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GroupTransform2D cT_GroupTransform2D = new CT_GroupTransform2D();
			cT_GroupTransform2D.rot = XmlHelper.ReadInt(node.Attributes["rot"]);
			cT_GroupTransform2D.flipH = XmlHelper.ReadBool(node.Attributes["flipH"]);
			cT_GroupTransform2D.flipV = XmlHelper.ReadBool(node.Attributes["flipV"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "off")
				{
					cT_GroupTransform2D.off = CT_Point2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ext")
				{
					cT_GroupTransform2D.ext = CT_PositiveSize2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "chOff")
				{
					cT_GroupTransform2D.chOff = CT_Point2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "chExt")
				{
					cT_GroupTransform2D.chExt = CT_PositiveSize2D.Parse(childNode, namespaceManager);
				}
			}
			return cT_GroupTransform2D;
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
			if (chOff != null)
			{
				chOff.Write(sw, "chOff");
			}
			if (chExt != null)
			{
				chExt.Write(sw, "a:chExt");
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

		public CT_PositiveSize2D AddNewChExt()
		{
			chExtField = new CT_PositiveSize2D();
			return chExtField;
		}

		public CT_Point2D AddNewChOff()
		{
			chOffField = new CT_Point2D();
			return chOffField;
		}
	}
}
