using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot("blip", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = false)]
	public class CT_Blip
	{
		private List<object> itemsField;

		private CT_OfficeArtExtensionList extLstField;

		private string embedField;

		private string linkField;

		private ST_BlipCompression cstateField;

		[XmlElement("tint", typeof(CT_TintEffect), Order = 0)]
		[XmlElement("biLevel", typeof(CT_BiLevelEffect), Order = 0)]
		[XmlElement("lum", typeof(CT_LuminanceEffect), Order = 0)]
		[XmlElement("alphaCeiling", typeof(CT_AlphaCeilingEffect), Order = 0)]
		[XmlElement("alphaFloor", typeof(CT_AlphaFloorEffect), Order = 0)]
		[XmlElement("alphaInv", typeof(CT_AlphaInverseEffect), Order = 0)]
		[XmlElement("alphaMod", typeof(CT_AlphaModulateEffect), Order = 0)]
		[XmlElement("alphaModFix", typeof(CT_AlphaModulateFixedEffect), Order = 0)]
		[XmlElement("alphaRepl", typeof(CT_AlphaReplaceEffect), Order = 0)]
		[XmlElement("alphaBiLevel", typeof(CT_AlphaBiLevelEffect), Order = 0)]
		[XmlElement("blur", typeof(CT_BlurEffect), Order = 0)]
		[XmlElement("clrChange", typeof(CT_ColorChangeEffect), Order = 0)]
		[XmlElement("clrRepl", typeof(CT_ColorReplaceEffect), Order = 0)]
		[XmlElement("duotone", typeof(CT_DuotoneEffect), Order = 0)]
		[XmlElement("fillOverlay", typeof(CT_FillOverlayEffect), Order = 0)]
		[XmlElement("grayscl", typeof(CT_GrayscaleEffect), Order = 0)]
		[XmlElement("hsl", typeof(CT_HSLEffect), Order = 0)]
		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		[DefaultValue("")]
		public string embed
		{
			get
			{
				return embedField;
			}
			set
			{
				embedField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string link
		{
			get
			{
				return linkField;
			}
			set
			{
				linkField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_BlipCompression.none)]
		public ST_BlipCompression cstate
		{
			get
			{
				return cstateField;
			}
			set
			{
				cstateField = value;
			}
		}

		public CT_Blip()
		{
			embedField = "";
			linkField = "";
			cstateField = ST_BlipCompression.none;
		}

		public static CT_Blip Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Blip cT_Blip = new CT_Blip();
			cT_Blip.embed = XmlHelper.ReadString(node.Attributes["r:embed"]);
			cT_Blip.link = XmlHelper.ReadString(node.Attributes["r:link"]);
			if (node.Attributes["cstate"] != null)
			{
				cT_Blip.cstate = (ST_BlipCompression)Enum.Parse(typeof(ST_BlipCompression), node.Attributes["cstate"].Value);
			}
			cT_Blip.Items = new List<object>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_Blip.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Blip;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0} xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\"", nodeName));
			XmlHelper.WriteAttribute(sw, "r:embed", embed);
			XmlHelper.WriteAttribute(sw, "r:link", link);
			if (cstate != ST_BlipCompression.none)
			{
				XmlHelper.WriteAttribute(sw, "cstate", cstate.ToString());
			}
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
