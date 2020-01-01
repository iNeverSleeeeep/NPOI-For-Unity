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
	public class CT_IgnoredError
	{
		private string sqrefField;

		private bool evalErrorField;

		private bool twoDigitTextYearField;

		private bool numberStoredAsTextField;

		private bool formulaField;

		private bool formulaRangeField;

		private bool unlockedFormulaField;

		private bool emptyCellReferenceField;

		private bool listDataValidationField;

		private bool calculatedColumnField;

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

		[XmlAttribute]
		[DefaultValue(false)]
		public bool evalError
		{
			get
			{
				return evalErrorField;
			}
			set
			{
				evalErrorField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool twoDigitTextYear
		{
			get
			{
				return twoDigitTextYearField;
			}
			set
			{
				twoDigitTextYearField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool numberStoredAsText
		{
			get
			{
				return numberStoredAsTextField;
			}
			set
			{
				numberStoredAsTextField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool formula
		{
			get
			{
				return formulaField;
			}
			set
			{
				formulaField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool formulaRange
		{
			get
			{
				return formulaRangeField;
			}
			set
			{
				formulaRangeField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool unlockedFormula
		{
			get
			{
				return unlockedFormulaField;
			}
			set
			{
				unlockedFormulaField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool emptyCellReference
		{
			get
			{
				return emptyCellReferenceField;
			}
			set
			{
				emptyCellReferenceField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool listDataValidation
		{
			get
			{
				return listDataValidationField;
			}
			set
			{
				listDataValidationField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool calculatedColumn
		{
			get
			{
				return calculatedColumnField;
			}
			set
			{
				calculatedColumnField = value;
			}
		}

		public CT_IgnoredError()
		{
			evalErrorField = false;
			twoDigitTextYearField = false;
			numberStoredAsTextField = false;
			formulaField = false;
			formulaRangeField = false;
			unlockedFormulaField = false;
			emptyCellReferenceField = false;
			listDataValidationField = false;
			calculatedColumnField = false;
		}

		public static CT_IgnoredError Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_IgnoredError cT_IgnoredError = new CT_IgnoredError();
			cT_IgnoredError.evalError = XmlHelper.ReadBool(node.Attributes["evalError"]);
			cT_IgnoredError.twoDigitTextYear = XmlHelper.ReadBool(node.Attributes["twoDigitTextYear"]);
			cT_IgnoredError.numberStoredAsText = XmlHelper.ReadBool(node.Attributes["numberStoredAsText"]);
			cT_IgnoredError.formula = XmlHelper.ReadBool(node.Attributes["formula"]);
			cT_IgnoredError.formulaRange = XmlHelper.ReadBool(node.Attributes["formulaRange"]);
			cT_IgnoredError.unlockedFormula = XmlHelper.ReadBool(node.Attributes["unlockedFormula"]);
			cT_IgnoredError.emptyCellReference = XmlHelper.ReadBool(node.Attributes["emptyCellReference"]);
			cT_IgnoredError.listDataValidation = XmlHelper.ReadBool(node.Attributes["listDataValidation"]);
			cT_IgnoredError.calculatedColumn = XmlHelper.ReadBool(node.Attributes["calculatedColumn"]);
			cT_IgnoredError.sqref = XmlHelper.ReadString(node.Attributes["sqref"]);
			return cT_IgnoredError;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "evalError", evalError);
			XmlHelper.WriteAttribute(sw, "twoDigitTextYear", twoDigitTextYear);
			XmlHelper.WriteAttribute(sw, "numberStoredAsText", numberStoredAsText);
			XmlHelper.WriteAttribute(sw, "formula", formula);
			XmlHelper.WriteAttribute(sw, "formulaRange", formulaRange);
			XmlHelper.WriteAttribute(sw, "unlockedFormula", unlockedFormula);
			XmlHelper.WriteAttribute(sw, "emptyCellReference", emptyCellReference);
			XmlHelper.WriteAttribute(sw, "listDataValidation", listDataValidation);
			XmlHelper.WriteAttribute(sw, "calculatedColumn", calculatedColumn);
			XmlHelper.WriteAttribute(sw, "sqref", sqref);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
