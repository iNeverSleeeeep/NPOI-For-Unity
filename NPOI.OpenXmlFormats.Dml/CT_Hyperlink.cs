using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Hyperlink
	{
		private CT_EmbeddedWAVAudioFile sndField;

		private CT_OfficeArtExtensionList extLstField;

		private string idField;

		private string invalidUrlField;

		private string actionField;

		private string tgtFrameField;

		private string tooltipField;

		private bool historyField;

		private bool highlightClickField;

		private bool endSndField;

		[XmlElement(Order = 0)]
		public CT_EmbeddedWAVAudioFile snd
		{
			get
			{
				return sndField;
			}
			set
			{
				sndField = value;
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
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute]
		public string invalidUrl
		{
			get
			{
				return invalidUrlField;
			}
			set
			{
				invalidUrlField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute]
		public string action
		{
			get
			{
				return actionField;
			}
			set
			{
				actionField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
		public string tgtFrame
		{
			get
			{
				return tgtFrameField;
			}
			set
			{
				tgtFrameField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
		public string tooltip
		{
			get
			{
				return tooltipField;
			}
			set
			{
				tooltipField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool history
		{
			get
			{
				return historyField;
			}
			set
			{
				historyField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool highlightClick
		{
			get
			{
				return highlightClickField;
			}
			set
			{
				highlightClickField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool endSnd
		{
			get
			{
				return endSndField;
			}
			set
			{
				endSndField = value;
			}
		}

		public static CT_Hyperlink Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Hyperlink cT_Hyperlink = new CT_Hyperlink();
			cT_Hyperlink.id = XmlHelper.ReadString(node.Attributes["id"]);
			cT_Hyperlink.invalidUrl = XmlHelper.ReadString(node.Attributes["invalidUrl"]);
			cT_Hyperlink.action = XmlHelper.ReadString(node.Attributes["action"]);
			cT_Hyperlink.tgtFrame = XmlHelper.ReadString(node.Attributes["tgtFrame"]);
			cT_Hyperlink.tooltip = XmlHelper.ReadString(node.Attributes["tooltip"]);
			cT_Hyperlink.history = XmlHelper.ReadBool(node.Attributes["history"]);
			cT_Hyperlink.highlightClick = XmlHelper.ReadBool(node.Attributes["highlightClick"]);
			cT_Hyperlink.endSnd = XmlHelper.ReadBool(node.Attributes["endSnd"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "snd")
				{
					cT_Hyperlink.snd = CT_EmbeddedWAVAudioFile.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Hyperlink.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Hyperlink;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "id", id);
			XmlHelper.WriteAttribute(sw, "invalidUrl", invalidUrl);
			XmlHelper.WriteAttribute(sw, "action", action);
			XmlHelper.WriteAttribute(sw, "tgtFrame", tgtFrame);
			XmlHelper.WriteAttribute(sw, "tooltip", tooltip);
			XmlHelper.WriteAttribute(sw, "history", history);
			XmlHelper.WriteAttribute(sw, "highlightClick", highlightClick);
			XmlHelper.WriteAttribute(sw, "endSnd", endSnd);
			sw.Write(">");
			if (snd != null)
			{
				snd.Write(sw, "snd");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_Hyperlink()
		{
			invalidUrlField = "";
			actionField = "";
			tgtFrameField = "";
			tooltipField = "";
			historyField = true;
			highlightClickField = false;
			endSndField = false;
		}
	}
}
