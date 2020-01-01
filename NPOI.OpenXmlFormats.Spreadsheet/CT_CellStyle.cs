using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CellStyle
	{
		private CT_ExtensionList extLstField;

		private string nameField;

		private uint xfIdField;

		private uint builtinIdField;

		private bool builtinIdFieldSpecified;

		private uint iLevelField;

		private bool iLevelFieldSpecified;

		private bool hiddenField;

		private bool hiddenFieldSpecified;

		private bool customBuiltinField;

		private bool customBuiltinFieldSpecified;

		[XmlElement]
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

		[XmlAttribute]
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

		[XmlAttribute]
		public uint xfId
		{
			get
			{
				return xfIdField;
			}
			set
			{
				xfIdField = value;
			}
		}

		[XmlAttribute]
		public uint builtinId
		{
			get
			{
				return builtinIdField;
			}
			set
			{
				builtinIdField = value;
			}
		}

		[XmlIgnore]
		public bool builtinIdSpecified
		{
			get
			{
				return builtinIdFieldSpecified;
			}
			set
			{
				builtinIdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint iLevel
		{
			get
			{
				return iLevelField;
			}
			set
			{
				iLevelField = value;
			}
		}

		[XmlIgnore]
		public bool iLevelSpecified
		{
			get
			{
				return iLevelFieldSpecified;
			}
			set
			{
				iLevelFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool hidden
		{
			get
			{
				return hiddenField;
			}
			set
			{
				hiddenField = value;
			}
		}

		[XmlIgnore]
		public bool hiddenSpecified
		{
			get
			{
				return hiddenFieldSpecified;
			}
			set
			{
				hiddenFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool customBuiltin
		{
			get
			{
				return customBuiltinField;
			}
			set
			{
				customBuiltinField = value;
			}
		}

		[XmlIgnore]
		public bool customBuiltinSpecified
		{
			get
			{
				return customBuiltinFieldSpecified;
			}
			set
			{
				customBuiltinFieldSpecified = value;
			}
		}

		public static CT_CellStyle Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellStyle cT_CellStyle = new CT_CellStyle();
			cT_CellStyle.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_CellStyle.xfId = XmlHelper.ReadUInt(node.Attributes["xfId"]);
			cT_CellStyle.builtinId = XmlHelper.ReadUInt(node.Attributes["builtinId"]);
			cT_CellStyle.iLevel = XmlHelper.ReadUInt(node.Attributes["iLevel"]);
			cT_CellStyle.hidden = XmlHelper.ReadBool(node.Attributes["hidden"]);
			cT_CellStyle.customBuiltin = XmlHelper.ReadBool(node.Attributes["customBuiltin"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_CellStyle.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_CellStyle;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "xfId", (double)xfId, true);
			XmlHelper.WriteAttribute(sw, "builtinId", builtinId);
			XmlHelper.WriteAttribute(sw, "iLevel", iLevel);
			XmlHelper.WriteAttribute(sw, "hidden", hidden, false);
			XmlHelper.WriteAttribute(sw, "customBuiltin", customBuiltin, false);
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
