using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_PBdr
	{
		private CT_Border topField;

		private CT_Border leftField;

		private CT_Border bottomField;

		private CT_Border rightField;

		private CT_Border betweenField;

		private CT_Border barField;

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
		public CT_Border between
		{
			get
			{
				return betweenField;
			}
			set
			{
				betweenField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Border bar
		{
			get
			{
				return barField;
			}
			set
			{
				barField = value;
			}
		}

		public static CT_PBdr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PBdr cT_PBdr = new CT_PBdr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "top")
				{
					cT_PBdr.top = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "left")
				{
					cT_PBdr.left = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bottom")
				{
					cT_PBdr.bottom = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "right")
				{
					cT_PBdr.right = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "between")
				{
					cT_PBdr.between = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bar")
				{
					cT_PBdr.bar = CT_Border.Parse(childNode, namespaceManager);
				}
			}
			return cT_PBdr;
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
			if (between != null)
			{
				between.Write(sw, "between");
			}
			if (bar != null)
			{
				bar.Write(sw, "bar");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public bool IsSetTop()
		{
			if (topField != null && topField.val != ST_Border.none)
			{
				return topField.val != ST_Border.nil;
			}
			return false;
		}

		public CT_Border AddNewTop()
		{
			if (topField == null)
			{
				topField = new CT_Border();
			}
			return topField;
		}

		public void UnsetTop()
		{
			topField = new CT_Border();
		}

		public bool IsSetBottom()
		{
			if (bottomField != null && bottomField.val != ST_Border.none)
			{
				return bottomField.val != ST_Border.nil;
			}
			return false;
		}

		public CT_Border AddNewBottom()
		{
			if (bottomField == null)
			{
				bottomField = new CT_Border();
			}
			return bottomField;
		}

		public void UnsetBottom()
		{
			bottomField = new CT_Border();
		}

		public bool IsSetRight()
		{
			if (rightField != null && rightField.val != ST_Border.none)
			{
				return rightField.val != ST_Border.nil;
			}
			return false;
		}

		public void UnsetRight()
		{
			rightField = new CT_Border();
		}

		public CT_Border AddNewRight()
		{
			if (rightField == null)
			{
				rightField = new CT_Border();
			}
			return rightField;
		}

		public bool IsSetBetween()
		{
			if (betweenField != null && betweenField.val != ST_Border.none)
			{
				return betweenField.val != ST_Border.nil;
			}
			return false;
		}

		public CT_Border AddNewBetween()
		{
			if (betweenField == null)
			{
				betweenField = new CT_Border();
			}
			return betweenField;
		}

		public void UnsetBetween()
		{
			betweenField = new CT_Border();
		}

		public bool IsSetLeft()
		{
			if (leftField != null && leftField.val != ST_Border.none)
			{
				return leftField.val != ST_Border.nil;
			}
			return false;
		}

		public CT_Border AddNewLeft()
		{
			if (leftField == null)
			{
				leftField = new CT_Border();
			}
			return leftField;
		}

		public void UnsetLeft()
		{
			leftField = new CT_Border();
		}
	}
}
