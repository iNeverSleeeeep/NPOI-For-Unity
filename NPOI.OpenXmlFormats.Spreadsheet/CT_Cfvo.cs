using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Cfvo
	{
		private CT_ExtensionList extLstField;

		private ST_CfvoType typeField;

		private string valField;

		private bool gteField;

		public CT_ExtensionList extLst
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

		public ST_CfvoType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		public string val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[DefaultValue(true)]
		public bool gte
		{
			get
			{
				return gteField;
			}
			set
			{
				gteField = value;
			}
		}

		public CT_Cfvo()
		{
			gteField = true;
		}

		public static CT_Cfvo Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Cfvo cT_Cfvo = new CT_Cfvo();
			if (node.Attributes["type"] != null)
			{
				cT_Cfvo.type = (ST_CfvoType)Enum.Parse(typeof(ST_CfvoType), node.Attributes["type"].Value);
			}
			cT_Cfvo.val = XmlHelper.ReadString(node.Attributes["val"]);
			cT_Cfvo.gte = XmlHelper.ReadBool(node.Attributes["gte"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_Cfvo.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Cfvo;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "type", type.ToString());
			XmlHelper.WriteAttribute(sw, "val", val);
			if (gte)
			{
				XmlHelper.WriteAttribute(sw, "gte", gte);
			}
			if (extLst != null)
			{
				sw.Write(">");
				extLst.Write(sw, "extLst");
				sw.Write(string.Format("</{0}>", nodeName));
			}
			else
			{
				sw.Write("/>");
			}
		}
	}
}
