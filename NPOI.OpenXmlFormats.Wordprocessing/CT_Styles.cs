using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot("styles", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	public class CT_Styles
	{
		private CT_DocDefaults docDefaultsField;

		private CT_LatentStyles latentStylesField;

		private List<CT_Style> styleField;

		[XmlElement(Order = 0)]
		public CT_DocDefaults docDefaults
		{
			get
			{
				return docDefaultsField;
			}
			set
			{
				docDefaultsField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_LatentStyles latentStyles
		{
			get
			{
				return latentStylesField;
			}
			set
			{
				latentStylesField = value;
			}
		}

		[XmlElement("style", Order = 2)]
		public List<CT_Style> style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		public static CT_Styles Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Styles cT_Styles = new CT_Styles();
			cT_Styles.style = new List<CT_Style>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "docDefaults")
				{
					cT_Styles.docDefaults = CT_DocDefaults.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "latentStyles")
				{
					cT_Styles.latentStyles = CT_LatentStyles.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "style")
				{
					cT_Styles.style.Add(CT_Style.Parse(childNode, namespaceManager));
				}
			}
			return cT_Styles;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write("<w:styles xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\">");
			if (docDefaults != null)
			{
				docDefaults.Write(sw, "docDefaults");
			}
			if (latentStyles != null)
			{
				latentStyles.Write(sw, "latentStyles");
			}
			if (style != null)
			{
				foreach (CT_Style item in style)
				{
					item.Write(sw, "style");
				}
			}
			sw.Write("</w:styles>");
		}

		public IList<CT_Style> GetStyleList()
		{
			return style;
		}

		public void AddNewStyle()
		{
			CT_Style item = new CT_Style();
			if (styleField == null)
			{
				styleField = new List<CT_Style>();
			}
			styleField.Add(item);
		}

		public void SetStyleArray(int pos, CT_Style cT_Style)
		{
			lock (this)
			{
				styleField[pos] = cT_Style;
			}
		}

		public bool IsSetDocDefaults()
		{
			return docDefaultsField != null;
		}

		public CT_DocDefaults AddNewDocDefaults()
		{
			docDefaultsField = new CT_DocDefaults();
			return docDefaultsField;
		}
	}
}
