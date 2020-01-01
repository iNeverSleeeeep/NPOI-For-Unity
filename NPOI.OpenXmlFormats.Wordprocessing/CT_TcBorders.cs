using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TcBorders
	{
		private CT_Border topField;

		private CT_Border leftField;

		private CT_Border bottomField;

		private CT_Border rightField;

		private CT_Border insideHField;

		private CT_Border insideVField;

		private CT_Border tl2brField;

		private CT_Border tr2blField;

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

		[XmlElement(Order = 4)]
		public CT_Border insideH
		{
			get
			{
				return insideHField;
			}
			set
			{
				insideHField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Border insideV
		{
			get
			{
				return insideVField;
			}
			set
			{
				insideVField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Border tl2br
		{
			get
			{
				return tl2brField;
			}
			set
			{
				tl2brField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_Border tr2bl
		{
			get
			{
				return tr2blField;
			}
			set
			{
				tr2blField = value;
			}
		}

		public static CT_TcBorders Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TcBorders cT_TcBorders = new CT_TcBorders();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "top")
				{
					cT_TcBorders.top = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "left")
				{
					cT_TcBorders.left = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bottom")
				{
					cT_TcBorders.bottom = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "right")
				{
					cT_TcBorders.right = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "insideH")
				{
					cT_TcBorders.insideH = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "insideV")
				{
					cT_TcBorders.insideV = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tl2br")
				{
					cT_TcBorders.tl2br = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tr2bl")
				{
					cT_TcBorders.tr2bl = CT_Border.Parse(childNode, namespaceManager);
				}
			}
			return cT_TcBorders;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
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
			if (insideH != null)
			{
				insideH.Write(sw, "insideH");
			}
			if (insideV != null)
			{
				insideV.Write(sw, "insideV");
			}
			if (tl2br != null)
			{
				tl2br.Write(sw, "tl2br");
			}
			if (tr2bl != null)
			{
				tr2bl.Write(sw, "tr2bl");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
