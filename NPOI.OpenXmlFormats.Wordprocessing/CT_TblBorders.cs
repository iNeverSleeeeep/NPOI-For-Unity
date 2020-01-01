using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TblBorders
	{
		private CT_Border topField;

		private CT_Border leftField;

		private CT_Border bottomField;

		private CT_Border rightField;

		private CT_Border insideHField;

		private CT_Border insideVField;

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

		public static CT_TblBorders Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblBorders cT_TblBorders = new CT_TblBorders();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "top")
				{
					cT_TblBorders.top = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "left")
				{
					cT_TblBorders.left = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bottom")
				{
					cT_TblBorders.bottom = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "right")
				{
					cT_TblBorders.right = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "insideH")
				{
					cT_TblBorders.insideH = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "insideV")
				{
					cT_TblBorders.insideV = CT_Border.Parse(childNode, namespaceManager);
				}
			}
			return cT_TblBorders;
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
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_Border AddNewBottom()
		{
			if (bottomField == null)
			{
				bottomField = new CT_Border();
			}
			return bottomField;
		}

		public CT_Border AddNewLeft()
		{
			if (leftField == null)
			{
				leftField = new CT_Border();
			}
			return leftField;
		}

		public CT_Border AddNewRight()
		{
			if (rightField == null)
			{
				rightField = new CT_Border();
			}
			return rightField;
		}

		public CT_Border AddNewTop()
		{
			if (topField == null)
			{
				topField = new CT_Border();
			}
			return topField;
		}

		public CT_Border AddNewInsideH()
		{
			if (insideHField == null)
			{
				insideHField = new CT_Border();
			}
			return insideHField;
		}

		public CT_Border AddNewInsideV()
		{
			if (insideVField == null)
			{
				insideVField = new CT_Border();
			}
			return insideVField;
		}

		public bool IsSetInsideH()
		{
			return insideH != null;
		}

		public bool IsSetInsideV()
		{
			return insideV != null;
		}
	}
}
