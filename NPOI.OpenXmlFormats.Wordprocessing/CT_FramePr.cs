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
	public class CT_FramePr
	{
		private ST_DropCap dropCapField;

		private bool dropCapFieldSpecified;

		private string linesField;

		private ulong wField;

		private bool wFieldSpecified;

		private ulong hField;

		private bool hFieldSpecified;

		private ulong vSpaceField;

		private bool vSpaceFieldSpecified;

		private ulong hSpaceField;

		private bool hSpaceFieldSpecified;

		private ST_Wrap wrapField;

		private bool wrapFieldSpecified;

		private ST_HAnchor hAnchorField;

		private bool hAnchorFieldSpecified;

		private ST_VAnchor vAnchorField;

		private bool vAnchorFieldSpecified;

		private string xField;

		private ST_XAlign xAlignField;

		private bool xAlignFieldSpecified;

		private string yField;

		private ST_YAlign yAlignField;

		private bool yAlignFieldSpecified;

		private ST_HeightRule hRuleField;

		private bool hRuleFieldSpecified;

		private ST_OnOff anchorLockField;

		private bool anchorLockFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_DropCap dropCap
		{
			get
			{
				return dropCapField;
			}
			set
			{
				dropCapField = value;
			}
		}

		[XmlIgnore]
		public bool dropCapSpecified
		{
			get
			{
				return dropCapFieldSpecified;
			}
			set
			{
				dropCapFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string lines
		{
			get
			{
				return linesField;
			}
			set
			{
				linesField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong w
		{
			get
			{
				return wField;
			}
			set
			{
				wField = value;
			}
		}

		[XmlIgnore]
		public bool wSpecified
		{
			get
			{
				return wFieldSpecified;
			}
			set
			{
				wFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong h
		{
			get
			{
				return hField;
			}
			set
			{
				hField = value;
			}
		}

		[XmlIgnore]
		public bool hSpecified
		{
			get
			{
				return hFieldSpecified;
			}
			set
			{
				hFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong vSpace
		{
			get
			{
				return vSpaceField;
			}
			set
			{
				vSpaceField = value;
			}
		}

		[XmlIgnore]
		public bool vSpaceSpecified
		{
			get
			{
				return vSpaceFieldSpecified;
			}
			set
			{
				vSpaceFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong hSpace
		{
			get
			{
				return hSpaceField;
			}
			set
			{
				hSpaceField = value;
			}
		}

		[XmlIgnore]
		public bool hSpaceSpecified
		{
			get
			{
				return hSpaceFieldSpecified;
			}
			set
			{
				hSpaceFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Wrap wrap
		{
			get
			{
				return wrapField;
			}
			set
			{
				wrapField = value;
			}
		}

		[XmlIgnore]
		public bool wrapSpecified
		{
			get
			{
				return wrapFieldSpecified;
			}
			set
			{
				wrapFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_HAnchor hAnchor
		{
			get
			{
				return hAnchorField;
			}
			set
			{
				hAnchorField = value;
			}
		}

		[XmlIgnore]
		public bool hAnchorSpecified
		{
			get
			{
				return hAnchorFieldSpecified;
			}
			set
			{
				hAnchorFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_VAnchor vAnchor
		{
			get
			{
				return vAnchorField;
			}
			set
			{
				vAnchorField = value;
			}
		}

		[XmlIgnore]
		public bool vAnchorSpecified
		{
			get
			{
				return vAnchorFieldSpecified;
			}
			set
			{
				vAnchorFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string x
		{
			get
			{
				return xField;
			}
			set
			{
				xField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_XAlign xAlign
		{
			get
			{
				return xAlignField;
			}
			set
			{
				xAlignField = value;
			}
		}

		[XmlIgnore]
		public bool xAlignSpecified
		{
			get
			{
				return xAlignFieldSpecified;
			}
			set
			{
				xAlignFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string y
		{
			get
			{
				return yField;
			}
			set
			{
				yField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_YAlign yAlign
		{
			get
			{
				return yAlignField;
			}
			set
			{
				yAlignField = value;
			}
		}

		[XmlIgnore]
		public bool yAlignSpecified
		{
			get
			{
				return yAlignFieldSpecified;
			}
			set
			{
				yAlignFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_HeightRule hRule
		{
			get
			{
				return hRuleField;
			}
			set
			{
				hRuleField = value;
			}
		}

		[XmlIgnore]
		public bool hRuleSpecified
		{
			get
			{
				return hRuleFieldSpecified;
			}
			set
			{
				hRuleFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff anchorLock
		{
			get
			{
				return anchorLockField;
			}
			set
			{
				anchorLockField = value;
			}
		}

		[XmlIgnore]
		public bool anchorLockSpecified
		{
			get
			{
				return anchorLockFieldSpecified;
			}
			set
			{
				anchorLockFieldSpecified = value;
			}
		}

		public static CT_FramePr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FramePr cT_FramePr = new CT_FramePr();
			if (node.Attributes["w:dropCap"] != null)
			{
				cT_FramePr.dropCap = (ST_DropCap)Enum.Parse(typeof(ST_DropCap), node.Attributes["w:dropCap"].Value);
			}
			cT_FramePr.lines = XmlHelper.ReadString(node.Attributes["w:lines"]);
			cT_FramePr.w = XmlHelper.ReadULong(node.Attributes["w:w"]);
			cT_FramePr.h = XmlHelper.ReadULong(node.Attributes["w:h"]);
			cT_FramePr.vSpace = XmlHelper.ReadULong(node.Attributes["w:vSpace"]);
			cT_FramePr.hSpace = XmlHelper.ReadULong(node.Attributes["w:hSpace"]);
			if (node.Attributes["w:wrap"] != null)
			{
				cT_FramePr.wrap = (ST_Wrap)Enum.Parse(typeof(ST_Wrap), node.Attributes["w:wrap"].Value);
			}
			if (node.Attributes["w:hAnchor"] != null)
			{
				cT_FramePr.hAnchor = (ST_HAnchor)Enum.Parse(typeof(ST_HAnchor), node.Attributes["w:hAnchor"].Value);
			}
			if (node.Attributes["w:vAnchor"] != null)
			{
				cT_FramePr.vAnchor = (ST_VAnchor)Enum.Parse(typeof(ST_VAnchor), node.Attributes["w:vAnchor"].Value);
			}
			cT_FramePr.x = XmlHelper.ReadString(node.Attributes["w:x"]);
			if (node.Attributes["w:xAlign"] != null)
			{
				cT_FramePr.xAlign = (ST_XAlign)Enum.Parse(typeof(ST_XAlign), node.Attributes["w:xAlign"].Value);
			}
			cT_FramePr.y = XmlHelper.ReadString(node.Attributes["w:y"]);
			if (node.Attributes["w:yAlign"] != null)
			{
				cT_FramePr.yAlign = (ST_YAlign)Enum.Parse(typeof(ST_YAlign), node.Attributes["w:yAlign"].Value);
			}
			if (node.Attributes["w:hRule"] != null)
			{
				cT_FramePr.hRule = (ST_HeightRule)Enum.Parse(typeof(ST_HeightRule), node.Attributes["w:hRule"].Value);
			}
			if (node.Attributes["w:anchorLock"] != null)
			{
				cT_FramePr.anchorLock = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:anchorLock"].Value);
			}
			return cT_FramePr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:dropCap", dropCap.ToString());
			XmlHelper.WriteAttribute(sw, "w:lines", lines);
			XmlHelper.WriteAttribute(sw, "w:w", (double)w);
			XmlHelper.WriteAttribute(sw, "w:h", (double)h);
			XmlHelper.WriteAttribute(sw, "w:vSpace", (double)vSpace);
			XmlHelper.WriteAttribute(sw, "w:hSpace", (double)hSpace);
			XmlHelper.WriteAttribute(sw, "w:wrap", wrap.ToString());
			XmlHelper.WriteAttribute(sw, "w:hAnchor", hAnchor.ToString());
			XmlHelper.WriteAttribute(sw, "w:vAnchor", vAnchor.ToString());
			XmlHelper.WriteAttribute(sw, "w:x", x);
			XmlHelper.WriteAttribute(sw, "w:xAlign", xAlign.ToString());
			XmlHelper.WriteAttribute(sw, "w:y", y);
			XmlHelper.WriteAttribute(sw, "w:yAlign", yAlign.ToString());
			XmlHelper.WriteAttribute(sw, "w:hRule", hRule.ToString());
			XmlHelper.WriteAttribute(sw, "w:anchorLock", anchorLock.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
