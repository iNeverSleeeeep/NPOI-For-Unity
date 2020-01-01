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
	public class CT_FileRecoveryPr
	{
		private bool autoRecoverField;

		private bool crashSaveField;

		private bool dataExtractLoadField;

		private bool repairLoadField;

		[DefaultValue(true)]
		public bool autoRecover
		{
			get
			{
				return autoRecoverField;
			}
			set
			{
				autoRecoverField = value;
			}
		}

		[DefaultValue(false)]
		public bool crashSave
		{
			get
			{
				return crashSaveField;
			}
			set
			{
				crashSaveField = value;
			}
		}

		[DefaultValue(false)]
		public bool dataExtractLoad
		{
			get
			{
				return dataExtractLoadField;
			}
			set
			{
				dataExtractLoadField = value;
			}
		}

		[DefaultValue(false)]
		public bool repairLoad
		{
			get
			{
				return repairLoadField;
			}
			set
			{
				repairLoadField = value;
			}
		}

		public static CT_FileRecoveryPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FileRecoveryPr cT_FileRecoveryPr = new CT_FileRecoveryPr();
			cT_FileRecoveryPr.autoRecover = XmlHelper.ReadBool(node.Attributes["autoRecover"]);
			cT_FileRecoveryPr.crashSave = XmlHelper.ReadBool(node.Attributes["crashSave"]);
			cT_FileRecoveryPr.dataExtractLoad = XmlHelper.ReadBool(node.Attributes["dataExtractLoad"]);
			cT_FileRecoveryPr.repairLoad = XmlHelper.ReadBool(node.Attributes["repairLoad"]);
			return cT_FileRecoveryPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "autoRecover", autoRecover);
			XmlHelper.WriteAttribute(sw, "crashSave", crashSave);
			XmlHelper.WriteAttribute(sw, "dataExtractLoad", dataExtractLoad);
			XmlHelper.WriteAttribute(sw, "repairLoad", repairLoad);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_FileRecoveryPr()
		{
			autoRecoverField = true;
			crashSaveField = false;
			dataExtractLoadField = false;
			repairLoadField = false;
		}
	}
}
