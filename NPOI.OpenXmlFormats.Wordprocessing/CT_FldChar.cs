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
	public class CT_FldChar
	{
		private object itemField;

		private ST_FldCharType fldCharTypeField;

		private ST_OnOff fldLockField;

		private bool fldLockFieldSpecified;

		private ST_OnOff dirtyField;

		private bool dirtyFieldSpecified;

		[XmlElement("fldData", typeof(CT_Text), Order = 0)]
		[XmlElement("numberingChange", typeof(CT_TrackChangeNumbering), Order = 0)]
		[XmlElement("ffData", typeof(CT_FFData), Order = 0)]
		public object Item
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_FldCharType fldCharType
		{
			get
			{
				return fldCharTypeField;
			}
			set
			{
				fldCharTypeField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff fldLock
		{
			get
			{
				return fldLockField;
			}
			set
			{
				fldLockField = value;
			}
		}

		[XmlIgnore]
		public bool fldLockSpecified
		{
			get
			{
				return fldLockFieldSpecified;
			}
			set
			{
				fldLockFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff dirty
		{
			get
			{
				return dirtyField;
			}
			set
			{
				dirtyField = value;
			}
		}

		[XmlIgnore]
		public bool dirtySpecified
		{
			get
			{
				return dirtyFieldSpecified;
			}
			set
			{
				dirtyFieldSpecified = value;
			}
		}

		public static CT_FldChar Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FldChar cT_FldChar = new CT_FldChar();
			if (node.Attributes["w:fldCharType"] != null)
			{
				cT_FldChar.fldCharType = (ST_FldCharType)Enum.Parse(typeof(ST_FldCharType), node.Attributes["w:fldCharType"].Value);
			}
			if (node.Attributes["w:fldLock"] != null)
			{
				cT_FldChar.fldLock = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:fldLock"].Value);
			}
			if (node.Attributes["w:dirty"] != null)
			{
				cT_FldChar.dirty = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:dirty"].Value);
			}
			return cT_FldChar;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:fldCharType", fldCharType.ToString());
			XmlHelper.WriteAttribute(sw, "w:fldLock", fldLock.ToString());
			XmlHelper.WriteAttribute(sw, "w:dirty", dirty.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
