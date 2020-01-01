using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot("theme", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = false)]
	public class CT_OfficeStyleSheet
	{
		private CT_BaseStyles themeElementsField;

		private CT_ObjectStyleDefaults objectDefaultsField;

		private List<CT_ColorSchemeAndMapping> extraClrSchemeLstField;

		private List<CT_CustomColor> custClrLstField;

		private CT_OfficeArtExtensionList extLstField;

		private string nameField;

		[XmlElement]
		public CT_BaseStyles themeElements
		{
			get
			{
				return themeElementsField;
			}
			set
			{
				themeElementsField = value;
			}
		}

		[XmlElement]
		public CT_ObjectStyleDefaults objectDefaults
		{
			get
			{
				return objectDefaultsField;
			}
			set
			{
				objectDefaultsField = value;
			}
		}

		[XmlElement]
		public List<CT_ColorSchemeAndMapping> extraClrSchemeLst
		{
			get
			{
				return extraClrSchemeLstField;
			}
			set
			{
				extraClrSchemeLstField = value;
			}
		}

		[XmlElement]
		public List<CT_CustomColor> custClrLst
		{
			get
			{
				return custClrLstField;
			}
			set
			{
				custClrLstField = value;
			}
		}

		[XmlElement]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public static CT_OfficeStyleSheet Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OfficeStyleSheet cT_OfficeStyleSheet = new CT_OfficeStyleSheet();
			cT_OfficeStyleSheet.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_OfficeStyleSheet.extraClrSchemeLst = new List<CT_ColorSchemeAndMapping>();
			cT_OfficeStyleSheet.custClrLst = new List<CT_CustomColor>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "themeElements")
				{
					cT_OfficeStyleSheet.themeElements = CT_BaseStyles.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "objectDefaults")
				{
					cT_OfficeStyleSheet.objectDefaults = CT_ObjectStyleDefaults.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_OfficeStyleSheet.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extraClrSchemeLst")
				{
					cT_OfficeStyleSheet.extraClrSchemeLst.Add(CT_ColorSchemeAndMapping.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "custClrLst")
				{
					cT_OfficeStyleSheet.custClrLst.Add(CT_CustomColor.Parse(childNode, namespaceManager));
				}
			}
			return cT_OfficeStyleSheet;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<a:theme xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\"");
			XmlHelper.WriteAttribute(sw, "name", name);
			sw.Write(">");
			if (themeElements != null)
			{
				themeElements.Write(sw, "themeElements");
			}
			if (objectDefaults != null)
			{
				objectDefaults.Write(sw, "objectDefaults");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (extraClrSchemeLst != null)
			{
				foreach (CT_ColorSchemeAndMapping item in extraClrSchemeLst)
				{
					item.Write(sw, "extraClrSchemeLst");
				}
			}
			if (custClrLst != null)
			{
				foreach (CT_CustomColor item2 in custClrLst)
				{
					item2.Write(sw, "custClrLst");
				}
			}
			sw.Write(string.Format("</a:theme>"));
		}
	}
}
