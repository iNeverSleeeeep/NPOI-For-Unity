using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_DataValidations
	{
		private List<CT_DataValidation> dataValidationField;

		private bool disablePromptsField;

		private uint xWindowField;

		private bool xWindowFieldSpecified;

		private uint yWindowField;

		private bool yWindowFieldSpecified;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement("dataValidation", Order = 0)]
		public List<CT_DataValidation> dataValidation
		{
			get
			{
				return dataValidationField;
			}
			set
			{
				dataValidationField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool disablePrompts
		{
			get
			{
				return disablePromptsField;
			}
			set
			{
				disablePromptsField = value;
			}
		}

		[XmlAttribute]
		public uint xWindow
		{
			get
			{
				return xWindowField;
			}
			set
			{
				xWindowField = value;
			}
		}

		[XmlIgnore]
		public bool xWindowSpecified
		{
			get
			{
				return xWindowFieldSpecified;
			}
			set
			{
				xWindowFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint yWindow
		{
			get
			{
				return yWindowField;
			}
			set
			{
				yWindowField = value;
			}
		}

		[XmlIgnore]
		public bool yWindowSpecified
		{
			get
			{
				return yWindowFieldSpecified;
			}
			set
			{
				yWindowFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		[XmlIgnore]
		public bool countSpecified
		{
			get
			{
				return countFieldSpecified;
			}
			set
			{
				countFieldSpecified = value;
			}
		}

		public CT_DataValidations()
		{
			disablePromptsField = false;
		}

		public static CT_DataValidations Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DataValidations cT_DataValidations = new CT_DataValidations();
			cT_DataValidations.disablePrompts = XmlHelper.ReadBool(node.Attributes["disablePrompts"]);
			cT_DataValidations.xWindow = XmlHelper.ReadUInt(node.Attributes["xWindow"]);
			cT_DataValidations.yWindow = XmlHelper.ReadUInt(node.Attributes["yWindow"]);
			cT_DataValidations.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_DataValidations.dataValidation = new List<CT_DataValidation>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "dataValidation")
				{
					cT_DataValidations.dataValidation.Add(CT_DataValidation.Parse(childNode, namespaceManager));
				}
			}
			return cT_DataValidations;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "disablePrompts", disablePrompts);
			XmlHelper.WriteAttribute(sw, "xWindow", xWindow);
			XmlHelper.WriteAttribute(sw, "yWindow", yWindow);
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (dataValidation != null)
			{
				foreach (CT_DataValidation item in dataValidation)
				{
					item.Write(sw, "dataValidation");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public int sizeOfDataValidationArray()
		{
			return (int)countField;
		}

		public CT_DataValidation AddNewDataValidation()
		{
			if (dataValidationField == null)
			{
				dataValidationField = new List<CT_DataValidation>();
			}
			CT_DataValidation cT_DataValidation = new CT_DataValidation();
			dataValidationField.Add(cT_DataValidation);
			countField++;
			return cT_DataValidation;
		}
	}
}
