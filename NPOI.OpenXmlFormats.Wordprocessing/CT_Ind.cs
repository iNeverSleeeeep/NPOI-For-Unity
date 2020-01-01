using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Ind
	{
		private string leftField;

		private string leftCharsField;

		private string rightField;

		private string rightCharsField;

		private ulong hangingField;

		private bool hangingFieldSpecified;

		private string hangingCharsField;

		private long firstLineField;

		private bool firstLineFieldSpecified;

		private string firstLineCharsField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string left
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string leftChars
		{
			get
			{
				return leftCharsField;
			}
			set
			{
				leftCharsField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string right
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string rightChars
		{
			get
			{
				return rightCharsField;
			}
			set
			{
				rightCharsField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong hanging
		{
			get
			{
				return hangingField;
			}
			set
			{
				hangingField = value;
				hangingFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool hangingSpecified
		{
			get
			{
				return hangingFieldSpecified;
			}
			set
			{
				hangingFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string hangingChars
		{
			get
			{
				return hangingCharsField;
			}
			set
			{
				hangingCharsField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public long firstLine
		{
			get
			{
				return firstLineField;
			}
			set
			{
				firstLineField = value;
			}
		}

		[XmlIgnore]
		public bool firstLineSpecified
		{
			get
			{
				return firstLineFieldSpecified;
			}
			set
			{
				firstLineFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string firstLineChars
		{
			get
			{
				return firstLineCharsField;
			}
			set
			{
				firstLineCharsField = value;
			}
		}

		public CT_Ind()
		{
			firstLineField = -1L;
		}

		public static CT_Ind Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Ind cT_Ind = new CT_Ind();
			cT_Ind.left = XmlHelper.ReadString(node.Attributes["w:left"]);
			cT_Ind.leftChars = XmlHelper.ReadString(node.Attributes["w:leftChars"]);
			cT_Ind.right = XmlHelper.ReadString(node.Attributes["w:right"]);
			cT_Ind.rightChars = XmlHelper.ReadString(node.Attributes["w:rightChars"]);
			cT_Ind.hanging = XmlHelper.ReadULong(node.Attributes["w:hanging"]);
			cT_Ind.hangingChars = XmlHelper.ReadString(node.Attributes["w:hangingChars"]);
			if (node.Attributes["w:firstLine"] != null)
			{
				cT_Ind.firstLine = XmlHelper.ReadLong(node.Attributes["w:firstLine"]);
			}
			cT_Ind.firstLineChars = XmlHelper.ReadString(node.Attributes["w:firstLineChars"]);
			return cT_Ind;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:leftChars", leftChars);
			XmlHelper.WriteAttribute(sw, "w:left", left);
			XmlHelper.WriteAttribute(sw, "w:rightChars", rightChars);
			XmlHelper.WriteAttribute(sw, "w:right", right);
			XmlHelper.WriteAttribute(sw, "w:hangingChars", hangingChars);
			XmlHelper.WriteAttribute(sw, "w:hanging", (double)hanging);
			XmlHelper.WriteAttribute(sw, "w:firstLineChars", firstLineChars);
			if (firstLineField >= 0)
			{
				XmlHelper.WriteAttribute(sw, "w:firstLine", (double)firstLine, true);
			}
			sw.Write("/>");
		}

		public bool IsSetLeft()
		{
			return !string.IsNullOrEmpty(leftField);
		}

		public bool IsSetRight()
		{
			return !string.IsNullOrEmpty(rightField);
		}

		public bool IsSetHanging()
		{
			return hangingField != 0;
		}

		public bool IsSetFirstLine()
		{
			return firstLineField != 0;
		}
	}
}
