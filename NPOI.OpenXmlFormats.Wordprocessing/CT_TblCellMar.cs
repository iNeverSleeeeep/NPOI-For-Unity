using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TblCellMar
	{
		private CT_TblWidth topField;

		private CT_TblWidth leftField;

		private CT_TblWidth bottomField;

		private CT_TblWidth rightField;

		[XmlElement(Order = 0)]
		public CT_TblWidth top
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
		public CT_TblWidth left
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
		public CT_TblWidth bottom
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
		public CT_TblWidth right
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

		public static CT_TblCellMar Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblCellMar cT_TblCellMar = new CT_TblCellMar();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "top")
				{
					cT_TblCellMar.top = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "left")
				{
					cT_TblCellMar.left = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bottom")
				{
					cT_TblCellMar.bottom = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "right")
				{
					cT_TblCellMar.right = CT_TblWidth.Parse(childNode, namespaceManager);
				}
			}
			return cT_TblCellMar;
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
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public bool IsSetLeft()
		{
			return leftField != null;
		}

		public bool IsSetTop()
		{
			return topField != null;
		}

		public bool IsSetBottom()
		{
			return bottomField != null;
		}

		public bool IsSetRight()
		{
			return rightField != null;
		}

		public CT_TblWidth AddNewLeft()
		{
			leftField = new CT_TblWidth();
			return leftField;
		}

		public CT_TblWidth AddNewTop()
		{
			topField = new CT_TblWidth();
			return topField;
		}

		public CT_TblWidth AddNewBottom()
		{
			bottomField = new CT_TblWidth();
			return bottomField;
		}

		public CT_TblWidth AddNewRight()
		{
			rightField = new CT_TblWidth();
			return rightField;
		}
	}
}
