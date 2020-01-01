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
	public class CT_TblPPr
	{
		private ulong leftFromTextField;

		private bool leftFromTextFieldSpecified;

		private ulong rightFromTextField;

		private bool rightFromTextFieldSpecified;

		private ulong topFromTextField;

		private bool topFromTextFieldSpecified;

		private ulong bottomFromTextField;

		private bool bottomFromTextFieldSpecified;

		private ST_VAnchor vertAnchorField;

		private bool vertAnchorFieldSpecified;

		private ST_HAnchor horzAnchorField;

		private bool horzAnchorFieldSpecified;

		private ST_XAlign tblpXSpecField;

		private bool tblpXSpecFieldSpecified;

		private string tblpXField;

		private ST_YAlign tblpYSpecField;

		private bool tblpYSpecFieldSpecified;

		private string tblpYField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong leftFromText
		{
			get
			{
				return leftFromTextField;
			}
			set
			{
				leftFromTextField = value;
			}
		}

		[XmlIgnore]
		public bool leftFromTextSpecified
		{
			get
			{
				return leftFromTextFieldSpecified;
			}
			set
			{
				leftFromTextFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong rightFromText
		{
			get
			{
				return rightFromTextField;
			}
			set
			{
				rightFromTextField = value;
			}
		}

		[XmlIgnore]
		public bool rightFromTextSpecified
		{
			get
			{
				return rightFromTextFieldSpecified;
			}
			set
			{
				rightFromTextFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong topFromText
		{
			get
			{
				return topFromTextField;
			}
			set
			{
				topFromTextField = value;
			}
		}

		[XmlIgnore]
		public bool topFromTextSpecified
		{
			get
			{
				return topFromTextFieldSpecified;
			}
			set
			{
				topFromTextFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong bottomFromText
		{
			get
			{
				return bottomFromTextField;
			}
			set
			{
				bottomFromTextField = value;
			}
		}

		[XmlIgnore]
		public bool bottomFromTextSpecified
		{
			get
			{
				return bottomFromTextFieldSpecified;
			}
			set
			{
				bottomFromTextFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_VAnchor vertAnchor
		{
			get
			{
				return vertAnchorField;
			}
			set
			{
				vertAnchorField = value;
			}
		}

		[XmlIgnore]
		public bool vertAnchorSpecified
		{
			get
			{
				return vertAnchorFieldSpecified;
			}
			set
			{
				vertAnchorFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_HAnchor horzAnchor
		{
			get
			{
				return horzAnchorField;
			}
			set
			{
				horzAnchorField = value;
			}
		}

		[XmlIgnore]
		public bool horzAnchorSpecified
		{
			get
			{
				return horzAnchorFieldSpecified;
			}
			set
			{
				horzAnchorFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_XAlign tblpXSpec
		{
			get
			{
				return tblpXSpecField;
			}
			set
			{
				tblpXSpecFieldSpecified = true;
				tblpXSpecField = value;
			}
		}

		[XmlIgnore]
		public bool tblpXSpecSpecified
		{
			get
			{
				return tblpXSpecFieldSpecified;
			}
			set
			{
				tblpXSpecFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string tblpX
		{
			get
			{
				return tblpXField;
			}
			set
			{
				tblpXField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_YAlign tblpYSpec
		{
			get
			{
				return tblpYSpecField;
			}
			set
			{
				tblpYSpecFieldSpecified = true;
				tblpYSpecField = value;
			}
		}

		[XmlIgnore]
		public bool tblpYSpecSpecified
		{
			get
			{
				return tblpYSpecFieldSpecified;
			}
			set
			{
				tblpYSpecFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string tblpY
		{
			get
			{
				return tblpYField;
			}
			set
			{
				tblpYField = value;
			}
		}

		public static CT_TblPPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblPPr cT_TblPPr = new CT_TblPPr();
			cT_TblPPr.leftFromText = XmlHelper.ReadULong(node.Attributes["w:leftFromText"]);
			cT_TblPPr.rightFromText = XmlHelper.ReadULong(node.Attributes["w:rightFromText"]);
			cT_TblPPr.topFromText = XmlHelper.ReadULong(node.Attributes["w:topFromText"]);
			cT_TblPPr.bottomFromText = XmlHelper.ReadULong(node.Attributes["w:bottomFromText"]);
			if (node.Attributes["w:vertAnchor"] != null)
			{
				cT_TblPPr.vertAnchor = (ST_VAnchor)Enum.Parse(typeof(ST_VAnchor), node.Attributes["w:vertAnchor"].Value);
			}
			if (node.Attributes["w:horzAnchor"] != null)
			{
				cT_TblPPr.horzAnchor = (ST_HAnchor)Enum.Parse(typeof(ST_HAnchor), node.Attributes["w:horzAnchor"].Value);
			}
			if (node.Attributes["w:tblpXSpec"] != null)
			{
				cT_TblPPr.tblpXSpec = (ST_XAlign)Enum.Parse(typeof(ST_XAlign), node.Attributes["w:tblpXSpec"].Value);
			}
			cT_TblPPr.tblpX = XmlHelper.ReadString(node.Attributes["w:tblpX"]);
			if (node.Attributes["w:tblpYSpec"] != null)
			{
				cT_TblPPr.tblpYSpec = (ST_YAlign)Enum.Parse(typeof(ST_YAlign), node.Attributes["w:tblpYSpec"].Value);
			}
			cT_TblPPr.tblpY = XmlHelper.ReadString(node.Attributes["w:tblpY"]);
			return cT_TblPPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:leftFromText", (double)leftFromText);
			XmlHelper.WriteAttribute(sw, "w:rightFromText", (double)rightFromText);
			XmlHelper.WriteAttribute(sw, "w:topFromText", (double)topFromText);
			XmlHelper.WriteAttribute(sw, "w:bottomFromText", (double)bottomFromText);
			XmlHelper.WriteAttribute(sw, "w:vertAnchor", vertAnchor.ToString());
			XmlHelper.WriteAttribute(sw, "w:horzAnchor", horzAnchor.ToString());
			if (tblpXSpecFieldSpecified)
			{
				XmlHelper.WriteAttribute(sw, "w:tblpXSpec", tblpXSpec.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:tblpX", tblpX);
			if (tblpYSpecFieldSpecified)
			{
				XmlHelper.WriteAttribute(sw, "w:tblpYSpec", tblpYSpec.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:tblpY", tblpY);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
