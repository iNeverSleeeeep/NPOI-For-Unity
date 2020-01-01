using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Border
	{
		private CT_BorderPr leftField;

		private CT_BorderPr rightField;

		private CT_BorderPr topField;

		private CT_BorderPr bottomField;

		private CT_BorderPr diagonalField;

		private CT_BorderPr verticalField;

		private CT_BorderPr horizontalField;

		private bool diagonalUpField;

		private bool diagonalUpFieldSpecified;

		private bool diagonalDownField;

		private bool diagonalDownFieldSpecified;

		private bool outlineField;

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
		public CT_BorderPr left
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

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
		public CT_BorderPr right
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

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
		public CT_BorderPr top
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

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
		public CT_BorderPr bottom
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

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
		public CT_BorderPr diagonal
		{
			get
			{
				return diagonalField;
			}
			set
			{
				diagonalField = value;
			}
		}

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
		public CT_BorderPr vertical
		{
			get
			{
				return verticalField;
			}
			set
			{
				verticalField = value;
			}
		}

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
		public CT_BorderPr horizontal
		{
			get
			{
				return horizontalField;
			}
			set
			{
				horizontalField = value;
			}
		}

		[XmlAttribute]
		public bool diagonalUp
		{
			get
			{
				return diagonalUpField;
			}
			set
			{
				diagonalUpField = value;
			}
		}

		[XmlIgnore]
		public bool diagonalUpSpecified
		{
			get
			{
				return diagonalUpFieldSpecified;
			}
			set
			{
				diagonalUpFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool diagonalDown
		{
			get
			{
				return diagonalDownField;
			}
			set
			{
				diagonalDownField = value;
			}
		}

		[XmlIgnore]
		public bool diagonalDownSpecified
		{
			get
			{
				return diagonalDownFieldSpecified;
			}
			set
			{
				diagonalDownFieldSpecified = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool outline
		{
			get
			{
				return outlineField;
			}
			set
			{
				outlineField = value;
			}
		}

		public override string ToString()
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			Write(streamWriter, "border");
			streamWriter.Flush();
			memoryStream.Position = 0L;
			using (StreamReader streamReader = new StreamReader(memoryStream))
			{
				return streamReader.ReadToEnd();
			}
		}

		public static CT_Border Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Border cT_Border = new CT_Border();
			cT_Border.diagonalUp = XmlHelper.ReadBool(node.Attributes["diagonalUp"]);
			cT_Border.diagonalDown = XmlHelper.ReadBool(node.Attributes["diagonalDown"]);
			cT_Border.outline = XmlHelper.ReadBool(node.Attributes["outline"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "left")
				{
					cT_Border.left = CT_BorderPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "right")
				{
					cT_Border.right = CT_BorderPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "top")
				{
					cT_Border.top = CT_BorderPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bottom")
				{
					cT_Border.bottom = CT_BorderPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "diagonal")
				{
					cT_Border.diagonal = CT_BorderPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vertical")
				{
					cT_Border.vertical = CT_BorderPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "horizontal")
				{
					cT_Border.horizontal = CT_BorderPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_Border;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "diagonalUp", diagonalUp, false);
			XmlHelper.WriteAttribute(sw, "diagonalDown", diagonalDown, false);
			XmlHelper.WriteAttribute(sw, "outline", outline, false);
			sw.Write(">");
			if (left != null)
			{
				left.Write(sw, "left");
			}
			if (right != null)
			{
				right.Write(sw, "right");
			}
			if (top != null)
			{
				top.Write(sw, "top");
			}
			if (bottom != null)
			{
				bottom.Write(sw, "bottom");
			}
			if (diagonal != null)
			{
				diagonal.Write(sw, "diagonal");
			}
			if (vertical != null)
			{
				vertical.Write(sw, "vertical");
			}
			if (horizontal != null)
			{
				horizontal.Write(sw, "horizontal");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_Border Copy()
		{
			CT_Border cT_Border = new CT_Border();
			cT_Border.bottomField = bottomField;
			cT_Border.topField = topField;
			cT_Border.rightField = rightField;
			cT_Border.leftField = leftField;
			cT_Border.horizontalField = horizontalField;
			cT_Border.verticalField = verticalField;
			cT_Border.outlineField = outlineField;
			return cT_Border;
		}

		public CT_BorderPr AddNewDiagonal()
		{
			if (diagonalField == null)
			{
				diagonalField = new CT_BorderPr();
			}
			return diagonalField;
		}

		public bool IsSetDiagonal()
		{
			return diagonalField != null;
		}

		public void unsetDiagonal()
		{
			diagonalField = null;
		}

		public void unsetRight()
		{
			rightField = null;
		}

		public void unsetLeft()
		{
			leftField = null;
		}

		public void unsetTop()
		{
			topField = null;
		}

		public void unsetBottom()
		{
			bottomField = null;
		}

		public bool IsSetBottom()
		{
			return bottomField != null;
		}

		public bool IsSetLeft()
		{
			return leftField != null;
		}

		public bool IsSetRight()
		{
			return rightField != null;
		}

		public bool IsSetTop()
		{
			return topField != null;
		}

		public bool IsSetBorder()
		{
			if (leftField == null && rightField == null && topField == null)
			{
				return bottomField != null;
			}
			return true;
		}

		public CT_BorderPr AddNewTop()
		{
			if (topField == null)
			{
				topField = new CT_BorderPr();
			}
			return topField;
		}

		public CT_BorderPr AddNewRight()
		{
			if (rightField == null)
			{
				rightField = new CT_BorderPr();
			}
			return rightField;
		}

		public CT_BorderPr AddNewLeft()
		{
			if (leftField == null)
			{
				leftField = new CT_BorderPr();
			}
			return leftField;
		}

		public CT_BorderPr AddNewBottom()
		{
			if (bottomField == null)
			{
				bottomField = new CT_BorderPr();
			}
			return bottomField;
		}
	}
}
