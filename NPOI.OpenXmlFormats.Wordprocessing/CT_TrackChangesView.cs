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
	public class CT_TrackChangesView
	{
		private ST_OnOff markupField;

		private bool markupFieldSpecified;

		private ST_OnOff commentsField;

		private bool commentsFieldSpecified;

		private ST_OnOff insDelField;

		private bool insDelFieldSpecified;

		private ST_OnOff formattingField;

		private bool formattingFieldSpecified;

		private ST_OnOff inkAnnotationsField;

		private bool inkAnnotationsFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff markup
		{
			get
			{
				return markupField;
			}
			set
			{
				markupField = value;
			}
		}

		[XmlIgnore]
		public bool markupSpecified
		{
			get
			{
				return markupFieldSpecified;
			}
			set
			{
				markupFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff comments
		{
			get
			{
				return commentsField;
			}
			set
			{
				commentsField = value;
			}
		}

		[XmlIgnore]
		public bool commentsSpecified
		{
			get
			{
				return commentsFieldSpecified;
			}
			set
			{
				commentsFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff insDel
		{
			get
			{
				return insDelField;
			}
			set
			{
				insDelField = value;
			}
		}

		[XmlIgnore]
		public bool insDelSpecified
		{
			get
			{
				return insDelFieldSpecified;
			}
			set
			{
				insDelFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff formatting
		{
			get
			{
				return formattingField;
			}
			set
			{
				formattingField = value;
			}
		}

		[XmlIgnore]
		public bool formattingSpecified
		{
			get
			{
				return formattingFieldSpecified;
			}
			set
			{
				formattingFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff inkAnnotations
		{
			get
			{
				return inkAnnotationsField;
			}
			set
			{
				inkAnnotationsField = value;
			}
		}

		[XmlIgnore]
		public bool inkAnnotationsSpecified
		{
			get
			{
				return inkAnnotationsFieldSpecified;
			}
			set
			{
				inkAnnotationsFieldSpecified = value;
			}
		}

		public static CT_TrackChangesView Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TrackChangesView cT_TrackChangesView = new CT_TrackChangesView();
			if (node.Attributes["w:markup"] != null)
			{
				cT_TrackChangesView.markup = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:markup"].Value);
			}
			if (node.Attributes["w:comments"] != null)
			{
				cT_TrackChangesView.comments = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:comments"].Value);
			}
			if (node.Attributes["w:insDel"] != null)
			{
				cT_TrackChangesView.insDel = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:insDel"].Value);
			}
			if (node.Attributes["w:formatting"] != null)
			{
				cT_TrackChangesView.formatting = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:formatting"].Value);
			}
			if (node.Attributes["w:inkAnnotations"] != null)
			{
				cT_TrackChangesView.inkAnnotations = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:inkAnnotations"].Value);
			}
			return cT_TrackChangesView;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:markup", markup.ToString());
			XmlHelper.WriteAttribute(sw, "w:comments", comments.ToString());
			XmlHelper.WriteAttribute(sw, "w:insDel", insDel.ToString());
			XmlHelper.WriteAttribute(sw, "w:formatting", formatting.ToString());
			XmlHelper.WriteAttribute(sw, "w:inkAnnotations", inkAnnotations.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
