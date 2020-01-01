using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TcMar
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

		public static CT_TcMar Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TcMar cT_TcMar = new CT_TcMar();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "top")
				{
					cT_TcMar.top = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "left")
				{
					cT_TcMar.left = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bottom")
				{
					cT_TcMar.bottom = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "right")
				{
					cT_TcMar.right = CT_TblWidth.Parse(childNode, namespaceManager);
				}
			}
			return cT_TcMar;
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
	}
}
