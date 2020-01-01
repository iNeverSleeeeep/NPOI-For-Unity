using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_DataValidation
	{
		private string formula1Field;

		private string formula2Field;

		private ST_DataValidationType typeField;

		private ST_DataValidationErrorStyle errorStyleField;

		private ST_DataValidationImeMode imeModeField;

		private ST_DataValidationOperator operatorField;

		private bool allowBlankField;

		private bool showDropDownField;

		private bool showInputMessageField;

		private bool showErrorMessageField;

		private string errorTitleField;

		private string errorField;

		private string promptTitleField;

		private string promptField;

		private string sqrefField;

		[XmlElement(Order = 0)]
		public string formula1
		{
			get
			{
				return formula1Field;
			}
			set
			{
				formula1Field = value;
			}
		}

		[XmlElement(Order = 1)]
		public string formula2
		{
			get
			{
				return formula2Field;
			}
			set
			{
				formula2Field = value;
			}
		}

		[DefaultValue(ST_DataValidationType.none)]
		[XmlAttribute]
		public ST_DataValidationType type
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

		[DefaultValue(ST_DataValidationErrorStyle.stop)]
		[XmlAttribute]
		public ST_DataValidationErrorStyle errorStyle
		{
			get
			{
				return errorStyleField;
			}
			set
			{
				errorStyleField = value;
			}
		}

		[DefaultValue(ST_DataValidationImeMode.noControl)]
		[XmlAttribute]
		public ST_DataValidationImeMode imeMode
		{
			get
			{
				return imeModeField;
			}
			set
			{
				imeModeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_DataValidationOperator.between)]
		public ST_DataValidationOperator @operator
		{
			get
			{
				return operatorField;
			}
			set
			{
				operatorField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool allowBlank
		{
			get
			{
				return allowBlankField;
			}
			set
			{
				allowBlankField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool showDropDown
		{
			get
			{
				return showDropDownField;
			}
			set
			{
				showDropDownField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool showInputMessage
		{
			get
			{
				return showInputMessageField;
			}
			set
			{
				showInputMessageField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool showErrorMessage
		{
			get
			{
				return showErrorMessageField;
			}
			set
			{
				showErrorMessageField = value;
			}
		}

		[XmlAttribute]
		public string errorTitle
		{
			get
			{
				return errorTitleField;
			}
			set
			{
				errorTitleField = value;
			}
		}

		[XmlAttribute]
		public string error
		{
			get
			{
				return errorField;
			}
			set
			{
				errorField = value;
			}
		}

		[XmlAttribute]
		public string promptTitle
		{
			get
			{
				return promptTitleField;
			}
			set
			{
				promptTitleField = value;
			}
		}

		[XmlAttribute]
		public string prompt
		{
			get
			{
				return promptField;
			}
			set
			{
				promptField = value;
			}
		}

		[XmlAttribute]
		public string sqref
		{
			get
			{
				return sqrefField;
			}
			set
			{
				sqrefField = value;
			}
		}

		public static CT_DataValidation Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DataValidation cT_DataValidation = new CT_DataValidation();
			if (node.Attributes["type"] != null)
			{
				cT_DataValidation.type = (ST_DataValidationType)Enum.Parse(typeof(ST_DataValidationType), node.Attributes["type"].Value);
			}
			if (node.Attributes["errorStyle"] != null)
			{
				cT_DataValidation.errorStyle = (ST_DataValidationErrorStyle)Enum.Parse(typeof(ST_DataValidationErrorStyle), node.Attributes["errorStyle"].Value);
			}
			if (node.Attributes["imeMode"] != null)
			{
				cT_DataValidation.imeMode = (ST_DataValidationImeMode)Enum.Parse(typeof(ST_DataValidationImeMode), node.Attributes["imeMode"].Value);
			}
			if (node.Attributes["operator"] != null)
			{
				cT_DataValidation.@operator = (ST_DataValidationOperator)Enum.Parse(typeof(ST_DataValidationOperator), node.Attributes["operator"].Value);
			}
			cT_DataValidation.allowBlank = XmlHelper.ReadBool(node.Attributes["allowBlank"]);
			cT_DataValidation.showDropDown = XmlHelper.ReadBool(node.Attributes["showDropDown"]);
			cT_DataValidation.showInputMessage = XmlHelper.ReadBool(node.Attributes["showInputMessage"]);
			cT_DataValidation.showErrorMessage = XmlHelper.ReadBool(node.Attributes["showErrorMessage"]);
			cT_DataValidation.errorTitle = XmlHelper.ReadString(node.Attributes["errorTitle"]);
			cT_DataValidation.error = XmlHelper.ReadString(node.Attributes["error"]);
			cT_DataValidation.promptTitle = XmlHelper.ReadString(node.Attributes["promptTitle"]);
			cT_DataValidation.prompt = XmlHelper.ReadString(node.Attributes["prompt"]);
			cT_DataValidation.sqref = XmlHelper.ReadString(node.Attributes["sqref"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "formula1")
				{
					cT_DataValidation.formula1 = childNode.InnerText;
				}
				else if (childNode.LocalName == "formula2")
				{
					cT_DataValidation.formula2 = childNode.InnerText;
				}
			}
			return cT_DataValidation;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "type", type.ToString());
			XmlHelper.WriteAttribute(sw, "errorStyle", errorStyle.ToString());
			XmlHelper.WriteAttribute(sw, "imeMode", imeMode.ToString());
			XmlHelper.WriteAttribute(sw, "operator", @operator.ToString());
			XmlHelper.WriteAttribute(sw, "allowBlank", allowBlank);
			XmlHelper.WriteAttribute(sw, "showDropDown", showDropDown);
			XmlHelper.WriteAttribute(sw, "showInputMessage", showInputMessage);
			XmlHelper.WriteAttribute(sw, "showErrorMessage", showErrorMessage);
			XmlHelper.WriteAttribute(sw, "errorTitle", errorTitle);
			XmlHelper.WriteAttribute(sw, "error", error);
			XmlHelper.WriteAttribute(sw, "promptTitle", promptTitle);
			XmlHelper.WriteAttribute(sw, "prompt", prompt);
			XmlHelper.WriteAttribute(sw, "sqref", sqref);
			sw.Write(">");
			if (formula1 != null)
			{
				sw.Write(string.Format("<formula1><![CDATA[{0}]]></formula1>", formula1));
			}
			if (formula2 != null)
			{
				sw.Write(string.Format("<formula2><![CDATA[{0}]]></formula2>", formula2));
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_DataValidation()
		{
			typeField = ST_DataValidationType.none;
			errorStyleField = ST_DataValidationErrorStyle.stop;
			imeModeField = ST_DataValidationImeMode.noControl;
			operatorField = ST_DataValidationOperator.between;
			allowBlankField = false;
			showDropDownField = false;
			showInputMessageField = false;
			showErrorMessageField = false;
		}

		public void Set(CT_DataValidation obj)
		{
			formula1Field = obj.formula1Field;
			formula2Field = obj.formula2Field;
			typeField = obj.typeField;
			errorStyleField = obj.errorStyleField;
			imeModeField = obj.imeModeField;
			operatorField = obj.operatorField;
			allowBlankField = obj.allowBlankField;
			showDropDownField = obj.showDropDownField;
			showInputMessageField = obj.showInputMessageField;
			showErrorMessageField = obj.showErrorMessageField;
			errorTitleField = obj.errorTitleField;
			errorField = obj.errorField;
			promptTitleField = obj.promptTitleField;
			promptField = obj.promptField;
			sqrefField = obj.sqrefField;
		}
	}
}
