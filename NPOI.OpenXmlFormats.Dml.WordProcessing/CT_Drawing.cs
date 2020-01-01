using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Drawing
	{
		private List<CT_Anchor> anchorField;

		private List<CT_Inline> inlineField;

		public List<CT_Anchor> anchor
		{
			get
			{
				return anchorField;
			}
			set
			{
				anchorField = value;
			}
		}

		public List<CT_Inline> inline
		{
			get
			{
				return inlineField;
			}
			set
			{
				inlineField = value;
			}
		}

		public static CT_Drawing Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Drawing cT_Drawing = new CT_Drawing();
			cT_Drawing.anchor = new List<CT_Anchor>();
			cT_Drawing.inline = new List<CT_Inline>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "anchor")
				{
					cT_Drawing.anchor.Add(CT_Anchor.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "inline")
				{
					cT_Drawing.inline.Add(CT_Inline.Parse(childNode, namespaceManager));
				}
			}
			return cT_Drawing;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (anchor != null)
			{
				foreach (CT_Anchor item in anchor)
				{
					item.Write(sw, "anchor");
				}
			}
			if (inline != null)
			{
				foreach (CT_Inline item2 in inline)
				{
					item2.Write(sw, "inline");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_Inline AddNewInline()
		{
			CT_Inline cT_Inline = new CT_Inline();
			if (inlineField == null)
			{
				inlineField = new List<CT_Inline>();
			}
			inlineField.Add(cT_Inline);
			return cT_Inline;
		}

		public List<CT_Anchor> GetAnchorList()
		{
			return anchor;
		}

		public List<CT_Inline> GetInlineList()
		{
			return inline;
		}

		public CT_Inline GetInlineArray(int p)
		{
			lock (this)
			{
				return inline[p];
			}
		}
	}
}
